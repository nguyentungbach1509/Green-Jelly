using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
Controll the final boss
*/
public class FinalBoss : Enemy
{
    
    [SerializeField]
    public FinalBossNormalAttackObject ball;
    [SerializeField]
    public Beamfx beam;
    [SerializeField]
    public FinalBossNormalAttackObject atkObject;
    [SerializeField]
    public TeleExplosion explosionTele;
     [SerializeField]
    public TeleExplosion explosionTele_p2;
    [SerializeField]
    public EnergyObject energyObject;
    [SerializeField]
    public GameObject auraObject;
    [SerializeField]
    public FinalBossIllu finalBossIllu;
    [SerializeField]
    public Skill3_Illusions skillIllusion;
    [SerializeField]
    public IlluObject finalBossIlluObject;
    [SerializeField]
    public GameObject rocks;
    [SerializeField]
    public Camera camera;
    [SerializeField]
    public CameraMovement cameraMovement;

    public bool duringAttackCooldown;
    public float attackCooldown;
    private float attackTimer;

    public bool duringDashCooldown;
    public float dashCooldown;
    private float dashTimer;

    public bool duringBeamCooldown;
    public float beamCooldown;
    private float beamTimer;

    public bool duringSkill2Cooldown;
    public float skill2Cooldown;
    private float skill2Timer;

    private Vector2 teleportPoint;
    private CircleCollider2D bossCollider;
    private bool destroyAura;
    private GameObject aura;
    private Vector2 directToSpawnObject;
    private List<Vector2[]> targetPoints;
    
    public float coolDownIlluSkill;
   

    public float teleMovementCooldown;
    public bool duringTeleMovementCooldown;
    private float teleMovementTimer;

    public float skill3Cooldown;
    private float skill3Timer;
    public bool duringSkill3Cooldown;

    public bool duringSkill2Phase2Cooldown;
    public float skill2Phase2Cooldown;
    private float skill2Phase2Timer;

    private Animator animator;

    
    // Start is called before the first frame update
    void Start()
    {   
        animator = GetComponent<Animator>();
        bossCollider = GetComponent<CircleCollider2D>();
        targetPoints = new List<Vector2[]>();
        targetPoints.Add(new Vector2[]{new Vector2(-78f, 11f), Vector2.left});
        targetPoints.Add(new Vector2[]{new Vector2(-43f, 11f), Vector2.right});
        targetPoints.Add(new Vector2[]{new Vector2(-78f, 23f), new Vector2(-1,1)});
        targetPoints.Add(new Vector2[]{new Vector2(-43f, 23f), new Vector2(1,1)});
        targetPoints.Add(new Vector2[]{new Vector2(-43f, 0.5f), new Vector2(1,-1)});
        targetPoints.Add(new Vector2[]{new Vector2(-78f, 0.5f), new Vector2(-1,-1)});
        
    }

    // Update is called once per frame
    void Update()
    {
        if(GameObject.FindGameObjectWithTag("Player") == null || currentHealth <= 0) {
            if(currentHealth <= 0 && GameObject.FindGameObjectWithTag("Player") != null) {
                PlayerMovement pm = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
                pm.playerStats.getQuests()[enemyName] = 1;
            }
            animator.SetTrigger("Die");
        }

    

        calculateAttackCooldown();
        calculateDashCooldown();
        calculateBeamCooldown();
        calculateSkill2Cooldown();
        calculateTeleMovementCooldown();
        calculateSkill3Cooldown();
        calculateSkill2Phase2Cooldown();
    }


    public void startAttack() {
        duringAttackCooldown = true;
        attackTimer = 1;
    }

    private void calculateAttackCooldown() {
        if(duringAttackCooldown) {

            attackTimer -= 1 / attackCooldown * Time.deltaTime;

            if(attackTimer <= 0) {
                duringAttackCooldown = false;
                attackTimer = 0;
            }
        }
    }


