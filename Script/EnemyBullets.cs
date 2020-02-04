using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullets : MonoBehaviour
{
    
    private float speed;

    private Rigidbody2D myRigidBody;

    private Vector2 direction;

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

    public void Initialize(Vector2 direction)
    {

        this.direction = direction;
        if (direction == Vector2.left)
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
}
