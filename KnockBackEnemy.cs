using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


/*
Make the attack of player knock the enemy back
*/
public class KnockBackEnemy : MonoBehaviour
{
    public float knockbackRange;
    public float knockbackTime;
    public float hidingTime;
    private float nextHiddingTime;
    private bool hit;

    private Rigidbody2D erb;

    void Update() {
        
        if(Time.time >= nextHiddingTime && erb != null) {

            erb.GetComponent<Enemy>().enemyHealthBar.gameObject.SetActive(false);
            nextHiddingTime = Time.time + hidingTime;
        }
        
    }
    
    void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("SimpleSlimes") || other.gameObject.CompareTag("SplitSlimes") ||
        other.gameObject.CompareTag("SmallSlimes") || other.gameObject.CompareTag("MonsterSlimes") 
        || other.gameObject.CompareTag("BasicCobra") || other.gameObject.CompareTag("Woods") || 
        other.gameObject.CompareTag("ChobinHood") || other.gameObject.CompareTag("ChobinHoodlum") || 
        other.gameObject.CompareTag("ApeMummy") || other.gameObject.CompareTag("Skeletons") 
        || other.gameObject.CompareTag("TinKnights") || other.gameObject.CompareTag("CopKnights")) {

            nextHiddingTime = Time.time + hidingTime;
            Rigidbody2D enemy = other.GetComponent<Rigidbody2D>();
            executeKnockBack(enemy, other);
        }

        if(other.gameObject.CompareTag("Bosses")) {
            Rigidbody2D enemy = other.GetComponent<Rigidbody2D>();
            hittingBosses(enemy);
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.CompareTag("SimpleSlimes") || other.gameObject.CompareTag("SplitSlimes") ||
        other.gameObject.CompareTag("SmallSlimes") || other.gameObject.CompareTag("MonsterSlimes") 
        || other.gameObject.CompareTag("BasicCobra") || other.gameObject.CompareTag("Woods") || 
        other.gameObject.CompareTag("ChobinHood") || other.gameObject.CompareTag("ChobinHoodlum") || 
        other.gameObject.CompareTag("ApeMummy") ||  other.gameObject.CompareTag("Skeletons") 
        || other.gameObject.CompareTag("TinKnights") || other.gameObject.CompareTag("CopKnights")) {

            erb = other.GetComponent<Rigidbody2D>();
            nextHiddingTime = Time.time + hidingTime;
            erb.GetComponent<Enemy>().prepareDef =  false;
        }

    }

    private IEnumerator KnockBack(Rigidbody2D enemy, Collider2D other) {

        PlayerStats ps= this.transform.parent.gameObject.GetComponent<PlayerMovement>().playerStats;

        if(enemy != null) {

            Debug.Log(enemy.GetComponent<Enemy>().invulnerable + " Invulnerable");
            enemy.GetComponent<Enemy>().enemyHealthBar.setMaxHealth(enemy.GetComponent<Enemy>().enemyHealth);
            enemy.GetComponent<Enemy>().enemyHealthBar.setHealth(enemy.GetComponent<Enemy>().currentHealth);

            int health = enemy.GetComponent<Enemy>().currentHealth;

            if(enemy.GetComponent<Enemy>().currentState != EnemyStates.enemydef && !enemy.GetComponent<Enemy>().invulnerable) {
                health = enemy.GetComponent<Enemy>().currentHealth - ps.getPlayerDamage();
            }

            enemy.GetComponent<Enemy>().enemyHealthBar.gameObject.SetActive(true);
            enemy.GetComponent<Enemy>().enemyHealthBar.setText(enemy.GetComponent<Enemy>().enemyName, enemy.GetComponent<Enemy>().enemyLevel);
            yield return new WaitForSeconds(knockbackTime);

            if(!enemy.GetComponent<Enemy>().invulnerable) {
                enemy.velocity = Vector2.zero;
            }
            
            if(enemy.GetComponent<Enemy>().currentState != EnemyStates.enemydef && !enemy.GetComponent<Enemy>().invulnerable) {
                enemy.GetComponent<Enemy>().currentHealth = health;
                enemy.GetComponent<Enemy>().enemyHealthBar.setHealth(health);
            }
            
            if(!enemy.GetComponent<Enemy>().invulnerable) {
                if(health > 0) {
                    enemy.GetComponent<Enemy>().currentState = EnemyStates.enemymove;
                }
                else {

                    EnemyHealthBar ehb =  enemy.GetComponent<Enemy>().enemyHealthBar;
                    

                    if(other.gameObject.CompareTag("SimpleSlimes")) {
                        SimpleSlime ss = other.gameObject.GetComponent<SimpleSlime>();
                        ss.currentState = EnemyStates.enemydie;
                        enemy.GetComponent<Enemy>().dropCoin(ss);
                        SimpleSlimesManagement.Instance.saveVector(ss.initialPosition, enemy.GetComponent<Enemy>().enemyHealth);
                        SimpleSlimesManagement.Instance.StartCoroutine("SpawnSimpleSlime", ss.getBeginPosition());
                        
                    }
                    
                    if(other.gameObject.CompareTag("SplitSlimes")) {
                        SimpleSlime ss = other.gameObject.GetComponent<SimpleSlime>();
                        SlimesHolderManagement.Instance.saveStats(ss.initialPosition, enemy.GetComponent<Enemy>().enemyHealth, ss.getBeginPosition());
                        SlimesHolderManagement.Instance.StartCoroutine("SpawnSlimeHolder", new Vector2(ss.transform.position.x, ss.transform.position.y));
                    }

                    if(other.gameObject.CompareTag("SmallSlimes")) {
                        SimpleSlime ss = other.gameObject.GetComponent<SimpleSlime>();
                        enemy.GetComponent<Enemy>().dropCoin(ss);
                        SlimeHolder sh = ss.transform.parent.gameObject.GetComponent<SlimeHolder>();
                        sh.numberSlime ++;
                    }

                    if(other.gameObject.CompareTag("MonsterSlimes")) {
                        MonsterSlime ms = other.gameObject.GetComponent<MonsterSlime>();
                        ms.currentState = EnemyStates.enemydie;
                        enemy.GetComponent<Enemy>().dropCoin(ms);
                        MonsterSlimesManagement.Instance.saveVector(ms.initialPosition, enemy.GetComponent<Enemy>().enemyHealth);
                        MonsterSlimesManagement.Instance.StartCoroutine("SpawnMonsterSlime", ms.getBeginPosition());
                    }

                    if(other.gameObject.CompareTag("BasicCobra")) {
                        BasicCobra bc = other.gameObject.GetComponent<BasicCobra>();
                        bc.currentState = EnemyStates.enemydie;
                        enemy.GetComponent<Enemy>().dropCoin(bc);
                        BasicCobraManagement.Instance.saveVector(bc.initialPosition, enemy.GetComponent<Enemy>().enemyHealth);
                        BasicCobraManagement.Instance.StartCoroutine("SpawnBasicCobra", bc.getBeginPosition());
                    }

                    if(other.gameObject.CompareTag("Woods")) {
                        WoodMovement wm= other.gameObject.GetComponent<WoodMovement>();
                        wm.currentState = EnemyStates.enemydie;
                        enemy.GetComponent<Enemy>().dropCoin(wm);
                        WoodManagement.Instance.saveVector(wm.initialPosition, enemy.GetComponent<Enemy>().enemyHealth);
                        WoodManagement.Instance.StartCoroutine("SpawnWoodMovement", wm.getBeginPosition());
                    
                    }

                    if(other.gameObject.CompareTag("ChobinHood")) {
                        ChobinHood ch= other.gameObject.GetComponent<ChobinHood>();
                        ch.currentState = EnemyStates.enemydie;
                        enemy.GetComponent<Enemy>().dropCoin(ch);
                        ChobinHoodManagement.Instance.saveVector(ch.initialPosition, enemy.GetComponent<Enemy>().enemyHealth);
                        ChobinHoodManagement.Instance.StartCoroutine("SpawnChobinHood", ch.getBeginPosition());
                    
                    }

                    if(other.gameObject.CompareTag("ChobinHoodlum")) {
                        ChobinHoodlum chl= other.gameObject.GetComponent<ChobinHoodlum>();
                        chl.currentState = EnemyStates.enemydie;
                        enemy.GetComponent<Enemy>().dropCoin(chl);
                        ChobinHoodlumManagement.Instance.saveVector(chl.initialPosition, enemy.GetComponent<Enemy>().enemyHealth);
                        ChobinHoodlumManagement.Instance.StartCoroutine("SpawnChobinHoodlum", chl.getBeginPosition());
                    
                    }

                    if(other.gameObject.CompareTag("ApeMummy")) {
                        ApeMummy apm = other.gameObject.GetComponent<ApeMummy>();
                        apm.currentState = EnemyStates.enemydie;
                        enemy.GetComponent<Enemy>().dropCoin(apm);
                        ApeMummyManagement.Instance.saveVector(apm.initialPosition, enemy.GetComponent<Enemy>().enemyHealth);
                        ApeMummyManagement.Instance.StartCoroutine("SpawnApeMummy", apm.getBeginPosition());
                    }


                    if(other.gameObject.CompareTag("Skeletons")) {
                        Skeleton skt = other.gameObject.GetComponent<Skeleton>();
                        skt.currentState = EnemyStates.enemydie;
                        enemy.GetComponent<Enemy>().dropCoin(skt);
                        SkeletonsManagement.Instance.saveVector(skt.initialPosition, enemy.GetComponent<Enemy>().enemyHealth);
                        SkeletonsManagement.Instance.StartCoroutine("SpawnSkeleton", skt.getBeginPosition());
                    }


                    if(other.gameObject.CompareTag("TinKnights")) {
                        TinKnight tkt = other.gameObject.GetComponent<TinKnight>();
                        tkt.currentState = EnemyStates.enemydie;
                        enemy.GetComponent<Enemy>().dropCoin(tkt);
                        TinKnightsManagement.Instance.saveVector(tkt.initialPosition, enemy.GetComponent<Enemy>().enemyHealth);
                        TinKnightsManagement.Instance.StartCoroutine("SpawnTinKnight", tkt.getBeginPosition());
                    }


                    if(other.gameObject.CompareTag("CopKnights")) {
                        CopKnight ckt = other.gameObject.GetComponent<CopKnight>();
                        ckt.currentState = EnemyStates.enemydie;
                        enemy.GetComponent<Enemy>().dropCoin(ckt);
                        CopKnightsManagement.Instance.saveVector(ckt.initialPosition, enemy.GetComponent<Enemy>().enemyHealth);
                        CopKnightsManagement.Instance.StartCoroutine("SpawnCopKnight", ckt.getBeginPosition());
                    }
                    
                    ps.gainCurrentEXP(enemy.GetComponent<Enemy>().enemyDropEXP);
                    if(Random.Range(0, 3) == 1) {
                        Instantiate(enemy.GetComponent<Enemy>().drop_food[Mathf.FloorToInt(Random.Range(0, enemy.GetComponent<Enemy>().drop_food.Length))], enemy.GetComponent<Enemy>().transform.position, Quaternion.identity);
                    }
                    enemy.GetComponent<Enemy>().enemyHealthBar.gameObject.SetActive(false);
                }
            }
        }
    }


    void executeKnockBack(Rigidbody2D enemy, Collider2D other) {
        if(enemy != null) {

            if(enemy.GetComponent<Enemy>().prepareDef) {
                enemy.GetComponent<Enemy>().currentState = EnemyStates.enemydef;
            }
            else {
                if(!enemy.GetComponent<Enemy>().invulnerable) {
                    enemy.GetComponent<Enemy>().currentState = EnemyStates.enemystunning;
                }
                
            }

            if(!enemy.GetComponent<Enemy>().invulnerable) {
                Vector3 difference = enemy.transform.position - transform.position;
                difference = difference.normalized * knockbackRange;
                enemy.DOMove(enemy.transform.position + difference, knockbackTime);
               
            }

            StartCoroutine(KnockBack(enemy, other));            
        }
    }


    void hittingBosses(Rigidbody2D boss) {
        PlayerStats ps= this.transform.parent.gameObject.GetComponent<PlayerMovement>().playerStats;
        
        if(boss != null) {
            int health = boss.GetComponent<Enemy>().currentHealth;
            health = boss.GetComponent<Enemy>().currentHealth - ps.getPlayerDamage();
            boss.GetComponent<Enemy>().currentHealth = health;
            boss.GetComponent<Enemy>().enemyHealthBar.setHealth(health);

            if(health <= 0) {
                ps.gainCurrentEXP(boss.GetComponent<Enemy>().enemyDropEXP);
                if(boss.GetComponent<Enemy>().drop_food.Length > 0) {
                    Instantiate(boss.GetComponent<Enemy>().drop_food[0], boss.GetComponent<Enemy>().transform.position, Quaternion.identity);
                }
                
            }
        }
    }
    
}
