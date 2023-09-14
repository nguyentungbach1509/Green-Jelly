using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kamefx_shooting : StateMachineBehaviour
{   
    Kamefx kamefx;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        kamefx = animator.GetComponent<Kamefx>();

        if(kamefx.direction == Vector2.right) {
            kamefx.transform.position = new Vector2(kamefx.transform.position.x + 1.792f, kamefx.transform.position.y - .42f);
        }
        else if(kamefx.direction == Vector2.left) {
            kamefx.transform.position = new Vector2(kamefx.transform.position.x - 1.792f, kamefx.transform.position.y - .42f);
        }
        else if(kamefx.direction == Vector2.down) {
            kamefx.transform.position = new Vector2(kamefx.transform.position.x, kamefx.transform.position.y - 0.5f);
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
