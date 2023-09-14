using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBoss_Skill2 : StateMachineBehaviour
{
    Transform playerTransform;
    FinalBoss finalBoss;
    Rigidbody2D rb;
    public float bossSpeed = 2f;
    float difference;
    float startCoor;
    int numberOfBeams;
    List<int[]> beginPositions = new List<int[]>();
   

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        finalBoss = animator.GetComponent<FinalBoss>();
        rb = animator.GetComponent<Rigidbody2D>();
        numberOfBeams = 0;
        while(beginPositions.Count < 3) {
            int key = Mathf.FloorToInt(Random.Range(0, 3));
            int value = Mathf.FloorToInt(Random.Range(0, 2));

            if(beginPositions.Count == 0) {
                beginPositions.Add(new int[]{key, value});
            }
            else {
                int coutnDuplicate = 0;
                for(int i = 0 ; i < beginPositions.Count; i++) {
                    if(key == beginPositions[i][0]) {
                        coutnDuplicate ++;
                    }
                }

                if(coutnDuplicate == 0) {
                    beginPositions.Add(new int[]{key, value});
                }
            }

        }
        
        animator.SetFloat("Horizontal", Vector2.down.x);
        animator.SetFloat("Vertical", Vector2.down.y);
            
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {   

        if(beginPositions.Count > 0) {
            if(numberOfBeams == 0 && finalBoss.duringSkill2Cooldown == false) {
                if(beginPositions[0][0] == 0 || beginPositions[0][0] == 1){
                    numberOfBeams = 6;
                    switch(beginPositions[0][1]) {
                        case 0:
                            startCoor = 2f;
                            difference = 3.5f;
                            break;
                        case 1:
                            startCoor = 20f;
                            difference = -3.5f;
                            break;
                    }
                }
                else {
                    numberOfBeams = 10;
                    switch(beginPositions[0][1]) {
                        case 0:
                            startCoor = -77f;
                            difference = 3.5f;
                            break;
                        case 1:
                            startCoor = -44f;
                            difference = -3.5f;
                            break;
                    }
                }

                finalBoss.startSkill2();
            }
            else {
                if(numberOfBeams > 0 && finalBoss.duringBeamCooldown == false) {
                    numberOfBeams -=1;
                    finalBoss.startBeam();
                    finalBoss.SpawnBeam(startCoor, beginPositions[0][0]);
                    startCoor += difference;
                    if(numberOfBeams == 0){
                        beginPositions.RemoveAt(0);
                    }
                }
            }
        }
        else {
            animator.SetTrigger("Idle");
        }
            
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Idle");
    }
}
