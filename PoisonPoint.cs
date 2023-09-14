using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Poison point to make player get poison with attack of the enemies
*/
public class PoisonPoint : MonoBehaviour
{
    public float poisonTime;

    public int poisonDmg;
    
    
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player")) {

            Rigidbody2D pl = other.gameObject.GetComponent<Rigidbody2D>();

            if(pl != null) {
               PlayerMovement player = pl.GetComponent<PlayerMovement>();
               if(player.playerStats.isGotPoison()) {
                    player.moveSpeed = 2.5f;
                    player.playerStats.setPoison(false, poisonDmg);
                    
               }

                StartCoroutine(PoisonPlayer(pl));
            }
        }
    }

    private IEnumerator PoisonPlayer(Rigidbody2D player) {
        if(player != null) {
            
            PlayerMovement pm = player.GetComponent<PlayerMovement>();
            pm.playerStats.setPoison(true, poisonDmg);
            pm.moveSpeed = 1f;
            yield return new WaitForSeconds(0);
            
        }
    }
}
