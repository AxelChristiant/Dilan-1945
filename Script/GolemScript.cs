using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GolemScript : MonoBehaviour
{
    [SerializeField]
    private GameObject rock;
    private Animator myAnimator;
    [SerializeField]
    private Sprite[] HeartSprites;

    private static GolemScript instance;
    private int health;
    private float movementSpeed;
    private bool facingRight;
    private bool takingDamage;
    private float timer;

    [SerializeField]
    private Transform PositionA;

    [SerializeField]
    private GameObject BodyCrusher;

    [SerializeField]
    private Transform throwPosition;

    private bool OnTransition;

    [SerializeField]
    private Transform PositionB;

    [SerializeField]
    private Transform TargetPosition;
    [SerializeField]
    private Image HealthUI;



    public static GolemScript Instance {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<GolemScript>();
            }
            return instance;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        facingRight = false;
        takingDamage = false;
        myAnimator = GetComponent<Animator>();
        health = 10;
        timer = 0;
        movementSpeed = 3;
        OnTransition = true;
        BodyCrusher.SetActive(false);


    }

    // Update is called once per frame
    void Update()
    {

        healthCheck();
        



        if (!IsDead)
        {
            timer += Time.deltaTime;
            if (timer >= 0 && timer < 4 && OnTransition)
            {
                idle();

            }
            else if (timer >= 4 && timer < 15)
            {
                Attack();
                OnTransition = false;
            }
            else if (!OnTransition)
            {
                Move();
                timer = 0;
            }

        }

        
    }
    public Vector2 GetDirection()
    {
        return facingRight ? Vector2.right : Vector2.left;
    }

    public void Attack() {
        myAnimator.SetTrigger("attack");
        takingDamage = false;

    }
    public void Throw() {
        if (facingRight)
        {
            GameObject tmp = (GameObject)Instantiate(rock, throwPosition.position, Quaternion.identity);
            tmp.GetComponent<Rock>().Initialize(TargetPosition);

        }
        else
        {
            GameObject tmp = (GameObject)Instantiate(rock, throwPosition.position, Quaternion.identity);
            tmp.GetComponent<Rock>().Initialize( TargetPosition);

        }

    }

    private void Move()
    {
        BodyCrusher.SetActive(true);
        if ((GetDirection().x > 0 && transform.position.x < PositionA.position.x) || (GetDirection().x < 0 && transform.position.x > PositionB.position.x))
        {

            myAnimator.SetFloat("speed", 1);
            transform.Translate(GetDirection() * (movementSpeed * Time.deltaTime));
        }
        else {
            ChangeDirection();
            myAnimator.SetFloat("speed", 0);
            OnTransition = true;
        }

    }
    private void idle() {
        myAnimator.SetFloat("speed", 0);
        BodyCrusher.SetActive(false);
        takingDamage = true;

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
            
            myAnimator.SetFloat("speed", 0);
           

        }
        else {
            myAnimator.SetTrigger("die");
            LevelManager.Instance.DecrementEnemy();
            yield return null;
        }
       
    }

        public void OnTriggerEnter2D(Collider2D collision)
    {
        if (takingDamage) 
        StartCoroutine(TakeDamage((string)collision.tag));
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        
    }
    public bool IsDead
    {
        get
        {
            return health <= 0;
        }
    }
    public void ChangeDirection()
    {
        facingRight = !facingRight;
        transform.localScale = new Vector3(transform.localScale.x * -1, 1, 1);
    }

    private void healthCheck() {

        if (health <= 0)
        {
            HealthUI.sprite = HeartSprites[0];
        }
        else
        {

            HealthUI.sprite = HeartSprites[health];
        }
    }
}
