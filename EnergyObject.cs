using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;



/*
Handle other energy object of enemies
*/
public class EnergyObject : MonoBehaviour
{   
    public float radiusAround;
    public float speedRotate;
    float coorX, coorY;
    public float objectAngle = 0f;

    private bool duringCooldown;
    public float moveCooldown;
    private float moveTimer;

    private bool touchMaxRange;
    public float knockbackRange;
    public float knockbackTime;

    public int dmg;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   
        
        coorX = transform.parent.gameObject.transform.position.x + Mathf.Cos(objectAngle) * radiusAround;
        coorY = transform.parent.gameObject.transform.position.y + Mathf.Sin(objectAngle) * radiusAround;
        transform.position = new Vector2(coorX, coorY);
        objectAngle = objectAngle + Time.deltaTime * speedRotate;

        if(objectAngle > 360f) {
            objectAngle = 0f;
        }

        calculateMoveCooldown();
        
    }

    void FixedUpdate() {
        if(!duringCooldown) {
            duringCooldown = true;
            moveTimer = 1;
        }
    }


    private void calculateMoveCooldown() {
        if(duringCooldown) {

            moveTimer -= 1 / moveCooldown * Time.deltaTime;

            if(moveTimer <= 0) {
                duringCooldown = false;
                moveTimer = 0;
                if(radiusAround < 14f && touchMaxRange == false) {
                    radiusAround += 1f;
                    
                }
                else {
                    touchMaxRange = true;
                }

                if(touchMaxRange == true) {
                    if(radiusAround > 2) {
                        radiusAround -= 1f;
                    }
                    else {
                        FinalBoss fboss = transform.parent.gameObject.GetComponent<FinalBoss>();
                        fboss.willDestroyAura();
                        Destroy(gameObject);
                    }
                }
            }
        }
    }
    
    
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player")) {

            Rigidbody2D pl = other.gameObject.GetComponent<Rigidbody2D>();

            if(pl != null) {
               
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

            if(pm.playerStats.getPlayerCurrentHealth() > 0) {

                pm.setState(0);
            }
            else {
                pm.setState(2);
            }
        }
    }
}
