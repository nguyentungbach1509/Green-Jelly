using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/*
Handle the final boss's inllusions.
*/
public class FinalBossIllu : MonoBehaviour
{
    
    private Vector2 targetPoint;
    private Vector2 directionPoint;
    private bool duringSkillCooldown;
    private float skillCooldown;
    private float skillTimer;
    private Animator animator;
    public float illuSpeed;
    [SerializeField]
    public GameObject illuObject;
    private float fixedDifferentTime;

    private float livingTime;
    private bool duringLivingTime;
    private float livingTimer;

    private bool noTimeLeft;
    private PlayerMovement playerMovement;
    private Vector2 directionBetween;

    public float knockbackRange;
    public float knockbackTime;

    public int dmg;
    
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        if(GameObject.FindGameObjectWithTag("Player") != null) {
            playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        }

        noTimeLeft = false;
 
    }

    // Update is called once per frame
    void Update()
    {   

        if(noTimeLeft == true) {
            Destroy(gameObject);
        }

        if(playerMovement != null){
            Vector3 newPosition = Vector3.MoveTowards(transform.position, playerMovement.transform.position, illuSpeed * Time.deltaTime);
            directionBetween = (newPosition - transform.position);
        }

        if(GameObject.Find("FinalBoss") == null) {
            noTimeLeft = true;
        }
        
        ControllIlluDirect();
    }


    void FixedUpdate() {
        if(!duringLivingTime) {
            duringLivingTime = true;
            livingTimer = 1;
            
        } 

        if(transform.position.x == targetPoint.x && transform.position.y == targetPoint.y){ 
            if(duringSkillCooldown == false) {
                startSkill();
                skillCooldown = skillCooldown + fixedDifferentTime;
                animator.SetTrigger("Skill");
            }
            else{
                animator.SetTrigger("Idle");
            }
        }
        else {
            transform.position = Vector2.MoveTowards(transform.position, targetPoint, illuSpeed * Time.fixedDeltaTime);
        }

        calculateSkillCooldown();
        calculateLivingTime();
    }


   public void startSkill() {
        duringSkillCooldown = true;
        skillTimer = 1;
    }

    private void calculateSkillCooldown() {
        if(duringSkillCooldown) {

            skillTimer -= 1 / skillCooldown * Time.fixedDeltaTime;

            if(skillTimer <= 0) {
                duringSkillCooldown = false;
                skillTimer = 0;
            }
        }
    }


    public void setTargetPoint(Vector2 point) {
        targetPoint = point;
    }

    public Vector2 getTargetPoint() {
        return targetPoint;
    }

    public void setDirectionPoint(Vector2 point) {
        directionPoint = point;
    }

    public Vector2 getDirectionPoint() {
        return directionPoint;
    }

    private void ControllIlluDirect() {
        animator.SetFloat("Horizontal", directionPoint.x);
        animator.SetFloat("Vertical", directionPoint.y);
    }

    public void SpawnIlluObject() {
        if(playerMovement != null) {
            IlluObject objectIllusion =  (Instantiate(illuObject, transform.position, Quaternion.identity)).GetComponent<IlluObject>();
            objectIllusion.setDirectionObject(new Vector2(directionBetween.x, directionBetween.y));
        }
        
    }

    public void setSkillCooldown(float cdTime) {
        skillCooldown = cdTime;
    }

    public void setDifferentTime(float timeValue) {
        fixedDifferentTime = timeValue;
    }


    private void calculateLivingTime() {
        if(duringLivingTime) {

            livingTimer -= 1 / livingTime * Time.fixedDeltaTime;

            if(livingTimer <= 0) {
                duringLivingTime = false;
                livingTimer = 0;
                noTimeLeft = true;
            }
        }
    }

    public void setLivingTime(float timeValue) {
        livingTime = timeValue;
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
