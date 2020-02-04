using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeState : IEnemyState
{
    private EnemyScript enemy;
    private float attackTimer;
    private float attackCoolDown = 1;
    private bool canAttack = true;

    public void Enter(EnemyScript enemy)
    {
        this.enemy = enemy;
    }

    public void Execute()
    {

        Attack();
        if (enemy.Target == null) {
            enemy.ChangeState(new IdleState());
        }
        
    }

    public void Exit()
    {
       
    }

    public void OnTriggerEnter(Collider2D collision)
    {
        
    }

    private void Attack() {
        attackTimer += Time.deltaTime;
        if (attackTimer >= attackCoolDown) {
            canAttack = true;
            attackTimer = 0;
            }
        if (canAttack) {
            canAttack = false;
            enemy.myAnimator.SetFloat("speed", 0);
            enemy.myAnimator.SetTrigger("attack");
        }
    }
}
