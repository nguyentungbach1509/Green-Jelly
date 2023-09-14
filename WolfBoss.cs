using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/*
Control and handle the wolfboss
*/
public class WolfBoss : Enemy
{
    public float attackCoolDown;
    private float attackTimer;
    public bool duringCooldown = false;
    public WolfEnergyBallManagement wbm;
    public WolfEnergyBallUpgrade energyBall;
    private CircleCollider2D wolfCollider;
    
    public int skill3_dmg;
    public float skillKnockbackRange;
    public float skillKnockbackTime;

    public float strikeCooldown;
    private float strikeTimer;
    private bool duringStrikeCooldown;
    public bool useThirdSkill= false;
    public bool stateToIdle;

    private SlashPoint[] slashPointObjects;
    private Vector2 saveVector;

    private bool startToJump;

    public ColumnEnergy lighningBolt;
    private IDictionary<string,Vector2> slashPoints = new Dictionary<string, Vector2>();

    private Vector2 trackTheTarget;
    
    public float trackCooldown;
    private float trackTimer;
    private bool duringTrackCooldown; 
    private bool touchPlayer;

    public float castSkillCooldown;
    private float castSkillTimer;
    public bool duringCastSkillCooldown; 

    private PlayerMovement playerMovement;

    public RemainIllusion remainIllu;

    [SerializeField]
    public Camera camera;
    [SerializeField]
    public CameraMovement cameraMovement;

    private Animator animator;

    [SerializeField]
    public GameObject rockBlocks;
    [SerializeField]
    public GameObject smallRockBlocks;

    // Start is called before the first frame update
    void Start()
    {
        wolfCollider = GetComponent<CircleCollider2D>();
        slashPointObjects = GetComponentsInChildren<SlashPoint>(true);
        animator = GetComponent<Animator>();
        if(GameObject.FindGameObjectWithTag("Player") != null) {
            playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        }
        slashPoints["upleft"] = new Vector2(-20f,3f);
        slashPoints["upright"] = new Vector2(24f,3f);
        slashPoints["downleft"] = new Vector2(-20f,-30f);
        slashPoints["downright"] = new Vector2(24f,-30f);
        
    }

    // Update is called once per frame
    void Update()
    {   

        if(GameObject.FindGameObjectWithTag("Player") == null || currentHealth <= 0) {
            if(currentHealth <= 0 && GameObject.FindGameObjectWithTag("Player") != null) {
                playerMovement.playerStats.getQuests()[enemyName] = 1;
                animator.SetTrigger("Die");
            }
            else {
                WolfBossDie();
            }

        }

        calculateAttackCooldown();
        calculateStrikeCooldown();
        calculateTrackCooldown();
        calculateCastSkillCooldown();
        ShowCountSlash();
    }


    void FixedUpdate() {
        if(useThirdSkill) {
            if(!duringStrikeCooldown) {
                duringStrikeCooldown = true;
                strikeTimer = 1;
                StartCoroutine(SpawnLighningBolt());
            }
        }
        
    }


    public void startAttack() {
        duringCooldown = true;
        attackTimer = 1;
    }

    public void startCastSkill() {
        duringCastSkillCooldown = true;
        castSkillTimer = 1;
    }

    public void startSkill4() {  
        ((Instantiate(remainIllu, transform.position, Quaternion.identity)).GetComponent<RemainIllusion>()).illuDirect = getSaveVector(); 
        transform.position = new Vector3(transform.position.x, transform.position.y, -100f);
        duringTrackCooldown = true;
        trackTimer = 1;
    }

    public void SpawnWoflEnergy() {
        Instantiate(wbm, transform.position, Quaternion.identity);
    }


    public void SpawnWoflEnergyUpgrade() {
        
        Instantiate(energyBall, transform.position, Quaternion.identity);
    
    }

    
    private void ShowCountSlash() {
        foreach (SlashPoint sp in slashPointObjects)
        {   
            if(!sp.gameObject.active) {
                sp.setSpawnSlash(false);
            }
        }
    }


    private void calculateAttackCooldown() {
        if(duringCooldown) {

            attackTimer -= 1 / attackCoolDown * Time.deltaTime;

            if(attackTimer <= 0) {
                duringCooldown = false;
                attackTimer = 0;
            }
        }
    }


