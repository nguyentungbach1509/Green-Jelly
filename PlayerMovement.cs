using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/*
Controll the movement and states of the player
*/
public class PlayerMovement : MonoBehaviour
{

    public static PlayerMovement instance;
    public PlayerStats playerStats;
    public string scenePassword;
    public float moveSpeed = 2.5f;
    public float rollSpeed;
    public Rigidbody2D rb;
    Vector2 movement;
    Vector2 lastMove;
    public Animator animator;
    private SpriteRenderer sprite;
    private bool isStanding;
    private enum State {
        Normal,
        Rolling,
        Attacking,
        Standing,
        Die
    }

    private State state;
    private string currentState;
    private List<Vector2> directs;

    [Header("Attack Skill")]
    public Image atkSkillImage;
    private bool duringAtkCooldown = false;
    public float cooldownAtkTime = 1.5f;

    [Header("Roll Skill")]
    public Image rollSkillImage;
    private bool duringRollCooldown = false;
    public float cooldownRollTime = 3f;

    public bool playerBeKilled = false;
    
    //Another InventorySys
    public Inventorydb inventorydb;

    void Awake() {

        state = State.Normal;
        lastMove.x = 0;
        lastMove.y = -1;
        directs = new List<Vector2>();
        sprite = GetComponent<SpriteRenderer>();
        
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
    
        }
        else
        {
            if(instance != this)
            {
               Destroy(gameObject);
            }
        }

    }


    void Start() {

        atkSkillImage.fillAmount = 0;
        rollSkillImage.fillAmount = 0;
    }

    // Update is called once per frame
    void Update()
    {

        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        switch(state) {

            case State.Normal:

                if(movement.x != 0 || movement.y != 0) {
                    lastMove = movement;
                    if(directs.Count != 0) {
                        directs.RemoveAt(0);
                    }
                    directs.Add(movement);
                }

                currentState = "Normal";

                if(Input.GetKeyDown(KeyCode.Space) && duringRollCooldown == false) {
                    duringRollCooldown = true;
                    rollSkillImage.fillAmount = 1;
                    rollSpeed = 10f;
                    state = State.Rolling;
                    currentState = "Rolling";
                    animator.SetTrigger("Rolling");
                }
            
            
                if(Input.GetKeyDown(KeyCode.X) && duringAtkCooldown == false) {
                    duringAtkCooldown = true;
                    atkSkillImage.fillAmount = 1;
                    state = State.Attacking;
                    currentState = "Attacking";
                }

                
                animator.SetFloat("Speed", movement.sqrMagnitude);
                animator.SetFloat("Horizontal", movement.x);
                animator.SetFloat("Vertical", movement.y);
                animator.SetFloat("XDirect", lastMove.x);
                animator.SetFloat("YDirect", lastMove.y);

                break;

            case State.Rolling:
                float rollSpeedDrop = 3f;
                rollSpeed -= rollSpeed * rollSpeedDrop * Time.deltaTime;

                if(rollSpeed < 2f) {
                    state = State.Normal;
                }
                

                break;

            case State.Attacking:
                
                StartCoroutine(StartAttack());

                break;

            case State.Standing:

                isStanding = true;
                currentState = "Standing";
                animator.SetFloat("XDirect", lastMove.x);
                animator.SetFloat("YDirect", lastMove.y);
                animator.SetFloat("Speed", 0);
        
                break;

            case State.Die:
                
                playerBeKilled = true;
                animator.SetFloat("XDirect", lastMove.x);
                animator.SetFloat("YDirect", lastMove.y);
                animator.SetFloat("Speed", 0);
                animator.SetBool("beKilled", playerBeKilled);
                break;

        }

        attackCooldownAnimation();
        rollCooldownAnimation();
            
        
        animator.SetBool("isStanding", isStanding);
        
    }


    public void destroyPlayer() {
        SpawnPlayer.Instance.StartCoroutine("ReloadPlayer", instance);
        gameObject.SetActive(false);  
         
    }

    private void attackCooldownAnimation() {
        if(duringAtkCooldown) {

            atkSkillImage.fillAmount -= 1 / cooldownAtkTime * Time.deltaTime;

            if(atkSkillImage.fillAmount <= 0) {

                atkSkillImage.fillAmount = 0;
                duringAtkCooldown = false;
            }        
        }
    }

    private void rollCooldownAnimation() {
        if(duringRollCooldown) {

            rollSkillImage.fillAmount -= 1 / cooldownRollTime * Time.deltaTime;

            if(rollSkillImage.fillAmount <= 0) {

                rollSkillImage.fillAmount = 0;
                duringRollCooldown = false;
            }        
        }
    }


    void FixedUpdate() {

        switch(state) {
            case State.Normal:
                rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
                break;
            case State.Rolling:
                rb.MovePosition(rb.position + lastMove * rollSpeed * Time.fixedDeltaTime);
                break;
        }
       
    }   

    private IEnumerator StartAttack() {
        animator.SetBool("isAttacking", true);
        sprite.sortingOrder = 2;
        yield return null;
        animator.SetBool("isAttacking", false);
        yield return new WaitForSeconds(0.1f);
        sprite.sortingOrder = 0;
        state = State.Normal;
    }


    public void setState(float num) {
        switch(num) {
            case 0:
                state = State.Normal;
                isStanding = false;
                break;
            case 1:
                state = State.Standing;
                break;
            case 2:
                state = State.Die;
                isStanding = false;
                break;
        }
    }

    public string getCurrentState() {
        return currentState;
    }

    public List<Vector2> getDirects() {
        return directs;
    }


    public Vector2 getMovement() {
        return lastMove;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Items")) {
            ItemObject item = other.gameObject.GetComponent<ItemObject>();

            if(item.itemdb){
                inventorydb.AddItem(item.itemdb, 1);
                Destroy(other.gameObject);
            }
        }
    }

    /*private void OnApplicationQuit() {
        inventorydb.inventory_slots.Clear();
    }*/
}
