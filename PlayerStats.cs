using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
The class to handle all stats of the player such as health points, attack points, levels and so on.
*/
public class PlayerStats : MonoBehaviour
{

    public static PlayerStats instance;

    public PlayerMovement pm;

    //Healthbar system
    private float playerMaxHealth = 1800f;
    private float playerCurrentHealth;
    public Image frontFillHealth;
    public Image backFillHealth;

    private float lerpTimeHealth;
    private float speedTime = 2f;
    


    private int playerDamage =500;
    private int playerArmor= 25;
    private int playerGold = 0;

    //Level and EXP system
    public Text levelNumber;
    private float playerCurrentEXP = 0f;
    private float playerTotalEXP;
    private int playerLevel = 35;
    private float timeDelay;
    public Image frontFillEXP;
    public Image backFillEXP;
    private float lerpTimeExp;

    [Range(1f, 300f)]
    public float addNumber = 300;
    [Range(2f, 4f)]
    public float powerNumber = 2;
    [Range(7f, 14f)]
    public float divisionNumber = 7;


    //Levelup Notification
    public GameObject notiBoard;
    public Text attackPoint;
    public Text defencePoint;
    public Text healthPoint;


    //Stats 
    public Text statsAtkPoint;
    public Text statsDefPoint;
    public Text statsHealthPoint;
    public Text statsExp;
    public Text statsGold;
    public Text statsLevel;

    public Text swordStatsLevel;
    public Text swordStatsDamge;



    //Gold System
    public Text goldUI;


    //Poison status
    public bool gotPoison = false;
    private int dmgOverTime;
    private float dmgTimer;
    public float dmgCooldown;
    public float countTime;
    private bool duringCooldown;

    //in Kame area
    private bool inArea = false;
    private int damagePerSec;
    private float dmgKameTimer;
    public float dmgKameCooldown;
    private bool duringKameCooldown;

    //Weapon
    private int swordLevel=0;
    private int swordDmg = 10;

    //Quests
    private Dictionary<string, int> quests = new Dictionary<string, int>();

    //Difficult
    private int difficult;


    void Awake() {

        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
    
        }
        else
        {
            if(instance != this)
            {
                Destroy(gameObject);
            }
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        if(difficult==1 && playerMaxHealth == 100) {
            playerMaxHealth = playerMaxHealth-20;
            playerDamage = playerDamage - 3;
        }
        playerCurrentHealth = playerMaxHealth;
        levelNumber.text = playerLevel + "";
        playerTotalEXP = updatePlayerTotalEXP();
        frontFillEXP.fillAmount = playerCurrentEXP / playerTotalEXP;
        backFillEXP.fillAmount = playerCurrentEXP / playerTotalEXP;
        goldUI.text = playerGold + "G";
        
        
    }

    // Update is called once per frame
    void Update()
    {
        playerCurrentHealth = Mathf.Clamp(playerCurrentHealth, 0, playerMaxHealth);
        HealthBarAnimation();
        EXPBarAnimation();
        if(playerCurrentEXP >= playerTotalEXP) {
            LevelUp();
        }

        if(playerLevel >= 10 && quests.ContainsKey("Get Stronger")) {
            if(quests["Get Stronger"] != -1) {
                quests["Get Stronger"] = 1;
            } 
        }

        if(gotPoison) {
            if(playerCurrentHealth <= 0) {
                pm.setState(2);
            }
        }

        if(inArea) {
            if(playerCurrentHealth <= 0) {
                pm.setState(2);
            }
        }

        calculateCooldown();
        calculateKameCooldown();
    
    }


    void FixedUpdate() {
        if(gotPoison && countTime <= 5) {
            if(!duringCooldown) {
                duringCooldown = true;
                dmgTimer = 1;
                countTime++;
                setPlayerTakeDamage(dmgOverTime);
            } 
        }
        else {
            pm.moveSpeed = 2.5f;
            countTime = 0;
            gotPoison = false;
        }

        if(inArea) {
            if(!duringKameCooldown) {
                duringKameCooldown = true;
                dmgKameTimer = 1;
                setPlayerTakeDamage(damagePerSec);
            } 
        }
    }



