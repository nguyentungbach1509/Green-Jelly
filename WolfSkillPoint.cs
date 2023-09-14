using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class WolfSkillPoint : MonoBehaviour
{
    public float knockbackRange;
    public float knockbackTime;
    public string direct;

    public int dmg;

    
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player")) {

            
            Rigidbody2D pl = other.gameObject.GetComponent<Rigidbody2D>();


            if(pl != null) {
                transform.parent.gameObject.GetComponent<WolfBoss>().setTouchPlayer(true);
                StartCoroutine(DisplayEffect());
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
            transform.parent.gameObject.GetComponent<WolfBoss>().setTouchPlayer(false);
            if(pm.playerStats.getPlayerCurrentHealth() > 0) {

                pm.setState(0);
            }
            else {
                pm.setState(2);
            }
        }
    }


    private IEnumerator DisplayEffect() {
        gameObject.GetComponent<Renderer>().enabled = true;
        yield return new WaitForSeconds(.15f);
        gameObject.GetComponent<Renderer>().enabled = false;
    }
}
