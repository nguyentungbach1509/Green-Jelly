using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


/*
Handle the dragkon boss's illusion 
*/
public class DragkonIllusions : MonoBehaviour
{
    private Animator animator;
    public Vector2 illuDirect;
    public Kamefx kame;
    public int touchingDmg;
    public float knockbackTime;
    public float knockbackRange;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        setIlluDirection(illuDirect);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void setIlluDirection(Vector2 direct) {
        animator.SetFloat("Horizontal", direct.x);
        animator.SetFloat("Vertical", direct.y);
    }


    public void DestroyIllu() {
        Destroy(gameObject);
    }


    public void IlluSpawnKame() {

        if(illuDirect == Vector2.right) {
            ((Instantiate(kame, new Vector2(transform.position.x + 3.52f, transform.position.y), Quaternion.identity)).GetComponent<Kamefx>()).direction = Vector2.right;
        }
        else if(illuDirect == Vector2.left) {
            ((Instantiate(kame, new Vector2(transform.position.x - 3.52f, transform.position.y), Quaternion.identity)).GetComponent<Kamefx>()).direction = Vector2.left;
        }
        else if(illuDirect == Vector2.up) {
            ((Instantiate(kame, new Vector2(transform.position.x, transform.position.y + 3.839f), Quaternion.identity)).GetComponent<Kamefx>()).direction = Vector2.up;
        }      
        else if(illuDirect == Vector2.down){        
            ((Instantiate(kame, new Vector2(transform.position.x, transform.position.y - 1.63f), Quaternion.identity)).GetComponent<Kamefx>()).direction = Vector2.down;
        }
    }


    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player")) {

            Rigidbody2D pl = other.gameObject.GetComponent<Rigidbody2D>();

            if(pl != null) {
                
                
                pl.GetComponent<PlayerMovement>().setState(1);
                Vector3 difference = pl.transform.position - transform.position;
                difference = difference.normalized * knockbackRange;
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
            pm.playerStats.setPlayerTakeDamage(touchingDmg);
            
            if(pm.playerStats.getPlayerCurrentHealth() > 0) {
                pm.setState(0);
            }
            else {
                pm.setState(2);
            }
        }
    }
}
