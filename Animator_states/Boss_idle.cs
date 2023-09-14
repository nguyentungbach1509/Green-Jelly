using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
All script in Animator_states folders are used to controll animation state and AI of all bosses
with different states and skills
*/
public class Boss_idle : StateMachineBehaviour
{
    Transform playerTransform;
    Drakgon drakgon;
    Rigidbody2D rb;
    public float bossSpeed = 2f;
    public float attackRange = 3f;

    int randomState;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(GameObject.FindGameObjectWithTag("Player") != null) {
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        }
        
        randomState = Mathf.FloorToInt(Random.Range(0, 3));
        drakgon = animator.GetComponent<Drakgon>();
        rb = animator.GetComponent<Rigidbody2D>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       if(playerTransform != null) {
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

            if(drakgon.teleport) {
                
                animator.SetTrigger("Second Phase");
            }
            else {

                if(Vector2.Distance(playerTransform.position, rb.position) <= attackRange && drakgon.duringCooldown == false) {

                    drakgon.startAttack();
                    animator.SetTrigger("Attack");

                }
                else {

                    if(drakgon.currentHealth > (drakgon.enemyHealth / 2)) {
                        if(drakgon.duringShootingCooldown == false) {
                            drakgon.startShooting();
                            animator.SetTrigger("Shooting");
                        }
                        else {
                            animator.SetTrigger("Move");
                        }
                    }
                    else {
                       
                        switch(randomState) {
                            case 0:
                                animator.SetTrigger("First Skill Update");
                                break;
                            case 1:
                                animator.SetTrigger("Second Skill");
                                break;
                            default:
                                animator.SetTrigger("Move");
                                break;
                        }
                            
                    }
                    
                }
            }

            
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       animator.ResetTrigger("Attack");
       animator.ResetTrigger("Move");
       animator.ResetTrigger("Shooting");
       animator.ResetTrigger("Second Phase");
       animator.ResetTrigger("First Skill Update");
       animator.ResetTrigger("Second Skill");

    }

    
}
