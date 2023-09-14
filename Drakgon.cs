using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
Controll the Drakgon bosses
*/
public class Drakgon : Enemy
{
    public float attackCoolDown;
    private float attackTimer;
    public bool duringCooldown = false;
    public float shootingCoolDown;
    private float shootingTimer;
    public bool duringShootingCooldown = false;
    public int directNumber;
    public Kamefx kame;
    public bool teleport = false;
    public DragkonIllusions dragkonIllusions;
    public MarkPoint markPoint;
    private List<Vector2> markPositions = new List<Vector2>();
    private Transform playerTransform;
    private int startSecondPhase = 0;
    [SerializeField]
    public GameObject rocks;
    [SerializeField]
    public Camera camera;
    [SerializeField]
    public CameraMovement cameraMovement;
    private Animator animator;
    


    // Start is called before the first frame update
    void Start()
    {
        if(GameObject.FindGameObjectWithTag("Player") != null) {
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        }

        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        calculateAttackCooldown();
        calculateShootingCooldown();

        if(currentHealth <= (enemyHealth / 2) && startSecondPhase == 0) {
            teleport = true;
            startSecondPhase ++;
        } 

        if(currentHealth <= 0 || GameObject.FindGameObjectWithTag("Player") == null) {
            if(currentHealth <= 0 && GameObject.FindGameObjectWithTag("Player") != null){
                PlayerMovement playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
                playerMovement.playerStats.getQuests()[enemyName] = 1;
            }
            animator.SetTrigger("Die");
        }
    }


    public void startAttack() {
        duringCooldown = true;
        attackTimer = 1;
    }

    public void startShooting() {
        duringShootingCooldown = true;
        shootingTimer = 1;
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


    private void calculateShootingCooldown() {
        if(duringShootingCooldown) {

            shootingTimer -= 1 / shootingCoolDown * Time.deltaTime;

            if(shootingTimer <= 0) {
                duringShootingCooldown = false;
                shootingTimer = 0;
            }
        }
    }



    public void SpawnKame() {

        switch(directNumber) {
            case 0:
                ((Instantiate(kame, new Vector2(transform.position.x + 3.52f, transform.position.y), Quaternion.identity)).GetComponent<Kamefx>()).direction = Vector2.right;
                break;
            case 1:
                ((Instantiate(kame, new Vector2(transform.position.x - 3.52f, transform.position.y), Quaternion.identity)).GetComponent<Kamefx>()).direction = Vector2.left;
                break;
            case 2:
                ((Instantiate(kame, new Vector2(transform.position.x, transform.position.y + 3.839f), Quaternion.identity)).GetComponent<Kamefx>()).direction = Vector2.up;
                break;
            case 3:
                ((Instantiate(kame, new Vector2(transform.position.x, transform.position.y - 1.63f), Quaternion.identity)).GetComponent<Kamefx>()).direction = Vector2.down;
                break;
             
        }
    }


    public void SpawnIllus() {
        switch(directNumber) {
            case 0:
                ((Instantiate(dragkonIllusions, new Vector2(transform.position.x, transform.position.y + 6.63f), Quaternion.identity)).GetComponent<DragkonIllusions>()).illuDirect = Vector2.right;
                ((Instantiate(dragkonIllusions, new Vector2(transform.position.x, transform.position.y - 6.63f), Quaternion.identity)).GetComponent<DragkonIllusions>()).illuDirect = Vector2.right;
                break;
            case 1:
                ((Instantiate(dragkonIllusions, new Vector2(transform.position.x, transform.position.y + 6.63f), Quaternion.identity)).GetComponent<DragkonIllusions>()).illuDirect = Vector2.left;
                ((Instantiate(dragkonIllusions, new Vector2(transform.position.x, transform.position.y - 6.63f), Quaternion.identity)).GetComponent<DragkonIllusions>()).illuDirect = Vector2.left;
                break;
            case 2:
                ((Instantiate(dragkonIllusions, selectIlluPosition()[0], Quaternion.identity)).GetComponent<DragkonIllusions>()).illuDirect = Vector2.up;
                 ((Instantiate(dragkonIllusions, selectIlluPosition()[1], Quaternion.identity)).GetComponent<DragkonIllusions>()).illuDirect = Vector2.up;
                break;
            case 3:
                ((Instantiate(dragkonIllusions, selectIlluPosition()[0], Quaternion.identity)).GetComponent<DragkonIllusions>()).illuDirect = Vector2.down;
                ((Instantiate(dragkonIllusions, selectIlluPosition()[1], Quaternion.identity)).GetComponent<DragkonIllusions>()).illuDirect = Vector2.down;
                break;
             
        }

    }


    public void positionApperance() {
        transform.position = new Vector2(transform.position.x, 33f);
    }


    private Vector2[] selectIlluPosition() {
        if(transform.position.x < 2.5f) {
            return new Vector2[] {
                new Vector2(transform.position.x + 20f, transform.position.y),
                new Vector2(transform.position.x + 10f, transform.position.y)
            };
        }
        else if(transform.position.x >= 2.5f && transform.position.x <= 11.5f) {
            return new Vector2[] {
                new Vector2(transform.position.x + 10f, transform.position.y),
                new Vector2(transform.position.x - 10f, transform.position.y)
            };
        }
        
        return new Vector2[] {
                new Vector2(transform.position.x - 20f, transform.position.y),
                new Vector2(transform.position.x - 10f, transform.position.y)
            };
    }


    public void SpawnMarkPoints() {

        storeRandomPoints();
        for(int i = 0; i < markPositions.Count; i++){
            MarkPoint mp = (Instantiate(markPoint, markPositions[i], Quaternion.identity)).GetComponent<MarkPoint>();
            mp.StartCoroutine("destroyPoint");
           
        }
    }


    private void storeRandomPoints() {
        markPositions.Add(playerTransform.position);

        while(markPositions.Count < 15) {
            Vector2 tempVector = new Vector2(Random.Range(-8.5f, 21.5f), Random.Range(24.5f, 39.5f));

            if(!checkPointsDistance(markPositions, tempVector) && !containPoint(markPositions, tempVector)) {
                markPositions.Add(tempVector);
            }

        }
    }


    private bool checkPointsDistance(List<Vector2> list, Vector2 vectorPosition) {
        for(int i = 0; i < list.Count; i ++) {
            if(Vector2.Distance(list[i], vectorPosition) <= 4.5) {
                return true;
            }
        }

        return false;

    }


    private bool containPoint(List<Vector2> list, Vector2 vectorPosition) {
        for(int i = 0; i < list.Count; i ++) {
            if(vectorPosition == list[i]) {
                return true;
            }
        }

        return false;
    }

    public void returnSpawnPoints() {
        markPositions = new List<Vector2>();
    }


    public void showHealthBar() {
        enemyHealthBar.setMaxHealth(enemyHealth);
        enemyHealthBar.setHealth(currentHealth);
        enemyHealthBar.gameObject.SetActive(true);
        enemyHealthBar.setText(enemyName, enemyLevel);
    }

    public void DrakgonBossDie() {
        AudioManager audioManager = FindObjectOfType<AudioManager>();
        enemyHealthBar.gameObject.SetActive(false);
        rocks.SetActive(false);
        audioManager.StopSound(enemyName);
        camera.orthographicSize = 5f;
        cameraMovement.maxBorder = new Vector2(18.29f, 39.72f);
        cameraMovement.minBorder = new Vector2(-5.22f, 27.03f);
        gameObject.SetActive(false);
    }

}
