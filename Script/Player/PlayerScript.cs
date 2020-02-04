using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void DeadEventHandler();

public class PlayerScript :CharacterScript
{
    public Rigidbody2D MyRigidbody { get; set; }
    public bool Attack { get; set; }
    public bool Jump { get; set; }
    public bool OnGround { get; set; }
    public bool TakingDamage;
    [SerializeField]
    private int health;
    public event DeadEventHandler Dead;
    public bool IsHand, IsSword, IsGun;
    private int handDamage = 1;
    private int swordDamage = 2;
    private int gunDamage = 2;
    private int handLevel = 1;
    private int swordLevel = 1;
    private int gunLevel = 1;
    private int healthLevel = 1;
    private int healthMax = 3;
    [SerializeField]
    private EdgeCollider2D sword,hand;
    private bool immortal = false;
    [SerializeField]
    private RuntimeAnimatorController[] Char;
    [SerializeField]
    private float immortalTime;
    [SerializeField]
    private GameObject bullets;
    [SerializeField]
    private Transform[] groundPoints;
    [SerializeField]
    private float groundRadius;
    [SerializeField]
    private bool airControl;
    [SerializeField]
    private float jumpforce;
    [SerializeField]
    private LayerMask whatIsGround;
    public GameObject currentInterObj;
    private static PlayerScript instance;
    private int cost = 7;
    private SpriteRenderer spriteRenderer;
    private Vector2 startPos;
    [SerializeField]
    private float BatasKanan;
    [SerializeField]
    private float BatasKiri;
    private bool knockBack;
    public bool OnBossFighting;
    private AudioSource myAudioSource;
    [SerializeField]
    private AudioClip shotSound,hurtSound,punchSound,swordSound,pickedCoinSound;
    private int WeaponResource=5;





    public static PlayerScript Instance {

        get {
            if (instance == null) {
                instance = GameObject.FindObjectOfType<PlayerScript>();
            }
            return instance;
        }
        set {

        }
       
    }

 

    public override bool IsDead {
        get {
            if (health <= 0) {
                OnDead();
               
            }
            return health <= 0;
        }
    }

    public int Health { 
            get {
                return health;
            }

        }



    public int HandDamage { get {
            return handDamage;
        } }
    public int SwordDamage { get {
            return swordDamage;
        } }
    public int GunDamage { get {
            return gunDamage;
        }
        set {
            this.gunDamage = value;
        }
    }
 
public bool ResourceIsEmpty {
        get {
            return WeaponResource <= 0;
        }
    }


    // Start is called before the first frame update


    public override void Start()
    {
        base.Start();
        Time.timeScale = 1;
        startPos = transform.position;
        IsHand = true;
        MyRigidbody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        //this.GetComponent<Animator>().runtimeAnimatorController = Char[0];
        myAnimator = this.GetComponent<Animator>();
        currentInterObj = null;
        knockBack = false;
        CheckStat();
        health = healthMax;
        myAudioSource = GetComponent<AudioSource>();
        GameManager.Instance.Initialize();
    }
    // Update is called once per frame
    private void Update()
    {
        //Debug.Log(transform.position.x);
        
        transform.position = new Vector2(
  Mathf.Clamp(transform.position.x, BatasKiri, BatasKanan), transform.position.y);
        CheckStat();
        if (!TakingDamage && !IsDead )
        {
            if (transform.position.y <= -14f) {
               
                Death();
            }
            HandleInput();
        }
        if (IsDead) {
            MyRigidbody.velocity = Vector2.zero;
            
        }
        
        

        

        
    }
  

