using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfBoss_idle_phase_2 : StateMachineBehaviour
{
    Transform playerTransform;
    WolfBoss wolfBoss;
    Rigidbody2D rb;
    public float bossSpeed = 2f;
    public float attackRange = 3.5f;
    int randomState;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(GameObject.FindGameObjectWithTag("Player") != null) {
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        }
        
        randomState = Mathf.FloorToInt(Random.Range(0, 3));
        wolfBoss = animator.GetComponent<WolfBoss>();
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

            
            if(Vector2.Distance(playerTransform.position, rb.position) <= attackRange) {
                if(wolfBoss.duringCooldown == false && wolfBoss.duringCastSkillCooldown == false){
                    switch(randomState) {
                        case 0:
                            wolfBoss.startAttack();
                            animator.SetTrigger("Attack");
                            break;
                        case 1:
                            wolfBoss.startCastSkill();
                            animator.SetTrigger("Rolling");
                            break;
                        case 2:
                            animator.SetTrigger("Skill5");
                            break;
                    }
                }
                else {
                    if(wolfBoss.duringCooldown == false) {
                        wolfBoss.startAttack();
                        animator.SetTrigger("Attack");
                    }
                    else if(wolfBoss.duringCastSkillCooldown == false) {
                        wolfBoss.startCastSkill();
                        animator.SetTrigger("Rolling");
                    }
                    else {
                        animator.SetTrigger("Skill5");
                    }
                }
            }
            else {
        
                switch(randomState){
                    case 0:
                        animator.SetTrigger("Move");
                        break;
                    case 1:
                        animator.SetTrigger("Skill3");
                        break;
                    case 2:
                        animator.SetTrigger("Skill5");
                        break;
                }
                
            }
            

           
        }
        
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Attack");
        animator.ResetTrigger("Move");
        animator.ResetTrigger("Skill3");
        animator.ResetTrigger("Skill5");
        animator.ResetTrigger("Rolling");
    }
}
