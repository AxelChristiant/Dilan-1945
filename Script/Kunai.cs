using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kunai : MonoBehaviour
{

   
    private float speed;

    private Rigidbody2D myRigidBody;


    private Vector2 direction;
    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        speed = 5;
        
    }
    void FixedUpdate()
    {
        myRigidBody.velocity = new Vector2(direction.x, direction.y) * speed;
    }
    public void Initialize(Vector2 Direction)
    {
        direction = Direction;   
        if (Direction == Vector2.left)
        {
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
        if (collision.CompareTag("Player"))
            Destroy(gameObject);
    }
}
