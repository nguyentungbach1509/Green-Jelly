using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


/*
Controll the skill objects of enemies
*/
public class Beamfx : MonoBehaviour
{   

    public int damagePerSec;

    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }
 
    public void BodyShooting() {
        transform.Find("Beam_Body").gameObject.SetActive(true);
    }

    public void BeamDestroy() {
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
