using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/*
Handle the arrow object
*/
public class Arrow : MonoBehaviour
{

    private PlayerMovement playerMovement;
    private Transform playerTransform;
    private Vector2 target;

    public float speedArrow;
    public float knockbackTime;
    public float knockbackRange;
    public int dmg;
    private int count = 0;

    private Animator animator;
    private Vector2 beginPosition;
    private Vector2 anotherTarget;
    private Vector2 tempTarget;

    private bool touchingEnvironment=false;



    // Start is called before the first frame update
    void Start()
    {   
        if(GameObject.FindGameObjectWithTag("Player") != null) {
            playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
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

            if(anotherTarget != null && anotherTarget != Vector2.zero) {
                target = new Vector2(anotherTarget.x, anotherTarget.y);
            }

            if(!touchingEnvironment && playerTransform != null) { 
                transform.position = Vector2.MoveTowards(transform.position, target, speedArrow * Time.deltaTime);
            }
            

            if(playerTransform == null ||(transform.position.x == target.x && transform.position.y == target.y) ||  touchingEnvironment) {
                animator.SetTrigger("Destroy");
            }
                    

            AnimationMovement(target);
        
    }

    public void DestroyArrow() {
        Destroy(gameObject);
    }


    void setDirectionAnim(Vector2 directionVector) {
        animator.SetFloat("Horizontal", directionVector.x);
        animator.SetFloat("Vertical", directionVector.y);
    }


    void AnimationMovement(Vector2 enemyDirection) {
        if(Mathf.Abs(enemyDirection.x - beginPosition.x) > Mathf.Abs(enemyDirection.y - beginPosition.y)) {
            if(Mathf.Abs(enemyDirection.y - beginPosition.y) < 2) {
                if(enemyDirection.x > beginPosition.x) {
                    setDirectionAnim(Vector2.right);
                    
                }
                else if(enemyDirection.x < beginPosition.x) {
                    setDirectionAnim(Vector2.left);
                    
                }
            }
            else {
                if(enemyDirection.x > beginPosition.x) {
                    if(enemyDirection.y > beginPosition.y) {
                        setDirectionAnim(new Vector2(1,1));
                    
                    }
                    else if(enemyDirection.y < beginPosition.y) {
                        setDirectionAnim(new Vector2(1,-1));
                        
                    }
                    
                }
                else if(enemyDirection.x < beginPosition.x) {
                    if(enemyDirection.y > beginPosition.y) {
                        setDirectionAnim(new Vector2(-1,1));
                    
                    }
                    else if(enemyDirection.y < beginPosition.y) {
                        setDirectionAnim(new Vector2(-1,-1));
                        
                    }
                    
                }
            }
            
        }
        else if(Mathf.Abs(enemyDirection.x - beginPosition.x) < Mathf.Abs(enemyDirection.y - beginPosition.y)) {
            if(Mathf.Abs(enemyDirection.x - beginPosition.x) < 2) {
                if(enemyDirection.y > beginPosition.y) {
                    setDirectionAnim(Vector2.up);
                
                }
                else if(enemyDirection.y < beginPosition.y) {
                    setDirectionAnim(Vector2.down);
                    
                }
            }
            else {
                if(enemyDirection.y > beginPosition.y) {
                    if(enemyDirection.x > beginPosition.x) {
                        setDirectionAnim(new Vector2(1,1));
                    
                    }
                    else if(enemyDirection.x < beginPosition.x) {
                        setDirectionAnim(new Vector2(-1,1));
                        
                    }
                
                }
                else if(enemyDirection.y < beginPosition.y) {
                    if(enemyDirection.x > beginPosition.x) {
                        setDirectionAnim(new Vector2(1,-1));
                        
                    }
                    else if(enemyDirection.x < beginPosition.x) {
                        setDirectionAnim(new Vector2(-1,-1));
                        
                    }
                }
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

    public void setAnotherTarget(Vector2 another) {
        anotherTarget = new Vector2(another.x, another.y);
    }

}
