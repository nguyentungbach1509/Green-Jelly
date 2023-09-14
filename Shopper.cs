using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


/*
Control the shopper
*/
public class Shopper : MonoBehaviour
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
    public GameObject secret_shop_UI;
    public GameObject inventory_UI;

    public Inventorydb inventory;


    // Start is called before the first frame update
    void Start()
    {
        npcTransform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if(inRange && (pm.getCurrentState() == "Normal" || pm.getCurrentState() == "Standing" )) {
            playerDirect = pm.getDirects()[0];
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
                    secret_shop_UI.SetActive(false);
                    GameObject items_container = (inventory_UI.transform.Find("ItemsContainer")).gameObject;
                    InventoryUI inventoryUI = items_container.GetComponentInChildren<InventoryUI>(true);
                    inventoryUI.setSelling(false);
                    inventory_UI.SetActive(false);
                }
            }
        }
    }


    public void BuyItemButton() {
        secret_shop_UI.SetActive(true);
    }

    public void SellItemButton() {
        GameObject items_container = (inventory_UI.transform.Find("ItemsContainer")).gameObject;
        InventoryUI inventoryUI = items_container.GetComponentInChildren<InventoryUI>(true);
        inventory_UI.SetActive(true);
        inventoryUI.setSelling(true);
        inventoryUI.ShowInventory();
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
