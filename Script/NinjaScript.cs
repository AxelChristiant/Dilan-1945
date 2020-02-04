using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NinjaScript : MonoBehaviour
{

    [SerializeField]
    private GameObject kunai;
    [SerializeField]
    private Transform throwPosition;
    private Rigidbody2D MyrigidBody;
    
    public EdgeCollider2D swordCollider;
    [SerializeField]
    private GameObject Target;
    [SerializeField]
    private Transform positionA,positionB;
    [SerializeField]
    private Sprite[] HeartSprites;
    private Animator myAnimator;
    [SerializeField]
    private Image HealthUI;
    private static NinjaScript instance;
    private int health;
    private float movementSpeed;
    [SerializeField]
    private bool facingRight;

    private Collider2D Player;
    private float timer,posA,posB;
    public bool OnPosB;
    private float TransitionTimer;
    public bool OnTransition;








    // Start is called before the first frame update
    void Start()
    {
        movementSpeed = 5;
        health = 15;
        timer = 0;
        TransitionTimer = 0;
        myAnimator = GetComponent<Animator>();
        MyrigidBody = GetComponent<Rigidbody2D>();
        OnPosB = false;
      
        swordCollider.enabled = false;
        Physics2D.IgnoreCollision(Target.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        OnTransition = true;
  



    }


    // Update is called once per frame
    void Update()
    {
        healthCheck();
        transform.position=new Vector2(Mathf.Clamp(transform.position.x, positionA.position.x,positionB.position.x), transform.position.y);
        if (!IsDead)
        {

            if (InMeleeRange&&TargetInface)
            {

                myAnimator.SetFloat("speed", 0);
                MyrigidBody.velocity = Vector2.zero;
                SlashAttack();
            }
            else if (InThrowRange&&!TargetInArea)
            {
                myAnimator.SetFloat("speed", 0);
                MyrigidBody.velocity = Vector2.zero;
                KunaiAttack();

            }
            else {

               if(OnTransition)Move();
                    
            }
        }
        
    }

    private bool InMeleeRange
    {
        get
        {
            return Vector2.Distance(transform.position, Target.transform.position) <= 1;
        }

    }
    private bool InThrowRange {

        get {

            return (Vector2.Distance(transform.position, positionA.position) <= 0.2|| Vector2.Distance(transform.position, positionB.position) <= 0.2) &&!InMeleeRange;
        }
    }
    private bool TargetInArea {
        get {
            return Target.transform.position.x < positionB.position.x && Target.transform.position.x > positionA.position.x;
        }
    }


    private void healthCheck()
    {

        if (health <= 0)
        {
            HealthUI.sprite = HeartSprites[0];
        }
        else
        {

            HealthUI.sprite = HeartSprites[health];
        }
    }

    public bool IsDead
    {
        get
        {
            return health <= 0;
        }
    }

    private void ThrowKunai() {

        if (facingRight)
        {
            GameObject tmp = (GameObject)Instantiate(kunai, throwPosition.position, Quaternion.identity);
            tmp.GetComponent<Kunai>().Initialize(Vector2.right);

        }
        else
        {
            GameObject tmp = (GameObject)Instantiate(kunai, throwPosition.position, Quaternion.identity);
            tmp.GetComponent<Kunai>().Initialize(Vector2.left);

        }
    }
    private void KunaiAttack() {
        timer += Time.deltaTime;
        if (timer >= 2f)
        {
            myAnimator.SetTrigger("KunaiAttack");
            timer = 0;
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {

        if (!IsDead)
        {
            StartCoroutine(TakeDamage((string)collision.tag));
        }
    }
   
    public IEnumerator TakeDamage(string weaponType)
    {


        int damage = 0;

        switch (weaponType)
        {
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
        health -= damage;
        if (!IsDead)
        {

            if (damage > 0)
            {
                myAnimator.SetFloat("speed", 0);
               // myAnimator.SetTrigger("hurt");
            }


        }
        else
        {
            myAnimator.SetFloat("speed", 0);
            myAnimator.ResetTrigger("SlashAttack");
            myAnimator.ResetTrigger("KunaiAttack");
            myAnimator.SetTrigger("die");
            LevelManager.Instance.DecrementEnemy();
            yield return null;
        }

    }

    private void SlashAttack() {

       
        myAnimator.SetTrigger("SlashAttack");
        
    }
    public void MeleeAttack() {

        //nge disable nya di behaviour scriptnya
        swordCollider.enabled = true;
    }
    public Vector2 GetDirection()
    {
        return facingRight ? Vector2.right : Vector2.left;
    }
    private bool TargetInface {
        get {
            return (GetDirection().x > 0 && transform.position.x - Target.transform.position.x < 0) || (GetDirection().x < 0 && transform.position.x - Target.transform.position.x > 0);
        }
    }

    private void Move()
    {

        
        if (TargetInface)
        {
            
            myAnimator.SetFloat("speed", 1);
            MyrigidBody.velocity = GetDirection() * movementSpeed;
        }
        else
        {

            myAnimator.SetFloat("speed", 0);
            MyrigidBody.velocity = Vector2.zero;
            TransitionTimer += Time.deltaTime;
            
            if (TransitionTimer >= 0.5f)
            {

                ChangeDirection();
                TransitionTimer = 0;
                

            }
            
           
        }
    }



    public void ChangeDirection()
    {
        facingRight = !facingRight;
        transform.localScale = new Vector3(transform.localScale.x * -1, 1, 1);
    }



}
