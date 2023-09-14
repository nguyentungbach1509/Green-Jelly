using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class WolfEnergyBall : MonoBehaviour
{

    private PlayerMovement playerMovement;
    private Transform playerTransform;
    private Vector2 target;

    public float speed;
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
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        target = new Vector2(playerTransform.position.x, playerTransform.position.y);
        tempTarget = new Vector2(playerTransform.position.x, playerTransform.position.y);
        animator = GetComponent<Animator>();
        beginPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {       
            
            if(anotherTarget != null && anotherTarget != Vector2.zero) {
                target = new Vector2(anotherTarget.x, anotherTarget.y);
            }

            if(!touchingEnvironment) { 
                transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
            }
            

            if((transform.position.x == target.x && transform.position.y == target.y) ||  touchingEnvironment) {
                animator.SetTrigger("Destroy");
            }
    
    }

    public void DestroyWolfEnergyBall() {
        Destroy(gameObject);
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
