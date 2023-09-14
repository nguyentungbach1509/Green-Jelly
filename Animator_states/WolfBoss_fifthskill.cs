using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfBoss_fifthskill : StateMachineBehaviour
{
    Transform playerTransform;
    WolfBoss wolfBoss;
    Rigidbody2D rb;
    public float bossSpeed = 2f;
    

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        wolfBoss = animator.GetComponent<WolfBoss>();
        rb = animator.GetComponent<Rigidbody2D>();

        wolfBoss.UpdateSlashPoint("up", new Vector2(rb.position.x, 3f));
        wolfBoss.UpdateSlashPoint("down", new Vector2(rb.position.x, -30f));
        wolfBoss.UpdateSlashPoint("left", new Vector2(-20f, rb.position.y));
        wolfBoss.UpdateSlashPoint("right", new Vector2(24f, rb.position.y));

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
