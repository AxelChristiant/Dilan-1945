using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IEnemyState
{

    private EnemyScript enemy;

    private float idleTimer;

    private float idleDuration=1;
    public void Enter(EnemyScript enemy)
    {
        this.enemy = enemy;
    }


    public void Execute()
    {
        
        Idle();
        if (enemy.Target != null)
        {
            enemy.ChangeState(new PatrolState());
        }
    }

    public void Exit()
    {
      
    }

    public void OnTriggerEnter(Collider2D collision)
    {
        if (collision.tag == "Sword"||collision.tag=="Bullets"||collision.tag=="Melee") {
            enemy.Target = PlayerScript.Instance.gameObject;
        }
    }

    private void Idle() {
        enemy.myAnimator.SetFloat("speed", 0);
        idleTimer += Time.deltaTime;
        if (idleTimer >= idleDuration) {
            enemy.ChangeState(new PatrolState());
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
