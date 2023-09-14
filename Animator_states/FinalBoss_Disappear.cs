using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBoss_Disappear : StateMachineBehaviour
{
    Transform playerTransform;
    FinalBoss finalBoss;
    Rigidbody2D rb;
    public float bossSpeed = 2f;
    Ray2D aline;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        finalBoss = animator.GetComponent<FinalBoss>();
        rb = animator.GetComponent<Rigidbody2D>();
        finalBoss.enemyTouchingEnvironment = false;

        if(GameObject.FindGameObjectWithTag("Player") != null) {
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
            Vector2 target = new Vector2(playerTransform.position.x, playerTransform.position.y);
            Vector3 newPosition = Vector3.MoveTowards(rb.position, target, bossSpeed * Time.fixedDeltaTime);
            Vector2 enemyDirection = (newPosition - rb.transform.position);
            if(Mathf.Abs(enemyDirection.x) > Mathf.Abs(enemyDirection.y)) {
                if(enemyDirection.x > 0) {
                    animator.SetFloat("Horizontal", Vector2.right.x);
                    animator.SetFloat("Vertical", Vector2.right.y);
                }
                else if(enemyDirection.x < 0) {
                    animator.SetFloat("Horizontal", Vector2.left.x);
                    animator.SetFloat("Vertical", Vector2.left.y);
                }
            }
            else if(Mathf.Abs(enemyDirection.x) < Mathf.Abs(enemyDirection.y)) {
                if(enemyDirection.y > 0) {
                    animator.SetFloat("Horizontal", Vector2.up.x);
                    animator.SetFloat("Vertical", Vector2.up.y);
                }
                else if(enemyDirection.y < 0) {
                    animator.SetFloat("Horizontal", Vector2.down.x);
                    animator.SetFloat("Vertical", Vector2.down.y);
                }
            }

            aline = new Ray2D(rb.transform.position, enemyDirection);
            target = aline.GetPoint(Vector2.Distance(playerTransform.position, rb.position) + 5f);
            finalBoss.setTeleportPoint(target);
            finalBoss.switchColliderTrigger();
            
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
