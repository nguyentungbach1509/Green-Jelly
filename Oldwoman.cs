using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

/*
Controll the NPC
*/
public class Oldwoman : MonoBehaviour
{
    public GameObject dialogBox;
    public Text signText;
    public bool signActive;
    public string text;
    private bool inRange;
    private Vector2 playerDirect;
    private PlayerMovement pm;
    private Transform npcTransform;
    private string textDirect;

    public GameObject button_food;
    public GameObject optionUI;
    public GameObject tavernShop_UI;
    public GameObject alert_UI;

    public Inventorydb inventory;
    public ShopInventorydb shop_inventory;

    // Start is called before the first frame update
    void Start()
    {
        npcTransform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if(inRange && (pm.getCurrentState() == "Normal" || pm.getCurrentState() == "Standing" )) {
            if(pm.getDirects().Count > 0){playerDirect = pm.getDirects()[0];}
            if(Input.GetKeyDown(KeyCode.Z) && playerDirect.y == 1) {
                if(!dialogBox.activeInHierarchy) {
                    pm.setState(1);
                    optionUI.SetActive(true);
                    EventSystem.current.SetSelectedGameObject(button_food);
                    dialogBox.SetActive(true);
                    signText.text = text;
                }
            }
            if(Input.GetKeyDown(KeyCode.X) && playerDirect.y == 1){
                if(dialogBox.activeInHierarchy) {
                    pm.setState(0);
                    optionUI.SetActive(false);
                    dialogBox.SetActive(false);
                    tavernShop_UI.SetActive(false);
                }
            }
        }
    }


    public void RestButton() {
        pm.playerStats.setPlayerCurrentHealth(pm.playerStats.getPlayerMaxHealth());
        GameSavingSystem.SaveGame(pm, "Tavern", DateTime.Now);
        inventory.SaveInventory();
        shop_inventory.SaveShopInventory();
        StartCoroutine(SavingAlert());
    }

    private IEnumerator SavingAlert() {
        alert_UI.SetActive(true);
        yield return new WaitForSeconds(1f);
        alert_UI.SetActive(false);
    }
    
    private void OnTriggerEnter2D(Collider2D other) {
       if(other.CompareTag("Player")) {
           inRange = true;
           pm = other.GetComponent<PlayerMovement>();
       }
    }


    private void OnTriggerExit2D(Collider2D other) {
      if(other.CompareTag("Player")) {
          inRange = false;
          dialogBox.SetActive(false);
      }
    }
}
