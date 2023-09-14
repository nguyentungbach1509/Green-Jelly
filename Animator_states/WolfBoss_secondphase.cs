using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfBoss_secondphase : StateMachineBehaviour
{
    Transform playerTransform;
    WolfBoss wolfBoss;
    int randomState;
    

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(GameObject.FindGameObjectWithTag("Player") != null) {
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        }
        
        wolfBoss = animator.GetComponent<WolfBoss>();
        randomState = Mathf.FloorToInt(Random.Range(0, 2));
        
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(playerTransform != null) {
            
            if(Vector2.Distance(wolfBoss.transform.position, playerTransform.position) <= 2.5f){
                switch(randomState) {
                    case 0:
                        animator.SetTrigger("Skill5");
                        break;
                    case 1:
                        wolfBoss.startCastSkill();
                        animator.SetTrigger("Rolling");
                        break;
                }
            }
            else {
                switch(randomState){
                    case 0:
                        animator.SetTrigger("Skill3");
                        break;
                    case 1:
                        animator.SetTrigger("Skill5");
                        break;
                }  
            }  
        }

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
        animator.ResetTrigger("Skill3");
        animator.ResetTrigger("Rolling");
        animator.ResetTrigger("Skill5");
        
    }
}
