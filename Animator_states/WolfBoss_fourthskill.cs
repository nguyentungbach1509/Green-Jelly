using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfBoss_fourthskill : StateMachineBehaviour
{
    Transform playerTransform;
    WolfBoss wolfBoss;
    Rigidbody2D rb;
    public float bossSpeed = 2f;
    int no_direct;
    Vector3 newPosition;
    Vector3 tempPosition;
    bool appearAtLocation;
    

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        wolfBoss = animator.GetComponent<WolfBoss>();
        rb = animator.GetComponent<Rigidbody2D>();
        tempPosition = rb.position;
        appearAtLocation = false;

        if(GameObject.FindGameObjectWithTag("Player") != null) {
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
            Vector2 target = new Vector2(playerTransform.position.x, playerTransform.position.y);
            newPosition = Vector3.MoveTowards(rb.position, target, bossSpeed * Time.fixedDeltaTime);
            Vector2 enemyDirection = (newPosition - rb.transform.position);
            if(Mathf.Abs(enemyDirection.x) > Mathf.Abs(enemyDirection.y)) {
                if(enemyDirection.x > 0) {
                    animator.SetFloat("Horizontal", Vector2.right.x);
                    animator.SetFloat("Vertical", Vector2.right.y);
                    no_direct = 0;
                    wolfBoss.storeVector(Vector2.right);
                }
                else if(enemyDirection.x < 0) {
                    animator.SetFloat("Horizontal", Vector2.left.x);
                    animator.SetFloat("Vertical", Vector2.left.y);
                    no_direct = 1;
                    wolfBoss.storeVector(Vector2.left);
                }
            }
            else if(Mathf.Abs(enemyDirection.x) < Mathf.Abs(enemyDirection.y)) {
                if(enemyDirection.y > 0) {
                    animator.SetFloat("Horizontal", Vector2.up.x);
                    animator.SetFloat("Vertical", Vector2.up.y);
                    no_direct = 2;
                    wolfBoss.storeVector(Vector2.up);
                }
                else if(enemyDirection.y < 0) {
                    animator.SetFloat("Horizontal", Vector2.down.x);
                    animator.SetFloat("Vertical", Vector2.down.y);
                    no_direct = 3;
                    wolfBoss.storeVector(Vector2.down);
                }
            }
        }
        
        
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        switch(no_direct) {
            case 0:
                if(!wolfBoss.getDuringTrackCooldown() && wolfBoss.getTrackTarget() != Vector2.zero) {
                    if(appearAtLocation == false) {
                        wolfBoss.transform.position = new Vector3(18f, rb.position.y, 0);
                        appearAtLocation = true;
                    }
                    newPosition = Vector3.MoveTowards(rb.position, wolfBoss.getTrackTarget(), bossSpeed * Time.fixedDeltaTime);
                    animator.SetFloat("Horizontal", Vector2.left.x);
                    animator.SetFloat("Vertical", Vector2.left.y);
                    wolfBoss.storeVector(Vector2.left);
                }
                break;
            case 1:
                if(!wolfBoss.getDuringTrackCooldown() && wolfBoss.getTrackTarget() != Vector2.zero) {
                    if(appearAtLocation == false) {
                        wolfBoss.transform.position = new Vector3(-14f, rb.position.y, 0);
                        appearAtLocation = true;
                    }
                    
                    newPosition = Vector3.MoveTowards(rb.position, wolfBoss.getTrackTarget(), bossSpeed * Time.fixedDeltaTime);
                    animator.SetFloat("Horizontal", Vector2.right.x);
                    animator.SetFloat("Vertical", Vector2.right.y);
                    wolfBoss.storeVector(Vector2.right);
                }
                break;
            case 2:
                if(!wolfBoss.getDuringTrackCooldown() && wolfBoss.getTrackTarget() != Vector2.zero) {
                    if(appearAtLocation == false) {
                        wolfBoss.transform.position = new Vector3(rb.position.x, -1.25f, 0);
                        appearAtLocation = true;
                    }
                    newPosition = Vector3.MoveTowards(rb.position, wolfBoss.getTrackTarget(), bossSpeed * Time.fixedDeltaTime);
                    animator.SetFloat("Horizontal", Vector2.down.x);
                    animator.SetFloat("Vertical", Vector2.down.y);
                    wolfBoss.storeVector(Vector2.down);
                }
                break;
            case 3:
                if(!wolfBoss.getDuringTrackCooldown() && wolfBoss.getTrackTarget() != Vector2.zero) {
                     if(appearAtLocation == false) {
                        wolfBoss.transform.position = new Vector3(rb.position.x, -21f, 0);
                        appearAtLocation = true;
                    }
                    newPosition = Vector3.MoveTowards(rb.position, wolfBoss.getTrackTarget(), bossSpeed * Time.fixedDeltaTime);
                    animator.SetFloat("Horizontal", Vector2.up.x);
                    animator.SetFloat("Vertical", Vector2.up.y);
                    wolfBoss.storeVector(Vector2.up);
                }
                break;
        }


        if(appearAtLocation == true){
            if(wolfBoss.getTouchPlayer()) {
                wolfBoss.ResetTrackTarget();
                animator.SetTrigger("Idle");
            }
            else {
                rb.MovePosition(newPosition);
                if(rb.position.x == wolfBoss.getTrackTarget().x && rb.position.y == wolfBoss.getTrackTarget().y) {
                    wolfBoss.ResetTrackTarget();
                    animator.SetTrigger("Idle");
                }
            }
           
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Idle");
    }

  
}
