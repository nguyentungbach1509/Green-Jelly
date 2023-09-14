using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfBoss_thirdskill : StateMachineBehaviour
{
    Transform playerTransform;
    WolfBoss wolfBoss;
    Rigidbody2D rb;
    public float bossSpeed = 2f;
    public float onPointRange = .1f;
    Ray2D aline;
    Vector3 newPosition;
    Vector2 target;
    

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        wolfBoss = animator.GetComponent<WolfBoss>();
        rb = animator.GetComponent<Rigidbody2D>();

        if(GameObject.FindGameObjectWithTag("Player") != null) {
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
            target = new Vector2(playerTransform.position.x, playerTransform.position.y);
            newPosition = Vector3.MoveTowards(rb.position, target, bossSpeed * Time.fixedDeltaTime);
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
            target = aline.GetPoint(Vector2.Distance(playerTransform.position, rb.position) + 3f);
            
        }
        
       
        
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {   

        if(Vector2.Distance(target, rb.position) > onPointRange && !wolfBoss.stateToIdle) {
            newPosition = Vector3.MoveTowards(rb.position, target, bossSpeed * Time.fixedDeltaTime);
            rb.MovePosition(newPosition);
        }

        else {
            wolfBoss.useThirdSkill = false;
            animator.SetTrigger("Idle");
        }

        if((Vector2.Distance(playerTransform.position, rb.position)) <= 2f) {
            wolfBoss.changeWolfCollider(true, 0.15f);
            
        }
        else {
            wolfBoss.changeWolfCollider(false,0.08137854f);

        }
        
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Idle");
    }
}
