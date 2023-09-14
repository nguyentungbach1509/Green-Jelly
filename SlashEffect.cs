using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class SlashEffect : MonoBehaviour
{   

    public Vector2 positionTarget;
    public int slash_dmg;
    public float slash_speed;
    private Vector2 slash_direct;
    private Animator animator;
    private int count = 0;
    public float knockbackTime;
    public float knockbackRange;
    private bool touchingPlayer = false;

    // Start is called before the first frame update
    void Start()
    {   
        animator = GetComponent<Animator>();
        Vector3 newPosition = Vector3.MoveTowards(transform.position, positionTarget, slash_speed * Time.fixedDeltaTime);
        slash_direct = (newPosition - transform.position);
        ControlSlashDirection(slash_direct);
    }

    // Update is called once per frame
    void Update()
    {   
        if(touchingPlayer == false) {
            transform.position = Vector2.MoveTowards(transform.position, positionTarget, slash_speed * Time.fixedDeltaTime);
        }
        

        if((transform.position.x == positionTarget.x && transform.position.y == positionTarget.y) || touchingPlayer) {
            animator.SetTrigger("Destroy");
        }

    }

    private void ControlSlashDirection(Vector2 direct){ 
        animator.SetFloat("Horizontal", direct.x);
        animator.SetFloat("Vertical", direct.y);
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
                touchingPlayer = true;
            }
        }

        
    }

    private IEnumerator KnockBackPlayer(Rigidbody2D player) {
        if(player != null) {
            
            PlayerMovement pm = player.GetComponent<PlayerMovement>();
            yield return new WaitForSeconds(knockbackTime);
            player.velocity = Vector2.zero;
            pm.playerStats.setPlayerTakeDamage(slash_dmg);
            if(pm.playerStats.getPlayerCurrentHealth() > 0) {

                pm.setState(0);
            }
            else {
                pm.setState(2);
            }

        }
    }


    public void Destroyslash() {
        Destroy(gameObject);
    }
}
