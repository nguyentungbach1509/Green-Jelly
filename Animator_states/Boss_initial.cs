using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_initial : StateMachineBehaviour
{

    private int random;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        random = Random.Range(0,2);
        random = 1;

        switch(random) {
            case 0:
                animator.SetTrigger("Idle");
                break;

            default:
                animator.SetTrigger("Move");
                break;
        }
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
