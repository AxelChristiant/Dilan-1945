using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionTrigger : MonoBehaviour
{
    private CapsuleCollider2D playerCollider;

    [SerializeField]    
    private BoxCollider2D platformCollider;

    [SerializeField]
    private BoxCollider2D platformTrigger;

    void Start()
    {
        playerCollider = GameObject.Find("Player").GetComponent<CapsuleCollider2D>();
        Physics2D.IgnoreCollision(platformCollider, platformTrigger, true);
        Physics2D.IgnoreCollision(platformCollider, playerCollider, false);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player") {
            Physics2D.IgnoreCollision(platformCollider, playerCollider, true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        
        if (collision.gameObject.name == "Player") {
            Physics2D.IgnoreCollision(platformCollider, playerCollider, false);
        }
    }
}
