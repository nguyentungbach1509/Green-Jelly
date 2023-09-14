using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using System;

/*
Controll the menuUI
*/
public class MenuController : MonoBehaviour
{
    public Animator animation;
    public GameObject controlUI;
    public GameObject playButton;
    public PlayerMovement player;
    public AudioManager audioManager;
    public Inventorydb inventory;
    public ShopInventorydb shop_inventory;
    public GameObject gameUI;
    public GameObject game_difficult_UI;
    public GameObject load_menu_UI;
    public NewSlotMenu new_slot_UI;
    


    void Start() {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update() {
        if(Input.GetKeyDown(KeyCode.X)) {
            if(game_difficult_UI.activeInHierarchy){
                game_difficult_UI.SetActive(false);
                EventSystem.current.SetSelectedGameObject(playButton);
            }

            if(new_slot_UI.gameObject.activeInHierarchy) {
                new_slot_UI.onceTime = 0;
                new_slot_UI.gameObject.SetActive(false);
                EventSystem.current.SetSelectedGameObject(playButton);
            }

            if(load_menu_UI.activeInHierarchy) {
                load_menu_UI.SetActive(false);
                EventSystem.current.SetSelectedGameObject(playButton);
            }
        }
    }

    public void NewGame() {
        game_difficult_UI.SetActive(true);
    }

    public void StartNewGameNormal() {
        player.gameObject.SetActive(true);
        gameUI.SetActive(true);
        player.playerStats.selectDifficult(0);
        game_difficult_UI.SetActive(false);
        new_slot_UI.gameObject.SetActive(true);
    }

    public void StartNewGameHard() {
        player.gameObject.SetActive(true);
        gameUI.SetActive(true);
        player.playerStats.selectDifficult(1);
        game_difficult_UI.SetActive(false);
        new_slot_UI.gameObject.SetActive(true);
    }

    public void CreateNewSlot(string path) {
        indentifyPath(path);
        GameSavingSystem.save_file = path;
        new_slot_UI.gameObject.SetActive(false);
        StartCoroutine(SceneEffect());
    }

    private void indentifyPath(string path) {
         switch(path) {
            case "/player1.sav":
                ShopInventorydb.save_path = "/shopinventory1.sav";
                Inventorydb.inven_save_path = "/inventory1.sav";
                break;
            case "/player2.sav":
                ShopInventorydb.save_path = "/shopinventory2.sav";
                Inventorydb.inven_save_path = "/inventory2.sav";
                break;
            default:
                ShopInventorydb.save_path = "/shopinventory.sav";
                Inventorydb.inven_save_path = "/inventory.sav";
                break;
        }
        
    }

    public void GameControl() {
        controlUI.SetActive(true);
    }

    public void ExitGame() {
        Application.Quit();
    }

    public void BackButton() {
        controlUI.SetActive(false);
        EventSystem.current.SetSelectedGameObject(playButton);
    }

    public void OpenLoadMenu() {
        load_menu_UI.SetActive(true);
    }

    public void LoadGame(string path) {
        GameSavingSystem.save_file = path;
        indentifyPath(path);
        PlayerData data = GameSavingSystem.LoadGame();
        if(data != null) {
            player.gameObject.SetActive(true);
            gameUI.SetActive(true);
            StartCoroutine(ContinueGame(data));
        }
    }

    IEnumerator SceneEffect() {
        animation.SetTrigger("end");
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }


    IEnumerator ContinueGame(PlayerData data) {
        animation.SetTrigger("end");
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
        load_menu_UI.SetActive(false);
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