    private void calculateCooldown() {
        if(duringCooldown) {

            dmgTimer -= 1 / dmgCooldown * Time.deltaTime;

            if(dmgTimer <= 0) {
                duringCooldown = false;
                dmgTimer = 0;
            }
        }
    }


    private void calculateKameCooldown() {
        if(duringKameCooldown) {

            dmgKameTimer -= 1 / dmgKameCooldown * Time.deltaTime;

            if(dmgKameTimer <= 0) {
                duringKameCooldown = false;
                dmgKameTimer = 0;
            }
        }
    }


    public void HealthBarAnimation() {

        float frontFill = frontFillHealth.fillAmount;
        float backFill = backFillHealth.fillAmount;
        float fractionHealth = playerCurrentHealth / playerMaxHealth;
        if(backFill > fractionHealth) {

            frontFillHealth.fillAmount = fractionHealth;
            backFillHealth.color = Color.red;
            lerpTimeHealth += Time.deltaTime;
            float percent = lerpTimeHealth / speedTime;
            percent = percent * percent;
            backFillHealth.fillAmount = Mathf.Lerp(backFill, fractionHealth, percent);
        }

        if(frontFill < fractionHealth) {

            backFillHealth.fillAmount = fractionHealth;
            backFillHealth.color = new Color32(13, 60, 236, 255);
            lerpTimeHealth += Time.deltaTime;
            float percent = lerpTimeHealth / speedTime;
            percent = percent * percent;
            frontFillHealth.fillAmount = Mathf.Lerp(frontFill, backFillHealth.fillAmount, percent);
        }
    }


    public void EXPBarAnimation() {
        float fractionEXP = playerCurrentEXP / playerTotalEXP;
        float tempEXP = frontFillEXP.fillAmount;

        if(tempEXP < fractionEXP) {
            
            timeDelay += Time.deltaTime;
            backFillEXP.fillAmount = fractionEXP;

            if(timeDelay > 3) {
                lerpTimeExp += Time.deltaTime;
                float percent = lerpTimeExp / 4;
                frontFillEXP.fillAmount = Mathf.Lerp(tempEXP, backFillEXP.fillAmount, percent);
            }
        } 
    }


    public void LevelUp() {
        playerLevel ++;
        levelNumber.text = playerLevel + "";
        frontFillEXP.fillAmount = 0f;
        backFillEXP.fillAmount = 0f;
        playerCurrentEXP = Mathf.RoundToInt(playerCurrentEXP - playerTotalEXP);
        StartCoroutine(ShowNotification(playerDamage, playerArmor, playerMaxHealth));
        updateMaxHealth();
        playerDamage += 5;
        if(playerLevel % 3 == 0) {
            playerArmor += 2;
        }
        playerTotalEXP = updatePlayerTotalEXP();
  
    }

    private IEnumerator ShowNotification(int dmg, int def, float health) {
        notiBoard.SetActive(true);
        attackPoint.text = "Attack: " + dmg + " + 5";
        if(playerLevel % 3 == 0) {
            defencePoint.text = "Defence: " + def + " + 2";
        }
        else {
            defencePoint.text = "Defence: " + def;
        }
        
        if(difficult == 0) {
            healthPoint.text = "Health: " + health + " + 50";
        }
        else {
            healthPoint.text = "Health: " + health + " + 30";
        }
        yield return new WaitForSeconds(3.5f);
        notiBoard.SetActive(false);

    }
    
    public void setPlayerTakeDamage(int dmg) {
        if((dmg - playerArmor) > 0) {
            this.playerCurrentHealth -= (dmg - playerArmor);
        }
        else {

            this.playerCurrentHealth -= 1;
        }
        
        lerpTimeHealth = 0f;
        
    }


    public void setPlayerRestoreHealth(int health) {
        this.playerCurrentHealth += health;
        lerpTimeHealth = 0f;
    }

    public void gainCurrentEXP(float exp) {
        this.playerCurrentEXP += exp;
        lerpTimeExp = 0;
    }


