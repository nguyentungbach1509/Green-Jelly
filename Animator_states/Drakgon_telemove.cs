using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drakgon_telemove : StateMachineBehaviour
{
    Transform playerTransform;
    Drakgon drakgon;
    Rigidbody2D rb;
    public float bossSpeed = 5f;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

       rb = animator.GetComponent<Rigidbody2D>();
       drakgon = animator.GetComponent<Drakgon>();
       
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Vector2 target = new Vector2(7.03f, 39.63f);
        Vector3 newPosition = Vector3.MoveTowards(rb.position, target, bossSpeed * Time.fixedDeltaTime);

        rb.MovePosition(newPosition);

        if(Vector2.Distance(target, rb.position) <= 0) {
            animator.SetTrigger("Teleport");
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        drakgon.teleport = false;
        animator.ResetTrigger("Teleport");
    }

}
