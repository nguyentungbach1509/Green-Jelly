using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kamefx : MonoBehaviour
{
    
    private Animator animator;
    public Vector2 direction;
    public int damagePerSec;
   
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        setDirection(direction);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void setDirection(Vector2 direct) {
        animator.SetFloat("Horizontal", direct.x);
        animator.SetFloat("Vertical", direct.y);
    }


    public void DestroyKame() {
        
        Destroy(gameObject);
    }


    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player")) {

            Rigidbody2D pl = other.gameObject.GetComponent<Rigidbody2D>();

            if(pl != null) {
                
               StartCoroutine(TakeDamgeOverSec(pl));
            }
        }
    }

    private IEnumerator TakeDamgeOverSec(Rigidbody2D player) {
        if(player != null) {
            
            PlayerMovement pm = player.GetComponent<PlayerMovement>();
            pm.playerStats.stayInArea(true, damagePerSec);
            yield return null;
        }
    }


    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player")) {
            
            Rigidbody2D pl = other.gameObject.GetComponent<Rigidbody2D>();
            
            if(pl != null) {

               PlayerMovement pm = pl.GetComponent<PlayerMovement>();
               pm.playerStats.stayInArea(false, damagePerSec);
            }
            
        }
    }
}
