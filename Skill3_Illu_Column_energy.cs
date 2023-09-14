using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill3_Illu_Column_energy : MonoBehaviour
{   

    public int damagePerSec;


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


    public void DestroyEnergyObject() {
        Destroy(transform.parent.gameObject);
    }
}
