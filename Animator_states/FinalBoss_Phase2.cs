using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBoss_Phase2 : StateMachineBehaviour
{
    Transform playerTransform;
    FinalBoss finalBoss;
    Rigidbody2D rb;
    public float bossSpeed = 2f;
    public float attackRange = 3f;
    public float minRange = 5f;

    int randomState;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(GameObject.FindGameObjectWithTag("Player") != null) {
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        }
        
        randomState = Mathf.FloorToInt(Random.Range(0,2));
        finalBoss = animator.GetComponent<FinalBoss>();
        rb = animator.GetComponent<Rigidbody2D>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(playerTransform != null) {
            if(finalBoss.duringSkill2Phase2Cooldown == false){
                switch(randomState) {
                    case 0:
                        animator.SetTrigger("Skill2");
                        break;
                    case 1:
                        animator.SetTrigger("Skill3");
                        break;
                }
            }
            else {
                finalBoss.startSkill3();
                animator.SetTrigger("Skill3");
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       animator.ResetTrigger("Skill2");
       animator.ResetTrigger("Skill3");

    }
}
