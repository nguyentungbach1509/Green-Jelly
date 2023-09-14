using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


/*
Handle attack objects that the final boss creates when attacking
*/
public class FinalBossNormalAttackObject : MonoBehaviour
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
    public Vector2 direction;



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

        if(direction != Vector2.zero) {
            ControlObjectDirection();
        }
    }

    // Update is called once per frame
    void Update()
    {       

            if(!touchingPlayer) { 
                if(playerTransform != null) {
                    transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
                }
                else {
                    animator.SetTrigger("Destroy");
                }
                
            }
            

            if(touchingPlayer || (transform.position.x == target.x && transform.position.y == target.y)) {
                animator.SetTrigger("Destroy");
            }
    }

    public void DestroyBall() {
        Destroy(gameObject);
    }


    private void ControlObjectDirection(){ 
        animator.SetFloat("Horizontal", direction.x);
        animator.SetFloat("Vertical", direction.y);
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

   

}
