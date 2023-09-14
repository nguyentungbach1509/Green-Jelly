using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfBoss_rolling_onground : StateMachineBehaviour
{
    Transform playerTransform;
    WolfBoss wolfBoss;
    
    


    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        wolfBoss = animator.GetComponent<WolfBoss>();
        animator.SetFloat("Horizontal", wolfBoss.getSaveVector().x);
        animator.SetFloat("Vertical", wolfBoss.getSaveVector().y);
           
        
             
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
       
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }
}
