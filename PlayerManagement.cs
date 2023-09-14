using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManagement : MonoBehaviour
{

     public static PlayerManagement Instance = null;
     private int[] intStats = new int[4];
     private float[] floatStats = new float[3];
     private PlayerStats ps;
     private PlayerMovement pm;

    //GameObject
    [SerializeField]
    GameObject player;
    [SerializeField]
    GameObject levelBoard;


    //Image UI
    [SerializeField]
    Image atkSkill;
    [SerializeField]
    Image dodgeSkill;
    [SerializeField]
    Image frontHealth;
    [SerializeField]
    Image backHealth;
    [SerializeField]
    Image frontExp;
    [SerializeField]
    Image backExp;


    //Text UI
    [SerializeField]
    Text level;
    [SerializeField]
    Text coin;
    [SerializeField]
    Text atk;
    [SerializeField]
    Text def;
    [SerializeField]
    Text health;


    void Awake()
	{
		if (Instance == null) {
            Instance = this;
        }
		else if (Instance != this){
            Destroy (gameObject);
        }	
		
	}

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(player, new Vector2 (-9999f, -9999f), player.transform.rotation);
    
    }

    // Update is called once per frame
    IEnumerator SpawnPlayer(Vector2 spawnPosition)
	{
		yield return new WaitForSeconds (1f);
		pm = (Instantiate (player, spawnPosition, player.transform.rotation)).GetComponent<PlayerMovement>();
        setNewPlayer(pm);
	}


    public void savePlayerStats(PlayerStats pss, int[] intArray, float[] floatArray) {
        ps = pss;
        intStats = intArray;
        floatStats = floatArray;
    }

    void setNewPlayer(PlayerMovement playerMovement) {

        

        playerMovement.playerStats.setPlayerDamage(intStats[0]);
        playerMovement.playerStats.setPlayerArmor(intStats[1]);
        playerMovement.playerStats.setPlayerLevel(intStats[2]);
        playerMovement.playerStats.setPlayerGold(intStats[3]);

        playerMovement.playerStats.setPlayerMaxHealth(floatStats[0]);
        playerMovement.playerStats.setPlayerCurrentHealth(floatStats[0]);
        playerMovement.playerStats.setPlayerCurrentEXP(floatStats[1]);
        playerMovement.playerStats.setPlayerTotalEXP(floatStats[2]);


        playerMovement.playerStats.frontFillHealth = frontHealth;
        playerMovement.playerStats.backFillHealth = backHealth;
        playerMovement.playerStats.frontFillEXP = frontExp;
        playerMovement.playerStats.backFillEXP = backExp;
        playerMovement.atkSkillImage = atkSkill;
        playerMovement.rollSkillImage = dodgeSkill;

        playerMovement.playerStats.notiBoard = levelBoard;
        playerMovement.playerStats.levelNumber = level;
        playerMovement.playerStats.attackPoint = atk;
        playerMovement.playerStats.defencePoint = def;
        playerMovement.playerStats.healthPoint = health;
        playerMovement.playerStats.goldUI = coin;


        Debug.Log(playerMovement.playerStats.getPlayerCurrentHealth() + "");
        
    }
}
