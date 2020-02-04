using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WerewolfScript : MonoBehaviour
{
    [SerializeField]
    private Collider2D ClawCollider;
    [SerializeField]
    private Transform PositionA, PositionB;
    private Animator myAnimator;
    [SerializeField]
    private Sprite[] HeartSprites;
    [SerializeField]
    private Sprite[] ArmorSprites;
    private static WerewolfScript instance;
    private int health, armor;
    private float movementSpeed;
    private bool facingRight;
    [SerializeField]
    private List<string> damageSources;

    private float timer;
    [SerializeField]
    private GameObject Target;
    [SerializeField]
    private GameObject RunCrusher;
    [SerializeField]
    private Image HealthUI;
    [SerializeField]
    private Image ArmorUI;
    private Rigidbody2D myRigidBody;
    private float TransitionTimer;
    private bool TakingDamage;
    private bool attack;
    public bool HowlTime, RunTime, AttackTime;
    public int RunToken, AttackToken;
    public bool OnTransition;
    private bool benerGaksih;

    public static WerewolfScript Instance {
        get {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<WerewolfScript>();
            }
            return instance;

        }
    }
    public Rigidbody2D MyRigidBody {
        get {
            if (myRigidBody == null) {
                myRigidBody = GetComponent<Rigidbody2D>();
            }
            return myRigidBody;
        }
    }

    private bool TargetInface
    {
        get
        {
            return (GetDirection().x > 0 && transform.position.x - Target.transform.position.x < 0) || (GetDirection().x < 0 && transform.position.x - Target.transform.position.x > 0);
        }
    }

    private bool InMeleeRange
    {
        get
        {
            return Vector2.Distance(transform.position, Target.transform.position) <= 2;
        }

    }

    public Vector2 GetDirection()
    {
        return facingRight ? Vector2.right : Vector2.left;
    }

    public bool IsDead
    {
        get
        {
            return health <= 0;
        }
    }
    private bool ArmorIsBroken{
        get {

            return armor <= 0;
        }
}
    // Start is called before the first frame update
    void Start()
    {
        myAnimator = GetComponent<Animator>();
        myRigidBody = GetComponent<Rigidbody2D>();
        movementSpeed = 7;
        timer = 0;
        TransitionTimer = 0;
        RunCrusher.SetActive(false);
        health = 20;
        TakingDamage = false;
        facingRight = false;
        attack = false;
        StartState();
        Physics2D.IgnoreCollision(Target.GetComponent<Collider2D>(), GetComponent<Collider2D>());

       
    }
    private void FixedUpdate()
    {


    }

    // Update is called once per frame
    void Update()
    {


        transform.position = new Vector2(Mathf.Clamp(transform.position.x, PositionB.position.x, PositionA.position.x), transform.position.y);

        healthCheck();
        armorCheck();

        if (!IsDead && !ArmorIsBroken)
        {
            if (HowlTime)
            {
                Howl();

            }
            else if (RunTime)
            {
                RunCrusher.SetActive(true);
                Run();
            }
            else if (AttackTime)
            {

                if (InMeleeRange && TargetInface)
                {
                    OnTransition = false;
                    SlashAttack();


                }
                else
                {

                    if (OnTransition)
                    {
                        ChaseTheTarget();

                    }
                }
            }
            else if (!AttackTime)
            {

                HowlTime = true;
                RunToken = 5;
                AttackToken = 3;

            }

        }
        else if (!IsDead && ArmorIsBroken) {

            myAnimator.SetFloat("speed", 0);
            myAnimator.ResetTrigger("attack");
            myAnimator.ResetTrigger("howl");
            myAnimator.SetTrigger("hurt");
            timer += Time.deltaTime;
  

            if (timer >= 3) {
                StartState();
            }

        }

    }

    private void Run() {
        if ((GetDirection().x > 0 && transform.position.x < PositionA.position.x) || (GetDirection().x < 0 && transform.position.x > PositionB.position.x))
        {
            myAnimator.SetFloat("speed", 1);
            myRigidBody.velocity = GetDirection() * movementSpeed;
          
        }
        else
        {
            myRigidBody.velocity = Vector2.zero;
            RunToken -= 1;
            myAnimator.SetFloat("speed", 0);
            ChangeDirection();
            if (RunToken <= 0)
            {
                RunTime = false;
                RunCrusher.SetActive(false);
                AttackTime = true;
            }
           
           
        }
    }
    private void Howl() {
        myAnimator.SetTrigger("howl");
        myRigidBody.velocity = Vector2.zero;
        myAnimator.SetFloat("speed", 0);
       

    }

    private void ChaseTheTarget() {

        myAnimator.ResetTrigger("attack");
        if (TargetInface&&!InMeleeRange)
        {

            myAnimator.SetFloat("speed", 1);
            myRigidBody.velocity = GetDirection() * movementSpeed;
        } else if (InMeleeRange&&TargetInface) {
            myAnimator.SetFloat("speed", 0);
            myRigidBody.velocity = Vector2.zero;
            OnTransition = false;



        }
        else
        {

            myAnimator.SetFloat("speed", 0);
            myRigidBody.velocity = Vector2.zero;
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

    public void Dash() {
        myRigidBody.AddForce(GetDirection()*100);
    }

    private void SlashAttack()
    {
        myAnimator.SetFloat("speed", 0);
        
        myAnimator.SetTrigger("attack");

    }
    public void OnTriggerEnter2D(Collider2D collision)
    {

        if (!IsDead)
        {
            if (damageSources.Contains(collision.tag))
            {

                if (ArmorIsBroken)
                {

                    StartCoroutine(TakeDamage((string)collision.tag));
                }
                else
                {
                    armor -= 1;
                }
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {


    }
    public void MeleeAttack()
    {

        attack = !attack;
        ClawCollider.enabled = attack;
    }
    private void StartState() {
        HowlTime = true;
        RunTime = false;
        AttackTime = false;
        RunToken = 5;
        AttackToken = 3;
        OnTransition = true;
        armor = 10;
        timer = 0;

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

    private void armorCheck()
    {

        if (armor <= 0)
        {
            ArmorUI.sprite = ArmorSprites[0];
        }
        else
        {

            ArmorUI.sprite = ArmorSprites[armor];
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


           
            health -= damage-1;
        if (IsDead)
        {
            myAnimator.SetFloat("speed", 0);
            myAnimator.ResetTrigger("attack");
            myAnimator.ResetTrigger("howl");
            myAnimator.ResetTrigger("hurt");
            myAnimator.SetTrigger("death");
            LevelManager.Instance.DecrementEnemy();
            yield return null;
            
        }
      

    }
}
