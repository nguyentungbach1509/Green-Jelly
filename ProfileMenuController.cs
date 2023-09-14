using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


/*
Handle the UI of the player such as items, equipment, stats. Also helping pause game
*/
public class ProfileMenuController : MonoBehaviour
{
    public static ProfileMenuController Instance;
    public static bool gameIsPausing;
    public GameObject profileMenu;
    public GameObject statsButton, equipButton, itemsButton;
    public GameObject statsRightSide, equipRightSide, itemsRightSide;
    private InventoryUI inventoryUI;
    public PlayerMovement playerMovement;
    private bool openInventory;

    // Start is called before the first frame update
    void Start()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(statsButton);
        playerMovement.playerStats.ShowStats();
        openInventory = false;
        inventoryUI = ((itemsRightSide.transform.Find("ItemsContainer")).gameObject.transform.Find("ItemsList")).GetComponent<InventoryUI>();
        inventoryUI.ShowInventory();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)) {
            if(gameIsPausing) {
                ResumeGame();
            }
            else{
                PauseGame();
            } 
        }

        if(gameIsPausing && (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.UpArrow))) {
            if(EventSystem.current.currentSelectedGameObject == statsButton) {
                statsRightSide.SetActive(true);
                playerMovement.playerStats.ShowStats();
                equipRightSide.SetActive(false);
                itemsRightSide.SetActive(false);
                
            }
            else if(EventSystem.current.currentSelectedGameObject == equipButton) {
                statsRightSide.SetActive(false);
                equipRightSide.SetActive(true);
                playerMovement.playerStats.SwordStats();
                itemsRightSide.SetActive(false);
            }
            else {
                statsRightSide.SetActive(false);
                equipRightSide.SetActive(false);
                itemsRightSide.SetActive(true);
                if(openInventory == false) {
                    inventoryUI.InventoryReorder(false);
                    openInventory = true;
                    
                }
               
            }

        }
       
    }

    void ResumeGame(){
        Time.timeScale = 1f;
        profileMenu.SetActive(false);
        gameIsPausing = false;
        openInventory = false;
    }

    void PauseGame() {
        playerMovement.playerStats.ShowStats();
        Time.timeScale = 0f;
        profileMenu.SetActive(true);
        gameIsPausing = true;
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(statsButton);
        statsRightSide.SetActive(true);
        equipRightSide.SetActive(false);
        itemsRightSide.SetActive(false);
    }
}
