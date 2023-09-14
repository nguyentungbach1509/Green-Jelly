using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/*
Controll movement, states and stats of Cobra enemy
*/
public class BasicCobra : Enemy
{
    public Transform playerTransform;
    public float chasingRange;
    private float[] attackRange = new float[]{1.25f,1.75f};
    public Vector3[] initialPosition;
    public float rangePoint;
    private Animator animator;
    private Rigidbody2D rb;
    public int currentPosition;
    private Vector3 goalPosition;
    private PlayerMovement playerMovement;
    public float attackCoolDown;
    private float attackTimer;

    private bool duringCooldown = false;
    private Vector2 beginPosition;
    private int random;
    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentState = EnemyStates.enemymove;
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        animator = GetComponent<Animator>();
        beginPosition = new Vector2(transform.position.x, transform.position.y);
        random = Mathf.FloorToInt(Random.Range(0,2));
    }

    // Update is called once per frame
    void Update() {

        if(currentHealth <= 0) {
            animator.SetBool("Deadth", true);
            if(!playerMovement.playerBeKilled){
                playerMovement.setState(0);
            }
        }

        calculateCooldown();
        animator.SetBool("Hurt", (currentState == EnemyStates.enemystunning));

    }

    void FixedUpdate()
    {
        if(currentHealth > 0 && currentState != EnemyStates.enemydie) {
            DistanceChecking();
        }
       
    }



    public virtual void DistanceChecking() {


        float distance =  0;
        

        if(!playerMovement.playerBeKilled) {
            distance = Vector3.Distance(transform.position,playerTransform.position);
            
        }
       
        if(distance <= chasingRange && distance > attackRange[random] && !playerMovement.playerBeKilled) {
            
            
            if(currentState == EnemyStates.enemymove && currentState != EnemyStates.enemystunning && currentState != EnemyStates.enemyattack){

                Vector3 temp = Vector3.MoveTowards(transform.position, playerTransform.position, enemySpeed * Time.deltaTime);

                AnimationMovement(temp - transform.position);
                rb.MovePosition(temp); 
                StateChanging(EnemyStates.enemymove);
            
            }
        
        }
        else if((distance > chasingRange) || playerMovement.playerBeKilled) {
            
            float distanceToGoal = Vector3.Distance(transform.position, initialPosition[currentPosition]);
            
            if(distanceToGoal > rangePoint) {
                
                Vector3 temp = Vector3.MoveTowards(transform.position, initialPosition[currentPosition], enemySpeed * Time.deltaTime);
                AnimationMovement(temp - transform.position);
                rb.MovePosition(temp); 
                
            }
            else {
                PositionChanging();
            }

            StateChanging(EnemyStates.enemymove);
            
        }
        else if(distance <= attackRange[random] && !playerMovement.playerBeKilled){
            if(currentState != EnemyStates.enemystunning && duringCooldown == false) {
                duringCooldown = true;
                attackTimer = 1;
                AttackPlayer(random);
                random = Mathf.FloorToInt(Random.Range(0,2));
            }
        }
    }


    private void AttackPlayer(int type) {
        switch(type) {
            case 0:
                StateChanging(EnemyStates.enemyattack);
                animator.SetTrigger("Attack");
                break;

            case 1:
                StateChanging(EnemyStates.enemyattack);
                animator.SetTrigger("Poison");
                break;
        }
        StateChanging(EnemyStates.enemymove);
    }

    private void calculateCooldown() {
        if(duringCooldown) {

            attackTimer -= 1 / attackCoolDown * Time.deltaTime;

            if(attackTimer <= 0) {
                duringCooldown = false;
                attackTimer = 0;
            }
        }
    }

   
    void PositionChanging() {
        if(currentPosition == initialPosition.Length - 1) {
            currentPosition = 0;
            goalPosition = initialPosition[0];
        }
        else {
            currentPosition ++;
            goalPosition = initialPosition[currentPosition];
        }
    }

    void StateChanging(EnemyStates newState) {
        if(currentState != newState) {
            currentState = newState;
        }
    }

    void setDirectionAnim(Vector2 directionVector) {
        animator.SetFloat("Horizontal", directionVector.x);
        animator.SetFloat("Vertical", directionVector.y);
    }

    void AnimationMovement(Vector2 enemyDirection) {
        if(Mathf.Abs(enemyDirection.x) > Mathf.Abs(enemyDirection.y)) {
            if(enemyDirection.x > 0) {
                setDirectionAnim(Vector2.right);
            }
            else if(enemyDirection.x < 0) {
                setDirectionAnim(Vector2.left);
            }
        }
        else if(Mathf.Abs(enemyDirection.x) < Mathf.Abs(enemyDirection.y)) {
            if(enemyDirection.y > 0) {
                setDirectionAnim(Vector2.up);
            }
            else if(enemyDirection.y < 0) {
                setDirectionAnim(Vector2.down);
            }
        }
    }


    public Vector2 getBeginPosition() {
        return this.beginPosition;
    }
}