    public void startDash() {
        duringDashCooldown = true;
        dashTimer = 1;
    }

    private void calculateDashCooldown() {
        if(duringDashCooldown) {

            dashTimer -= 1 / dashCooldown * Time.deltaTime;

            if(dashTimer <= 0) {
                duringDashCooldown = false;
                dashTimer = 0;
            }
        }
    }


    public void startBeam() {
        duringBeamCooldown = true;
        beamTimer = 1;
    }

    private void calculateBeamCooldown() {
        if(duringBeamCooldown) {

            beamTimer -= 1 / beamCooldown * Time.deltaTime;

            if(beamTimer <= 0) {
                duringBeamCooldown = false;
                beamTimer = 0;
            }
        }
    }


    public void startSkill2() {
        duringSkill2Cooldown = true;
        skill2Timer = 1;
    }

    private void calculateSkill2Cooldown() {
        if(duringSkill2Cooldown) {

            skill2Timer -= 1 / skill2Cooldown * Time.deltaTime;

            if(skill2Timer <= 0) {
                duringSkill2Cooldown = false;
                skill2Timer = 0;
            }
        }
    }


    public void startSkill2Phase2() {
        duringSkill2Phase2Cooldown = true;
        skill2Phase2Timer = 1;
    }

    private void calculateSkill2Phase2Cooldown() {
        if(duringSkill2Phase2Cooldown) {

            skill2Phase2Timer -= 1 / skill2Phase2Cooldown * Time.deltaTime;

            if(skill2Phase2Timer <= 0) {
                duringSkill2Phase2Cooldown = false;
                skill2Phase2Timer = 0;
            }
        }
    }


    public void startTeleMovement() {
        duringTeleMovementCooldown = true;
        teleMovementTimer = 1;
    }

    private void calculateTeleMovementCooldown() {
        if(duringTeleMovementCooldown) {

            teleMovementTimer -= 1 / teleMovementCooldown * Time.deltaTime;

            if(teleMovementTimer <= 0) {
                duringTeleMovementCooldown = false;
                teleMovementTimer = 0;
            }
        }
    }


    public void startSkill3() {
        duringSkill3Cooldown = true;
        skill3Timer = 1;
    }

    private void calculateSkill3Cooldown() {
        if(duringSkill3Cooldown) {

            skill3Timer -= 1 / skill3Cooldown * Time.deltaTime;

            if(skill3Timer <= 0) {
                duringSkill3Cooldown = false;
                skill3Timer = 0;
            }
        }
    }


    public void SpawnBall() {
        Instantiate(ball, transform.position, Quaternion.identity);
    }

    public void SpawnEnergyObjectFistPhase() {
        float angles = 0f;
        for(int i = 0; i < 3; i ++) {
            EnergyObject ob = (Instantiate(energyObject, transform.position, Quaternion.identity)).GetComponent<EnergyObject>();
            ob.gameObject.transform.parent = transform;
            angles += 90f;
            ob.objectAngle = angles;
        }
        
    }

    public void SpawnAtackObjectPhase2() {
        (Instantiate(atkObject, transform.position, Quaternion.identity)).GetComponent<FinalBossNormalAttackObject>().direction = getDirectToSpawnObject();
    }

    public void SpawnAura() {
       aura = Instantiate(auraObject, new Vector2(transform.position.x, transform.position.y - .5f), Quaternion.identity) as GameObject;
        
    }

    public void SpawnSkillIllusions() {
        Instantiate(skillIllusion, transform.position, Quaternion.identity);
    }

    public void setTeleportPoint(Vector2 point) {
        teleportPoint = point;
    }

    public Vector2 getTeleportPoint() {
        return teleportPoint;
    }

    public void switchColliderTrigger() {
        bossCollider.isTrigger = !bossCollider.isTrigger;
    }

    public void ExplosionTeleport() {
        Instantiate(explosionTele, transform.position, Quaternion.identity);
    }