    private void calculateStrikeCooldown() {
        if(duringStrikeCooldown) {

            strikeTimer -= 1 / strikeCooldown * Time.deltaTime;

            if(strikeTimer <= 0) {
                duringStrikeCooldown = false;
                strikeTimer = 0;
            }
        }
        
    }


    private void calculateTrackCooldown() {
        if(duringTrackCooldown) {

            trackTimer -= 1 / trackCooldown * Time.deltaTime;

            if(trackTimer <= 0) {
                duringTrackCooldown = false;
                trackTimer = 0;
            }

            if(playerMovement != null) {
                trackTheTarget = playerMovement.transform.position;
            }
        }
    }


    private void calculateCastSkillCooldown() {
        if(duringCastSkillCooldown) {

            castSkillTimer -= 1 / castSkillCooldown * Time.deltaTime;

            if(castSkillTimer <= 0) {
                duringCastSkillCooldown = false;
                castSkillTimer = 0;
            }
        }
        
    }


    public void changeWolfCollider(bool param, float rad) {
        
        wolfCollider.radius = rad;
        wolfCollider.isTrigger = param;
        
    }

    
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player")) {

            Rigidbody2D pl = other.gameObject.GetComponent<Rigidbody2D>();

            if(pl != null && useThirdSkill) {
                
                pl.GetComponent<PlayerMovement>().setState(1);
                Vector3 difference = pl.transform.position - transform.position;
                difference = difference.normalized * (skillKnockbackRange);
                pl.DOMove(pl.transform.position - difference, skillKnockbackTime);
                StartCoroutine(KnockBackPlayer(pl));
            }
        }
    }

    private IEnumerator KnockBackPlayer(Rigidbody2D player) {
        if(player != null) {
            
            PlayerMovement pm = player.GetComponent<PlayerMovement>();
            yield return new WaitForSeconds(skillKnockbackTime);
            player.velocity = Vector2.zero;
            pm.playerStats.setPlayerTakeDamage(skill3_dmg);

            if(pm.playerStats.getPlayerCurrentHealth() > 0) {

                pm.setState(0);
            }
            else {
                pm.setState(2);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        
        if(other.gameObject.CompareTag("Environment")) {
            if(useThirdSkill) {
                stateToIdle = true;
                useThirdSkill = false;
                StartCoroutine(PreventBePushed());
            }
        }
    }

    public void UsingThirdSkill(){
        useThirdSkill = true;
    }


    private IEnumerator SpawnLighningBolt() {
        yield return new WaitForSeconds(.25f);
        Instantiate(lighningBolt, transform.position, Quaternion.identity);
    }


    private IEnumerator PreventBePushed() {
        gameObject.transform.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        yield return new WaitForSeconds(.2f);
        gameObject.transform.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
    }


    public IDictionary<string, Vector2> getSlashPoints() {
        return slashPoints;
    }

    public void UpdateSlashPoint(string name, Vector2 point) {
        slashPoints[name] = point;
    }


    public Vector2 getSaveVector() {
        return saveVector;
    }

    public void storeVector(Vector2 vect) {
        saveVector = vect;
    }

    public void JumpOn() {
        startToJump = true;
    }

    public bool getStartToJump() {
        return startToJump;
    }

    public bool getDuringTrackCooldown() {
        return duringTrackCooldown;
    }

    public Vector2 getTrackTarget(){
        return trackTheTarget;
    }

    public void ResetTrackTarget() {
        trackTheTarget = Vector2.zero;
    }
    
    
    public void showHealthBar() {
        enemyHealthBar.setMaxHealth(enemyHealth);
        enemyHealthBar.setHealth(currentHealth);
        enemyHealthBar.gameObject.SetActive(true);
        enemyHealthBar.setText(enemyName, enemyLevel);
    }


    public void setTouchPlayer(bool val) {
        touchPlayer = val;
    }

    public bool getTouchPlayer() {
        return touchPlayer;
    }


    public void WolfBossDie() {
        AudioManager audioManager = FindObjectOfType<AudioManager>();
        enemyHealthBar.gameObject.SetActive(false);
        rockBlocks.SetActive(false);
        smallRockBlocks.SetActive(false);
        audioManager.StopSound(enemyName);
        camera.orthographicSize = 5f;
        cameraMovement.maxBorder = new Vector2(18.26f,3.7f);
        cameraMovement.minBorder = new Vector2(-14.2f, -38f);
        gameObject.SetActive(false);
    }


}
