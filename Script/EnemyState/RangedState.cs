using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedState : IEnemyState
{

    private EnemyScript enemy;
    private float shootTimer;
    private float shootCoolDown=0;
    private bool canShoot;
    public void Enter(EnemyScript enemy)
    {
        this.enemy = enemy;
        canShoot = false;
    }

    public void Execute()
    {

        if (enemy.InMeleeRange && !enemy.IsGunned)
        {
            enemy.ChangeState(new MeleeState());
        }
        else if (enemy.Target != null && enemy.IsGunned&&enemy.InShootRange)
        {

            Shoot();
        }
        else if (enemy.Target != null && !enemy.IsGunned) {
            enemy.Move();
        }
        else
        {
            enemy.Target = null;
            enemy.ChangeState(new IdleState());
        }
    }

    private void Shoot() {
        shootTimer += Time.deltaTime;
        enemy.myAnimator.SetFloat("speed", 0);
        if (shootTimer >= shootCoolDown) {
            canShoot = true;
            shootTimer = 0;
        }
        if (canShoot) {
            canShoot = false;
            enemy.myAnimator.SetTrigger("attack");
        }

    }
    public void Exit()
    {
        
    }

    public void OnTriggerEnter(Collider2D collision)
    {
        
    }

}
