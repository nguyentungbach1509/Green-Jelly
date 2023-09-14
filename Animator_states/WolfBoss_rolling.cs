using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfBoss_rolling : StateMachineBehaviour
{
    Transform playerTransform;
    WolfBoss wolfBoss;
    Rigidbody2D rb;
    public float bossSpeed = 2f;
    float yDirect;
    Vector3 tempPosition;
    Vector3 newPosition;
    Vector2 mainDestination;
    bool isJump;
    int no_direct;


    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        wolfBoss = animator.GetComponent<WolfBoss>();
        rb = animator.GetComponent<Rigidbody2D>();
        yDirect = rb.position.y + 5f;
        tempPosition = rb.position;
        isJump = false;

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
                    mainDestination = new Vector2(18f, tempPosition.y);
                }
                else if(enemyDirection.x < 0) {
                    animator.SetFloat("Horizontal", Vector2.left.x);
                    animator.SetFloat("Vertical", Vector2.left.y);
                    no_direct = 1;
                    wolfBoss.storeVector(Vector2.left);
                    mainDestination = new Vector2(-14f, tempPosition.y);
                }
            }
            else if(Mathf.Abs(enemyDirection.x) < Mathf.Abs(enemyDirection.y)) {
                if(enemyDirection.y > 0) {
                    animator.SetFloat("Horizontal", Vector2.up.x);
                    animator.SetFloat("Vertical", Vector2.up.y);
                    no_direct = 2;
                    wolfBoss.storeVector(Vector2.up);
                    mainDestination = new Vector2(tempPosition.x, -1.25f);
                }
                else if(enemyDirection.y < 0) {
                    animator.SetFloat("Horizontal", Vector2.down.x);
                    animator.SetFloat("Vertical", Vector2.down.y);
                    no_direct = 3;
                    wolfBoss.storeVector(Vector2.down);
                    mainDestination = new Vector2(tempPosition.x, -21f);
                }
            }
        }
        
        
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        if(Vector2.Distance(rb.position, mainDestination) <= 0.5f) {
            animator.SetTrigger("OnGround");
        }
        
        if(wolfBoss.getStartToJump()) {
            switch(no_direct) {
                case 0:
                    if(isJump == false) {
                        newPosition = Vector3.MoveTowards(rb.position, new Vector2(((18f + tempPosition.x)/2f), yDirect), bossSpeed * Time.fixedDeltaTime);
                    }
                    else {
                        newPosition = Vector3.MoveTowards(rb.position, new Vector2(18f, tempPosition.y), bossSpeed * Time.fixedDeltaTime);
                    }
                    if(Vector2.Distance(rb.position, new Vector2(((18f + tempPosition.x)/2f), yDirect)) < 1.5f && isJump == false) {
                        isJump = true;
                    }
                    break;
                case 1:
                    if(isJump == false) {
                        newPosition = Vector3.MoveTowards(rb.position, new Vector2(((-14f + tempPosition.x)/2f), yDirect), bossSpeed * Time.fixedDeltaTime);
                    }
                    else {
                        newPosition = Vector3.MoveTowards(rb.position, new Vector2(-14f, tempPosition.y), bossSpeed * Time.fixedDeltaTime);
                    }
                    if(Vector2.Distance(rb.position, new Vector2(((-14f + tempPosition.x)/2f), yDirect)) < 1.5f && isJump == false) {
                        isJump = true;
                    }
                    break;
                case 2:
                    newPosition = Vector3.MoveTowards(rb.position, new Vector2(tempPosition.x, -1.25f), bossSpeed * Time.fixedDeltaTime);
                    break;
                case 3:
                    newPosition = Vector3.MoveTowards(rb.position, new Vector2(tempPosition.x, -21f), bossSpeed * Time.fixedDeltaTime);
                    break;
            }
            

            rb.MovePosition(newPosition);

            
        }
        
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("OnGround");
    }   
}