    public void shoot() {

        if (IsGun) {
            myAudioSource.clip = shotSound;
            myAudioSource.Play();
            WeaponResource -= 1;
            if (facingRight)
            {
               GameObject tmp=(GameObject)Instantiate(bullets, new Vector2(transform.position.x+1,transform.position.y), Quaternion.identity);
                tmp.GetComponent<Bullets>().Initialize(Vector2.right,true);
               
            }
            else {
                GameObject tmp=(GameObject)Instantiate(bullets, new Vector2(transform.position.x-1,transform.position.y), Quaternion.identity);
                tmp.GetComponent<Bullets>().Initialize(Vector2.left,true);
               
            }

            
        }
    }
    public void dropWeapon() {

        Jump = false;
            myAnimator.runtimeAnimatorController = Char[2];
        punch = hand;
            IsHand = true;
            IsGun = !IsHand;
            IsSword = !IsHand;
  
    }

   
    void FixedUpdate()
    {
        if (!IsDead)
        {
            float horizontal = Input.GetAxis("Horizontal");
            OnGround = IsGrounded();
            HandleMovement(horizontal);
            Flip(horizontal);
            HandleLayers();

        }
        
    }



    private void HandleInput() {
        if (Input.GetKeyDown(KeyCode.Z)) {
            myAnimator.SetTrigger("attack");
        }
        if (Input.GetButton("Jump")&&!IsFalling) {
            
            myAnimator.SetTrigger("jump");
        }
        if (Input.GetKeyDown(KeyCode.X)&& currentInterObj) {
            if (OnBossFighting)
                currentInterObj.SendMessage("OpenChest");
            else
                currentInterObj.SendMessage("BringTheQuiz");


        }

    }
    private void HandleMovement(float horizontal) {

        
        if (IsFalling&&OnGround) {
            myAnimator.SetBool("land", true);
        }
        if (!Attack && (OnGround || airControl)&&!knockBack)
        {

            MyRigidbody.velocity = new Vector2(horizontal*movementSpeed,MyRigidbody.velocity.y);
        }
        if ((Jump && (MyRigidbody.velocity.y == 0)&&OnGround)) {
            myAnimator.SetBool("land", false);
            MyRigidbody.AddForce(new Vector2(0,(MyRigidbody.velocity.y*0)+jumpforce));
            
        }
        myAnimator.SetFloat("speed", Mathf.Abs(horizontal));
    }
    private void Flip(float horizontal) {
        if (horizontal > 0 && !facingRight || horizontal<0 && facingRight) {
            ChangeDirection();
        }

    }


    public void OnDead() {
        if (Dead != null) {
           
            Dead();
        }
    }

