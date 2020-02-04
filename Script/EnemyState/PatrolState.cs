using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : IEnemyState
{

    private float patrolTimer;
    private float patrolDuration=10;
    private EnemyScript enemy;

    public void Enter(EnemyScript enemy)
    {
        this.enemy = enemy;

    }

    public void Execute()
    {
        
        Patrol();
        enemy.Move();
        if (enemy.Target != null) {
            enemy.ChangeState(new RangedState());
        }
    }

    public void Exit()
    {

    }

    public void OnTriggerEnter(Collider2D collision)
    {
        if (collision.tag == "Sword" || collision.tag == "Bullets" || collision.tag == "Melee")
        {
            enemy.Target = PlayerScript.Instance.gameObject;
        }
    }

    private void Patrol()
    {
        enemy.myAnimator.SetFloat("speed", 0);
        patrolTimer += Time.deltaTime;
        if (patrolTimer >= patrolDuration)
        {
            enemy.ChangeState(new IdleState());
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
