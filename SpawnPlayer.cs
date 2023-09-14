using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
Respawn player after die, the player's data will be load from last save points
*/
public class SpawnPlayer : MonoBehaviour
{

    public Inventorydb inventory;
    public ShopInventorydb shop_inventory;
    public static SpawnPlayer Instance = null;
    //[SerializeField]
    //public CameraMovement cameraMovement;

    public Animator animation;
    void Awake()
	{
		if (Instance == null) {
            Instance = this;
        }
		else if (Instance != this){
            Destroy (gameObject);
        }	
		
	}


    IEnumerator ReloadPlayer(PlayerMovement player) {
        animation.SetTrigger("end");
        AudioManager audioManager = FindObjectOfType<AudioManager>();
        PlayerData data =  GameSavingSystem.LoadGame();
        player.gameObject.GetComponent<Collider2D>().enabled = true;
        player.playerStats.setPlayerMaxHealth(data.max_health);
        player.playerStats.setPlayerCurrentHealth(data.health);
        player.playerStats.setPlayerDamage(data.dmg);
        player.playerStats.setPlayerLevel(data.level);
        player.playerStats.setPlayerTotalEXP(data.max_exp);
        player.playerStats.setPlayerCurrentEXP(data.exp);
        player.playerStats.setPlayerArmor(data.armor);
        player.playerStats.setPlayerGold(data.gold);
        player.playerStats.setSwordLevel(data.sword_lv);
        player.playerStats.setSwordDamage(data.sword_dmg);
        player.playerStats.setQuest(data.qsts);
        player.playerStats.selectDifficult(data.difficult);
        player.playerStats.goldUI.text = data.gold + "G";
        player.playerStats.levelNumber.text = data.level+"";
        player.playerStats.frontFillEXP.fillAmount = data.exp / data.max_exp;
        player.playerStats.backFillEXP.fillAmount = data.exp / data.max_exp;
        inventory.LoadInventory();
        shop_inventory.LoadShopInventory();
        if(data.sceneName.CompareTo("Tavern") == 0) {
            audioManager.DuringSound("Village");
        }
        else {
            audioManager.DuringSound("Home");
        }
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(data.sceneName);
        player.gameObject.SetActive(true);
        player.playerBeKilled = false;
        player.setState(0);
        player.transform.position = new Vector3(data.position[0], data.position[1], data.position[2]);
        
    }

}