    private bool IsGrounded() {
        if (MyRigidbody.velocity.y <= 0)
        {
            foreach (Transform point in groundPoints)

            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(point.position, groundRadius, whatIsGround);
                for (int i = 0; i < colliders.Length; i++) {
                    if (colliders[i].gameObject != gameObject) {

                        return true;
                    }
                }
            }
        }
        return false;
    }

   
    private void HandleLayers() {
        if (!OnGround)
        {
            myAnimator.SetLayerWeight(1, 1);
        }
        else {
            myAnimator.SetLayerWeight(1, 0);
        }
    }

    private IEnumerator IndicateImmortal() {
        
        while (immortal) {
            spriteRenderer.enabled = false;
            yield return new WaitForSeconds(.1f);
            spriteRenderer.enabled = true;
            knockBack = false;
            yield return new WaitForSeconds(.1f);
            
        }
    }
    public override void OnTriggerEnter2D(Collider2D collision)
    {

        base.OnTriggerEnter2D(collision);
       
        if (!IsDead)
        {
            if (collision.CompareTag("Chest"))
            {
                currentInterObj = collision.gameObject;
               

            }
            if (collision.gameObject.tag == "Coin")
            {
                myAudioSource.clip = pickedCoinSound;
                myAudioSource.Play();
                Destroy(collision.gameObject);
                GameManager.Instance.PLAYERSKILLPOINT+= 1;

            }
            if (collision.gameObject.tag == "Sword-Item")
            {
                Destroy(collision.gameObject);
                myAnimator.runtimeAnimatorController = Char[0];
                punch = sword;
                WeaponResource = 5;
                IsGun = false;
                IsHand = false;
                IsSword = true;
                PlayerScript.Instance.Jump = false;

            }
            if (collision.gameObject.tag == "Gun")
            {
                Destroy(collision.gameObject);
                WeaponResource = 5;
                myAnimator.runtimeAnimatorController = Char[1];
                IsGun = true;
                IsHand = false;
                IsSword = false;
                PlayerScript.Instance.Jump = false;
            }

            if (collision.gameObject.tag == "Heart")
            {
                Destroy(collision.gameObject);
                if (health < healthMax)
                {
                    health += 1;
                }
            }
            if (collision.gameObject.tag == "Golem")
            {
                StartCoroutine(TakeDamage("Golem"));
            }
            if (collision.gameObject.tag == "Rock")
            {
                StartCoroutine(TakeDamage("Rock"));
            }
            if (collision.gameObject.tag == "TutorialEvent")
            {
                Destroy(collision.gameObject);
                UIManager.Instance.showTheTutorial();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Chest"))
        {
            if (collision.gameObject==currentInterObj) {
                currentInterObj = null;
            }

        }
     

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "MovingGround"&&IsGrounded()) {
            MyRigidbody.transform.parent = collision.transform;
        }
        if (collision.gameObject.tag == "Spike") {
            StartCoroutine(TakeDamage("Spike"));
            knockBack = true;
        }



    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "MovingGround")
        {
            MyRigidbody.transform.parent = null;
        }

    }

    public override void MeleeAttack()
    {
        base.MeleeAttack();
        if (IsSword)
            WeaponResource -= 1;
    }


    public override IEnumerator TakeDamage(string weaponType)
    {


        if (!immortal)
        {

            if (weaponType == "EnemyPunch")
            {
                health -= 1;

            }
            else if (weaponType == "EnemySword")
            {
                health -= 2;
            }
            else if (weaponType == "Enemy-Bullets")
            {
                health -= 2;
            }
            else if (weaponType == "Spike")
            {
                health -= 2;
                MyRigidbody.AddForce(new Vector2(-500,MyRigidbody.velocity.y));
            }
            else if (weaponType == "Golem") {
                health -= 2;
            }
            else if (weaponType == "Rock")
            {
                health -= 2;
            }
            else if (weaponType == "Kunai")
            {
                health -= 2;
            }
            else if (weaponType == "NinjaSword")
            {
                health -= 4;
            }
            else if (weaponType == "Piranha")
            {
                health -= 2;
            }
            else if (weaponType == "Claw")
            {
                health -= 5;
            }
            else if (weaponType == "RunCrusher")
            {
                health -= 2;
            }



            if (!IsDead)
            {
                myAnimator.SetTrigger("damage");
                
                immortal = true;
                myAudioSource.clip = hurtSound;
                myAudioSource.Play();
                StartCoroutine(IndicateImmortal());
                yield return new WaitForSeconds(immortalTime);
               
                immortal = false;
            }
            else
            {
                
                if (!facingRight) {
                    ChangeDirection();
                }
                
                myAnimator.SetLayerWeight(1, 0);
                myAnimator.SetTrigger("die");

            }
            yield return null;
        }
    }

    public bool IsFalling {
        get {

            return MyRigidbody.velocity.y < 0;
        }
    }

    public override void Death()
    {
        
        UIManager.Instance.OnDeadUI();
        // MyRigidbody.velocity = Vector2.zero;
        //myAnimator.SetTrigger("idle");
        //health = 3;
        //transform.position = startPos;
    }
    public void playPunchSound()
    {
        myAudioSource.clip = punchSound;
        myAudioSource.Play();
    }
    public void playSwordSound()
    {
        myAudioSource.clip = swordSound;
        myAudioSource.Play();
    }

    public void CheckStat() {
        handDamage =GameManager.Instance.PLAYERHANDLEVEL;
        gunDamage = GameManager.Instance.PLAYERGUNLEVEL + 1;
        swordDamage = GameManager.Instance.PLAYERSWORDLEVEL + 1;
        healthMax = GameManager.Instance.PLAYERHEALTHLEVEL == 1 ? 3 : GameManager.Instance.PLAYERHEALTHLEVEL == 2 ? 5 : 7;
    }




}