    public void updateMaxHealth() {
        if(difficult==0){
            playerMaxHealth += 50;
        }
        else {
            playerMaxHealth += 30;
        }
        
        playerCurrentHealth = playerMaxHealth;
    }


    public void gainPlayerGold(int gold) {
        this.playerGold += gold;
        goldUI.text = this.playerGold + "G";
    }

    public void usePlayerGold(int gold) {
        if((this.playerGold - gold) >= 0) {
            this.playerGold -= gold;
            goldUI.text = this.playerGold + "G";
        }
        
    }


    public int updatePlayerTotalEXP() {
        
        int requiredXp = 0;
        
        for(int i = 1 ; i <= playerLevel; i++) {

            requiredXp += (int)Mathf.Floor(i + addNumber * Mathf.Pow(powerNumber, (i/divisionNumber)));
        }

        return requiredXp / 4;
    }



    //Get stats
    public float getPlayerMaxHealth() {
        return this.playerMaxHealth;
    }

    public float getPlayerCurrentHealth() {
        return this.playerCurrentHealth;
    }

    public int getPlayerDamage() {
        return this.playerDamage;
    }

    public int getPlayerArmor() {
        return this.playerArmor;
    }

    public int getPlayerGold() {
        return this.playerGold;
    }

    public float getPlayerCurrentEXP() {
        return this.playerCurrentEXP;
    }

    public float getPlayerTotalEXP() {
        return this.playerTotalEXP;
    }

    public int getPlayerLevel() {
        return this.playerLevel;
    }

    public bool isGotPoison() {
        return this.gotPoison;
    }


    //Set stats

    public void setPlayerMaxHealth(float health) {
        this.playerMaxHealth = health;
    }

    public void setPlayerCurrentHealth(float health) {
        this.playerCurrentHealth = health;
    }

    public void setPlayerDamage(int damage) {
        this.playerDamage = damage;
    }

    public void setPlayerArmor(int armor) {
        this.playerArmor = armor;
    }


    public void setPlayerGold(int gold) {
        this.playerGold = gold;
    }

    public void setPlayerTotalEXP(float exp) {
        this.playerTotalEXP = exp;
    }

    public void setPlayerCurrentEXP(float exp) {
        this.playerCurrentEXP = exp;
    }

    public void setPlayerLevel(int level) {
        this.playerLevel = level;
    }

    public void setPoison(bool poison, int dmg) {
        this.gotPoison = poison;
        this.dmgOverTime = dmg;
        
    }


    public void stayInArea(bool area, int dmg) {
        this.inArea = area;
        this.damagePerSec = dmg;
    }

    public void ShowStats() {
        statsAtkPoint.text = "Attack(Atk): " + playerDamage;
        statsDefPoint.text = "Defense(Def): " + playerArmor;
        statsHealthPoint.text = "Health Points(HP): " + playerCurrentHealth + "/" + playerMaxHealth;
        statsLevel.text = "Level(Lv): " + playerLevel;
        statsExp.text = "Experience(Exp): " + playerCurrentEXP + "/" + playerTotalEXP;
        statsGold.text = "Gold(G): " + playerGold;
    }

    public void SwordStats() {
        swordStatsDamge.text = "Attack(Atk): " + swordDmg;
        swordStatsLevel.text = "Level(Lv): " + swordLevel;
    }

    public int getSwordLevel() {
        return swordLevel;
    }

    public void setSwordLevel(int lv){
        swordLevel = lv;
    }


    public int getSwordDmg() {
        return swordDmg;
    }

    public void setSwordDamage(int dmg) {
        swordDmg = dmg;
    }
    
    public void UpdateSword(int lv, int dmg){ 
        swordDmg += dmg;
        playerDamage += dmg;
        swordLevel = lv;
    }

    public void AddQuest(string title, int status) {
        quests.Add(title, status);
    }

    public Dictionary<string, int> getQuests() {
        return quests;
    }

    public void setQuest(Dictionary<string, int> qsts) {
        quests = qsts;
    }

    public void selectDifficult(int diff) {
        difficult = diff;
    }

    public int getDifficult() {
        return this.difficult;
    }
}   

