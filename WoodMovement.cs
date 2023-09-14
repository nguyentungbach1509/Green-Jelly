using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Handle and controll the wood monster enemies
*/
public class WoodMovement : Enemy
{
    public Transform playerTransform;
    public float chasingRange;
    public float attackRange;
    public float shootingRange;
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
    public EnergyBall energyBall;

    public float shootingCoolDown;
    private float shootingTimer;

    private bool duringShooting = false;

    public float regenCoolDown;
    private float regenTimer;

    private bool duringRegen = false;


    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentState = EnemyStates.enemymove;
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        animator = GetComponent<Animator>();
        beginPosition = new Vector2(transform.position.x, transform.position.y);
    }

    // Update is called once per frame
    void Update() {

        if(currentHealth <= 0) {
            animator.SetTrigger("Die");
            
        }

        calculateCooldown();
        shootingTimeCalculate();
        regenTimeCalculate();
        animator.SetBool("Hurt", (currentState == EnemyStates.enemystunning));
        animator.SetBool("Staying", (currentState == EnemyStates.enemyidle));

    }

    void FixedUpdate()
    {
        if(currentHealth > 0 && currentState != EnemyStates.enemydie) {
            if(currentHealth < (enemyHealth/2)) {
                if(Mathf.FloorToInt(Random.Range(0,2)) == 1) {
                    if(duringRegen == false && currentState != EnemyStates.enemystunning) {
                        duringRegen = true;
                        regenTimer = 1;
                        StartCoroutine(enemyRegen());
                        
                    }
                }
                else {
                    DistanceChecking();
                }
            }
            else {
                DistanceChecking();
            }
        }
    }


    public virtual void DistanceChecking() {
        float distance =  0;

        if(!playerMovement.playerBeKilled) {
            distance = Vector3.Distance(transform.position,playerTransform.position);
        }
    
        if(distance <= chasingRange && distance > shootingRange && !playerMovement.playerBeKilled) {
            
            
            if((currentState == EnemyStates.enemymove || currentState == EnemyStates.enemyidle) 
            && currentState != EnemyStates.enemystunning && currentState != EnemyStates.enemyattack){

                Vector3 temp = Vector3.MoveTowards(transform.position, playerTransform.position, enemySpeed * Time.deltaTime);

                AnimationMovement(temp - transform.position);
                rb.MovePosition(temp); 
                StateChanging(EnemyStates.enemymove);
            
            }
        
        }
        else if((distance > chasingRange) || playerMovement.playerBeKilled) {
            
            float distanceToGoal = Vector3.Distance(transform.position, beginPosition);
            
            if(distanceToGoal > rangePoint) {
                if(currentState != EnemyStates.enemyregen && currentState == EnemyStates.enemymove) {
                    Vector3 temp = Vector3.MoveTowards(transform.position, beginPosition, enemySpeed * Time.deltaTime);
                    AnimationMovement(temp - transform.position);
                    rb.MovePosition(temp); 
                    StateChanging(EnemyStates.enemymove);
                }
                
            }
            else {

                StateChanging(EnemyStates.enemyidle);
                
            }

            
        }
        else if((distance <= shootingRange) && !playerMovement.playerBeKilled) {

            if(distance > attackRange) {
            
                if(currentState != EnemyStates.enemystunning && duringShooting == false && currentState != EnemyStates.enemyregen) {
                    StateChanging(EnemyStates.enemyattack);
                    duringShooting = true;
                    shootingTimer = 1;
                    Vector3 temp = Vector3.MoveTowards(transform.position, playerTransform.position, enemySpeed * Time.deltaTime);
                    AnimationMovement(temp - transform.position);
                    animator.SetTrigger("Shooting");
                }
            }
            else {

                if(currentState != EnemyStates.enemystunning && duringCooldown == false && currentState != EnemyStates.enemyregen) {
                    StateChanging(EnemyStates.enemyattack);
                    duringCooldown = true;
                    attackTimer = 1;
                    animator.SetTrigger("Attack");   
                }
            }

            StateChanging(EnemyStates.enemymove);
        }

    
    }


    private IEnumerator enemyRegen() {
        StateChanging(EnemyStates.enemyregen);
        animator.SetBool("Regen", true);
        yield return new WaitForSeconds(5f);
        animator.SetBool("Regen", false);
        StateChanging(EnemyStates.enemymove);
    }


    public void spawnEnergyBall() {
        Instantiate(energyBall, transform.position, Quaternion.identity);
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


    private void shootingTimeCalculate() {
        if(duringShooting) {

            shootingTimer -= 1 / shootingCoolDown * Time.deltaTime;

            if(shootingTimer <= 0) {
                duringShooting = false;
                shootingTimer = 0;
            }
        }
    }



    private void regenTimeCalculate() {
        if(duringRegen) {

            regenTimer -= 1 / regenCoolDown * Time.deltaTime;

            if(regenTimer <= 0) {
                duringRegen = false;
                regenTimer = 0;
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
