using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySight : MonoBehaviour
{
    [SerializeField]
    private EnemyScript enemy;
    [SerializeField]
    private Collider2D Bullets, PlayerPunch, PlayerSword;

    private void Start()
    {
        Physics2D.IgnoreCollision(Bullets, GetComponent<Collider2D>());
        Physics2D.IgnoreCollision(PlayerPunch, GetComponent<Collider2D>());
        Physics2D.IgnoreCollision(PlayerSword, GetComponent<Collider2D>());
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") {
            enemy.Target = collision.gameObject;
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player") {
            enemy.Target = null;
        }
    }
}
