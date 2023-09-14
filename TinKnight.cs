using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Handle and controll the Tin knight enemies
*/
public class TinKnight : Enemy
{
    public Transform playerTransform;
    public float chasingRange;
    public float attackRange;
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
    private bool canMove = true;

    public float specialCoolDown;
    private float specialTimer;

    private bool duringSpecialCooldown = false;
    
    private float tempChasingRange;
    private float tempSpeed;


    public float skillCoolDown;
    private float skillTimer;

    private bool duringSkillCooldown = true;
    
    public SkillController skillObject;
    
    
    

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
            animator.SetBool("Deadth", true);
            if(!playerMovement.playerBeKilled){
                playerMovement.setState(0);
            }

        }


        calculateCooldown();
        calculateSpecialCooldown();
        calculateSkillCooldown();
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
       
        if(distance <= chasingRange && distance > attackRange && !playerMovement.playerBeKilled) {
            
            animator.SetBool("Staying", false);
            Debug.Log("Move " + canMove);

            if(currentState == EnemyStates.enemymove && currentState != EnemyStates.enemystunning && currentState != EnemyStates.enemyattack){

                Vector3 temp = Vector3.MoveTowards(transform.position, playerTransform.position, enemySpeed * Time.deltaTime);

                AnimationMovement(temp - transform.position);
                rb.MovePosition(temp); 
                StateChanging(EnemyStates.enemymove);
            
            }

        
        }
        else if((distance > chasingRange) || playerMovement.playerBeKilled) {
            
            if(canMove && currentState == EnemyStates.enemymove) {
                float distanceToGoal = Vector3.Distance(transform.position, initialPosition[currentPosition]);

                if(distanceToGoal > rangePoint) {
                
                    Vector3 temp = Vector3.MoveTowards(transform.position, initialPosition[currentPosition], enemySpeed * Time.deltaTime);
                    AnimationMovement(temp - transform.position);
                    rb.MovePosition(temp); 
                    
                }
                else {
                    
                    StartCoroutine(stayingBetweenPosition());
                    
                }
                
                StateChanging(EnemyStates.enemymove);
            }
            
        }
        else if(distance <= attackRange && !playerMovement.playerBeKilled){

            if(currentState != EnemyStates.enemystunning) {
                if(duringSkillCooldown == false) {

                        StateChanging(EnemyStates.enemyidle);
                        invulnerable = true;
                        canMove=false;
                        duringSpecialCooldown = true;
                        duringCooldown = true;
                        duringSkillCooldown = true;
                        attackTimer = 1;
                        skillTimer = 1;
                        specialTimer = 1;
                        animator.SetTrigger("Skill");
                }
                else {

                    if(duringSpecialCooldown == false) {
                        duringSpecialCooldown = true;
                        duringCooldown = true;
                        invulnerable = true;
                        specialTimer = 1;
                        attackTimer = 1;
                        SpecialAttackPlayer(); 
                        StateChanging(EnemyStates.enemymove);
                    }
                    else {
                        if(duringCooldown == false) {
                            duringCooldown = true;
                            attackTimer = 1;
                            AttackPlayer();  
                            StateChanging(EnemyStates.enemymove);
                        }
                    }
                }
            }            
        }
        
    }


    private IEnumerator stayingBetweenPosition() {
        canMove = false;
        if(chasingRange == 1) {
            chasingRange = tempChasingRange;
        }
        StateChanging(EnemyStates.enemyidle);
        animator.SetBool("Staying", true);
        yield return new WaitForSeconds(2f);
        animator.SetBool("Staying", false);
        PositionChanging();
        canMove = true;
    }


    private void AttackPlayer() {
        StateChanging(EnemyStates.enemyattack);
        Vector3 temp = Vector3.MoveTowards(transform.position, playerTransform.position, enemySpeed * Time.deltaTime);
        AnimationMovement(temp - transform.position);
        animator.SetTrigger("Attack");
    }

    private void SpecialAttackPlayer() {
        tempSpeed = enemySpeed;
        enemySpeed = 2.5f;
        StateChanging(EnemyStates.enemyattack);
        Vector3 temp = Vector3.MoveTowards(transform.position, playerTransform.position, enemySpeed * Time.deltaTime);
        AnimationMovement(temp - transform.position);
        animator.SetTrigger("SpecialAttack");
    }


    public void spawnSkill() {
        Instantiate(skillObject, transform.position, Quaternion.identity);
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


    private void calculateSpecialCooldown() {
        if(duringSpecialCooldown) {

            specialTimer -= 1 / specialCoolDown * Time.deltaTime;

            if(specialTimer <= 0) {
                duringSpecialCooldown = false;
                specialTimer = 0;
            }
        }
    }

    private void calculateSkillCooldown() {
        if(duringSkillCooldown) {

            skillTimer -= 1 / skillCoolDown * Time.deltaTime;

            if(skillTimer <= 0) {
                duringSkillCooldown = false;
                skillTimer = 0;
            }
        }
    }

    void PositionChanging() {
        if(currentPosition == initialPosition.Length - 1) {
            currentPosition = 0;
            goalPosition = initialPosition[0];
        }
        else {

            int randomPosition = Mathf.FloorToInt(Random.Range(0, initialPosition.Length));
            while(randomPosition == currentPosition) {
                randomPosition = Mathf.FloorToInt(Random.Range(0, initialPosition.Length));
            }
            currentPosition = randomPosition;
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

    /*
    private void OnCollisionEnter2D(Collision2D other) {

        if(other.gameObject.CompareTag("Environment")) {
            tempChasingRange = chasingRange;
            chasingRange = 1;
        }
    }
    */

    public void returnSpeed() {
        if(tempSpeed != 0) {
            enemySpeed = tempSpeed;
        }
        if(invulnerable == true) {
            invulnerable = false;
        }
    }


    public void returnNormalState() {
        if(invulnerable == true) {
            invulnerable = false;
        }
        StateChanging(EnemyStates.enemymove);
        canMove = true;
    }
}
