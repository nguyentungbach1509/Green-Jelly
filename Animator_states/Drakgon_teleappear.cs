using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drakgon_teleappear : StateMachineBehaviour
{
    int randomState;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        randomState = Mathf.FloorToInt(Random.Range(0, 2));
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
        switch(randomState) {
            case 0:
                animator.SetTrigger("First Skill Update");
                break;
            case 1:
                animator.SetTrigger("Second Skill");
                break;
        }
        
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("First Skill Update");
        animator.ResetTrigger("Second Skill");
    }
}
