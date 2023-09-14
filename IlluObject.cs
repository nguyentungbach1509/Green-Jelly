using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class IlluObject : MonoBehaviour
{
    private Transform playerTransform;
    private Vector2 target;

    public float speed;
    public float knockbackTime;
    public float knockbackRange;
    public int dmg;
    private int count = 0;

    private Animator animator;
    private Vector2 beginPosition;
    private Vector2 tempTarget;

    private bool touchingPlayer = false;
    private bool noTimeLeft = false;

    private float liveTimer;
    public float liveCooldown;
    private bool duringCooldown;
    private Vector2 directionObject;



    // Start is called before the first frame update
    void Start()
    {   
        
        if(GameObject.FindGameObjectWithTag("Player") != null) {
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
            target = new Vector2(playerTransform.position.x, playerTransform.position.y);
            tempTarget = new Vector2(playerTransform.position.x, playerTransform.position.y);
           
        }
        
        animator = GetComponent<Animator>();
        beginPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {       
        if(!touchingPlayer) { 
            if(playerTransform != null && (transform.position.x != target.x || transform.position.y != target.y)) {
                transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
            }
            else {
                animator.SetTrigger("Destroy");
            }
            
        }

        if(touchingPlayer || noTimeLeft) {
            animator.SetTrigger("Destroy");
        }

        ControlSlashDirection(directionObject);
        calculateCooldown();
    }


    private void ControlSlashDirection(Vector2 direct){ 
        animator.SetFloat("Horizontal", direct.x);
        animator.SetFloat("Vertical", direct.y);
    }


    void FixedUpdate() {
        if(!duringCooldown) {
            duringCooldown = true;
            liveTimer = 1;
            
        } 
    }

    public void DestroyIlluSlash() {
        Destroy(gameObject);
    }


    private void calculateCooldown() {
        if(duringCooldown) {

            liveTimer -= 1 / liveCooldown * Time.deltaTime;

            if(liveTimer <= 0) {
                duringCooldown = false;
                liveTimer = 0;
                noTimeLeft = true;
            }
        }
    }



    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player")) {

            Rigidbody2D pl = other.gameObject.GetComponent<Rigidbody2D>();
            count += 1;
            
            if(pl != null && count == 1) {
                
                touchingPlayer = true;
                pl.GetComponent<PlayerMovement>().setState(1);
                Vector3 difference = pl.transform.position - transform.position;
                difference = difference.normalized * (knockbackRange);
                pl.DOMove(pl.transform.position + difference, knockbackTime);
                StartCoroutine(KnockBackPlayer(pl));
            }
        }

    }

    private IEnumerator KnockBackPlayer(Rigidbody2D player) {
        if(player != null) {
            
            PlayerMovement pm = player.GetComponent<PlayerMovement>();
            yield return new WaitForSeconds(knockbackTime);
            player.velocity = Vector2.zero;
            pm.playerStats.setPlayerTakeDamage(dmg);
            if(pm.playerStats.getPlayerCurrentHealth() > 0) {

                pm.setState(0);
            }
            else {
                pm.setState(2);
            }

        }
    }

    public void setDirectionObject(Vector2 dir) {
        directionObject = dir;
    }

}