    public void ExplosionTeleportPhase2() {
        Instantiate(explosionTele_p2, transform.position, Quaternion.identity);
    }

    public void TeleportSkill2() {
        transform.position = new Vector2(-60.5f, 20f);
    }


    public void TeleportSkill1() {
        transform.position = new Vector2(-60.5f, 11f);
    }


    public void SpawnBeam(float difference, int directNo) {
        Vector3 rotationVector = transform.rotation.eulerAngles;
        Vector2 startPoint =  Vector2.zero;
        switch(directNo) {
            case 0:
                startPoint = new Vector2(-77f, difference);
                rotationVector.z = 0;
                break;
            case 1:
                startPoint = new Vector2(-44f, difference);
                rotationVector.z = 180;
                break;
            case 2:
                startPoint = new Vector2(difference, 23f);
                rotationVector.z = -90;
                break;
        }

        beam.transform.rotation = Quaternion.Euler(rotationVector);
        Instantiate(beam, startPoint, beam.transform.rotation);
        
    }


    public void SpawnIllusionSkill2() {
        ShuffleList(targetPoints);
        startSkill2Phase2();
        for(int i = 0; i < 6; i ++) {
            FinalBossIllu fnillu = (Instantiate(finalBossIllu, transform.position, Quaternion.identity)).GetComponent<FinalBossIllu>();
            fnillu.setTargetPoint(targetPoints[i][0]);
            fnillu.setDirectionPoint(targetPoints[i][1]);
            fnillu.setDifferentTime(coolDownIlluSkill * 6);
            fnillu.setSkillCooldown(coolDownIlluSkill * (i + 1));
            fnillu.setLivingTime(skill2Phase2Cooldown);
            if(i > 0) {
                fnillu.startSkill();
            }
        }
    }


    public void SpawnSkill3Object() {
        IlluObject objectIllusion =  (Instantiate(finalBossIlluObject, transform.position, Quaternion.identity)).GetComponent<IlluObject>();
        objectIllusion.setDirectionObject(getDirectToSpawnObject());
    }



    public void willDestroyAura() {
        if(destroyAura == false) {
            destroyAura = true;
            Destroy(aura);
        }
        else {
            destroyAura = false;
        }
        
    }


    public bool getDestroyAura() {
        return destroyAura;
    }


    public Vector2 getDirectToSpawnObject(){
        return directToSpawnObject;
    }

    public void setDirectToSpawnObject(Vector2 paramsDirect) {
        directToSpawnObject = paramsDirect;
    }


    private void ShuffleList<T>(IList<T> aList) {
        int size = aList.Count;  
        while (size > 1) {  
            size--;  
            int randomIndex = Mathf.FloorToInt(Random.Range(0, size+1));  
            T value = aList[randomIndex ];  
            aList[randomIndex ] = aList[size];  
            aList[size] = value;  
        }
    }

    public void FinalBossBeKilled() {
        AudioManager audioManager = FindObjectOfType<AudioManager>();
        enemyHealthBar.gameObject.SetActive(false);
        rocks.SetActive(false);
        audioManager.StopSound(enemyName);
        camera.orthographicSize = 5f;
        cameraMovement.maxBorder = new Vector2(-17.84f, 21.15f);
        cameraMovement.minBorder = new Vector2(-76f, -19.5f);
        gameObject.SetActive(false);
        EnergyObject[] energyObjects = GetComponentsInChildren<EnergyObject>(true); 
        if(energyObjects.Length > 0) {
            foreach(EnergyObject energy in energyObjects){
                Destroy(energy.gameObject);
            }

            willDestroyAura();
        }

    }


    public void showHealthBar() {
        enemyHealthBar.setMaxHealth(enemyHealth);
        enemyHealthBar.setHealth(currentHealth);
        enemyHealthBar.gameObject.SetActive(true);
        enemyHealthBar.setText(enemyName, enemyLevel);
    }
}
