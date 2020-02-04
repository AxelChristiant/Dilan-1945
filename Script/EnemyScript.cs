using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EnemyScript : CharacterScript
{
    private IEnemyState currentState;

    
    public GameObject Target;
    [SerializeField]
    private Transform leftEdge;
    [SerializeField]
    private Transform rightEdge;
    [SerializeField]
    private int health;
    [SerializeField]
    private bool isGunned,isSword;

    [SerializeField]
    private Sprite[] HeartSprites;
    [SerializeField]
    private Image HeartUI;

    [SerializeField]
    private GameObject droppedCoin;

    [SerializeField]
    private GameObject bullets;

    private float currentPosition;
    [SerializeField]
    private Transform shotPosition;

    private Collider2D Player;


    private Canvas healthCanvas;

    private static EnemyScript instance;

    private bool dropItem = true;
    private Rigidbody2D MyRigidBody;
    private AudioSource myAudioSource;
    [SerializeField]
    private AudioClip shotSound, hurtSound, punchSound, swordSound;
    private bool IsAlive;




    public bool IsGunned {
        get{
            return isGunned;
        }
    }
    public int Health
    {
        get
        {
            return health;
        }

    }

    public static EnemyScript Instance
    {

        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<EnemyScript>();
            }
            return instance;
        }

    }


    public override bool IsDead
    {
        get
        {

            return health <= 0;
        }
    }



    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        PlayerScript.Instance.Dead += new DeadEventHandler(RemoveTarget);
        ChangeState(new IdleState());
        healthCanvas = transform.GetComponentInChildren<Canvas>();
        currentPosition = this.transform.position.x;
        MyRigidBody = GetComponent<Rigidbody2D>();
        Player = GameObject.FindObjectOfType<PlayerScript>().GetComponent<Collider2D>();
        Physics2D.IgnoreCollision(Player, GetComponent<Collider2D>());
        myAudioSource = GetComponent<AudioSource>();
        IsAlive = true;
    }

    public void RemoveTarget() {
        Target = null;
        ChangeState(new PatrolState());

    }

    public void shoot()
    {

        if (isGunned)
        {
            myAudioSource.clip = shotSound;
            myAudioSource.Play();
            if (facingRight)
            {
                GameObject tmp = (GameObject)Instantiate(bullets,shotPosition.position , Quaternion.identity);
                tmp.GetComponent<Bullets>().Initialize(Vector2.right,false);

            }
            else
            {
                GameObject tmp = (GameObject)Instantiate(bullets, shotPosition.position, Quaternion.identity);
                tmp.GetComponent<Bullets>().Initialize(Vector2.left,false);

            }
        }
    }
    private void LookAtTarget() {
        if (Target != null) {
            float xDir = Target.transform.position.x - transform.position.x;

            if (xDir < 0 && facingRight || xDir > 0 && !facingRight) {
                ChangeDirection();
            }
        }

    }

    // Update is called once per frame
    void Update()
    {

        
        if (health <= 0)
        {
            HeartUI.sprite = HeartSprites[0];
        }
        else
        {

            HeartUI.sprite = HeartSprites[health];
        }

        if (!IsDead)
        {
            if (!TakingDamage) {
                currentState.Execute();
            }
            
            LookAtTarget();
        }
    }

    public void ChangeState(IEnemyState newState) {
        if (currentState != null) {
            currentState.Exit();
        }
        currentState = newState;
        currentState.Enter(this);
    }

    public void Move() {

        

        if (!attack)
        {

            if ((GetDirection().x > 0 && transform.position.x < rightEdge.position.x) || (GetDirection().x < 0 && transform.position.x >leftEdge.position.x))
            {
               
                myAnimator.SetFloat("speed", 1);
                transform.Translate(GetDirection() * (movementSpeed * Time.deltaTime));
            }
            else if (currentState is PatrolState)
            {

                ChangeDirection();
            }
            else if (currentState is RangedState) {
                Target = null;
                ChangeState(new IdleState());
            }
          
        }
        }
    public Vector2 GetDirection() {
        return facingRight ? Vector2.right : Vector2.left;
    }

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        currentState.OnTriggerEnter(collision);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "MovingGround")
        {
            MyRigidBody.transform.parent = collision.transform;
            rightEdge.transform.parent = collision.transform;
            leftEdge.transform.parent = collision.transform;

        }
        if (collision.gameObject.tag == "Coin") {
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
        }
        if (collision.gameObject.tag == "Gun") {
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
        }
        if (collision.gameObject.tag == "Heart")
        {
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
        }
        if (collision.gameObject.tag == "Sword-Item")
        {
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
        }

    }

    public override IEnumerator TakeDamage(string weaponType)
    {


        int damage = 0;

        switch (weaponType) {
            case "Melee":
                damage = PlayerScript.Instance.HandDamage;
                break;
            case "Sword":
                damage = PlayerScript.Instance.SwordDamage;
                break;
            case "Bullets":
                damage = PlayerScript.Instance.GunDamage;
                break;

        }
        
        if (health > 0)
        {
            health -= damage;
        }
        if (!IsDead)
        {
            myAudioSource.clip = hurtSound;
            myAudioSource.Play();
            myAnimator.SetFloat("speed",0);
            myAnimator.SetTrigger("damage");
        }
        else {
            if (!facingRight)
            {
                ChangeDirection();
            }
            if (dropItem)
            {
                for (int i = 0; i < 2; i++)
                {

                    float randomNum = Random.Range(0f, 1f);
                    GameObject coin = (GameObject)Instantiate(droppedCoin, new Vector3(transform.position.x , transform.position.y + 1f), Quaternion.identity);
                    Rigidbody2D RiCoin = GetComponent<Rigidbody2D>();
                    RiCoin.velocity = new Vector2(2, 0);
                    Physics2D.IgnoreCollision(coin.GetComponent<Collider2D>(), GetComponent<Collider2D>());
                }
                dropItem = false;
            }

            if (IsAlive)
            {
                myAnimator.SetTrigger("die");
                IsAlive = false;
            }
            yield return null;
        }
    }

    public override void Death()
    {
        LevelManager.Instance.DecrementEnemy();
        StartCoroutine(DestroyThis());
        
    }
    public override void MeleeAttack() {
        base.MeleeAttack();
        if (isSword)
            playSwordSound();
        else
            playPunchSound();
    }
    private IEnumerator DestroyThis() {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);

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

    public bool InMeleeRange {
        get {
            if (Target != null)
            {
                return Vector2.Distance(transform.position, Target.transform.position) <= 0.5;
            }
            return false;
            }
        
    }
    public bool InShootRange
    {
        get
        {
            if (Target != null)
            {
                return Vector2.Distance(transform.position, Target.transform.position) <= 5;
            }
            return false;
        }

    }



}

