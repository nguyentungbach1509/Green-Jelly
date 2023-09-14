using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBoss_Skill1 : StateMachineBehaviour
{
   Transform playerTransform;
    FinalBoss finalBoss;
    Rigidbody2D rb;
    public float bossSpeed = 2f;
    public float minRange = 5f;


    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(GameObject.FindGameObjectWithTag("Player") != null) {
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        }
        
        finalBoss = animator.GetComponent<FinalBoss>();
        rb = animator.GetComponent<Rigidbody2D>();
        finalBoss.SpawnEnergyObjectFistPhase();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
        if(finalBoss.getDestroyAura()) {
            
            finalBoss.willDestroyAura();
            animator.SetTrigger("Idle");

        }
        
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Idle");
    }
}
