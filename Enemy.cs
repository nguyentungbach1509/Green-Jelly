using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public enum EnemyStates {

    enemyidle,
    enemymove,
    enemyattack,
    enemystunning,
    enemydie,
    enemyregen,
    enemydef,
    enemypreparedef,
}


/*
The parent class for enemies
*/
public class Enemy : MonoBehaviour
{
    public string enemyName;
    public int enemyLevel;
    public int enemyHealth;
    public int currentHealth;
    public int healthRegen;
    public int enemyDamage;
    public int enemyTouchingDamage;
    public float enemySpeed;
    public EnemyStates currentState;
    public float knockbackRange;
    public float knockbackTime;
    public float enemyDropEXP;
    public EnemyHealthBar enemyHealthBar;
    public GameObject[] drop_food = new GameObject[0];

    public bool prepareDef;
    public bool invulnerable = false;
    public bool enemyTouchingEnvironment;


    
    
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Player")) {

            Rigidbody2D pl = other.gameObject.GetComponent<Rigidbody2D>();
            gameObject.transform.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;

            if(pl != null) {
                
                
                pl.GetComponent<PlayerMovement>().setState(1);
                Vector3 difference = pl.transform.position - transform.position;
                difference = difference.normalized * knockbackRange;
                pl.DOMove(pl.transform.position + difference, knockbackTime);
                StartCoroutine(KnockBackPlayer(pl));
            }
        }

        if(other.gameObject.CompareTag("Environment")) {
            enemyTouchingEnvironment = true;
        }
    }

    private IEnumerator KnockBackPlayer(Rigidbody2D player) {
        if(player != null) {
            gameObject.transform.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            PlayerMovement pm = player.GetComponent<PlayerMovement>();
            yield return new WaitForSeconds(knockbackTime);
            player.velocity = Vector2.zero;
            pm.playerStats.setPlayerTakeDamage(enemyTouchingDamage);
            
            if(pm.playerStats.getPlayerCurrentHealth() > 0) {
                pm.setState(0);
            }
            else {
                pm.setState(2);
            }
        }
    }

    
    public void dropCoin(Enemy enemy) {
        
        int random = Mathf.FloorToInt(Random.Range(1, enemyLevel + 1));

        for(int i = 0; i < random; i++) {

            GoldCoinManagement.Instance.StartCoroutine("SpawnGoldCoin", new Vector2(enemy.transform.position.x + Random.Range(-0.7f, 0.7f), enemy.transform.position.y + Random.Range(-0.7f, 0.7f)));
        }
    }

    public void DestroyEnemies() {
        Destroy(gameObject);
    }

    public void regenHealth() {
        currentHealth += healthRegen;
    }
 
}
