﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBehaviour : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponent<CharacterScript>().attack = true;
        if (animator.tag == "Player")
        {
            PlayerScript.Instance.Attack = true;
            if (!PlayerScript.Instance.Jump)
            {
                PlayerScript.Instance.MyRigidbody.velocity = Vector2.zero;
            }
            animator.GetComponent<CharacterScript>().attack = true;
            if (!PlayerScript.Instance.IsGun)
            {
                if (PlayerScript.Instance.IsSword)
                {
                    PlayerScript.Instance.playSwordSound();
                }
                else {
                    PlayerScript.Instance.playPunchSound();
                }
                animator.GetComponent<PlayerScript>().MeleeAttack();
               
            }
        }
        else {
         

        }
        animator.SetFloat("speed", 0);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
   override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
   {
        animator.GetComponent<CharacterScript>().attack = false;
        if (animator.tag=="Player"||animator.tag=="EnemyMelee") {
            animator.GetComponent<CharacterScript>().punch.enabled = false;
            if (animator.tag == "Player") {
                PlayerScript.Instance.Attack = false ;
                if ((PlayerScript.Instance.IsGun || PlayerScript.Instance.IsSword) && PlayerScript.Instance.ResourceIsEmpty) {
                    PlayerScript.Instance.dropWeapon();
                   
                }
            }
        }
       
            animator.ResetTrigger("attack");

    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
