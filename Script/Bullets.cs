using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]

public class Bullets : MonoBehaviour
{
    [SerializeField]
    private float speed;

    private Rigidbody2D myRigidBody;

    private Vector2 direction;
    private bool IsPlayer;

    
    
    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        speed = 10;
      
    }
  void FixedUpdate()
    {
        myRigidBody.velocity = direction * speed;
        
    }

    public void Initialize(Vector2 direction,bool IsPlayer)
    {

        this.direction = direction;
        this.IsPlayer = IsPlayer;
        if (!IsPlayer)
            transform.gameObject.tag = "Enemy-Bullets";
        if (direction == Vector2.left) {
            transform.localScale = new Vector3(transform.localScale.x * -1, 1, 1);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (IsPlayer)
        {
            if (collision.CompareTag("EnemyGunned") || collision.CompareTag("EnemyMelee") || collision.CompareTag("Golem") || collision.CompareTag("Ninja") || collision.CompareTag("Werewolf"))
            {
                Destroy(gameObject);
            }

        }
        else {
            if (collision.CompareTag("Player")) {
                Destroy(gameObject);
            }

        }
        
    }
}
