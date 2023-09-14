using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


/*
Handle the energy ball objects of enemies
*/
public class EnergyBall : MonoBehaviour
{

    
    private Transform playerTransform;
    private Vector2 target;

    public float speedBall;
    public float knockbackTime;
    public float knockbackRange;
    public int dmg;
    private int count = 0;

    private Animator animator;
    private Vector2 beginPosition;
    private bool touchingEnvironment = false;


    // Start is called before the first frame update
    void Start()
    {   
        if(GameObject.FindGameObjectWithTag("Player") != null) {
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
            target = new Vector2(playerTransform.position.x, playerTransform.position.y);
        }
       
        animator = GetComponent<Animator>();
        beginPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {   
        
            if(!touchingEnvironment && playerTransform != null) {
                transform.position = Vector2.MoveTowards(transform.position, target, speedBall * Time.deltaTime);
            }
            

            if((transform.position.x == target.x && transform.position.y == target.y) || touchingEnvironment || playerTransform == null) {
                animator.SetTrigger("Destroy");
            }
                    

            AnimationMovement(target);
        
    }

    public void DestroyBall() {
        Destroy(gameObject);
    }


    void setDirectionAnim(Vector2 directionVector) {
        animator.SetFloat("Horizontal", directionVector.x);
        animator.SetFloat("Vertical", directionVector.y);
    }


    void AnimationMovement(Vector2 enemyDirection) {
        if(Mathf.Abs(enemyDirection.x - beginPosition.x) > Mathf.Abs(enemyDirection.y - beginPosition.y)) {
            if(enemyDirection.x > beginPosition.x) {
                setDirectionAnim(Vector2.right);
                
            }
            else if(enemyDirection.x < beginPosition.x) {
                setDirectionAnim(Vector2.left);
                
            }
        }
        else if(Mathf.Abs(enemyDirection.x - beginPosition.x) < Mathf.Abs(enemyDirection.y - beginPosition.y)) {
            if(enemyDirection.y > beginPosition.y) {
                setDirectionAnim(Vector2.up);
               
            }
            else if(enemyDirection.y < beginPosition.y) {
                setDirectionAnim(Vector2.down);
                
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player")) {

            Rigidbody2D pl = other.gameObject.GetComponent<Rigidbody2D>();
            count += 1;
            
            if(pl != null && count == 1) {
                
                pl.GetComponent<PlayerMovement>().setState(1);
                Vector3 difference = pl.transform.position - transform.position;
                difference = difference.normalized * (knockbackRange);
                pl.DOMove(pl.transform.position + difference, knockbackTime);
                StartCoroutine(KnockBackPlayer(pl));
            }
        }

        if(other.CompareTag("Environment")) {
            touchingEnvironment = true;
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
