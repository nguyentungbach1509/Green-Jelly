using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


/*
Controll the blacksmith of the village
*/
public class BlackSmith : MonoBehaviour
{
   
    public GameObject dialogBox;
    public Text signText;
    public bool signActive;
    public string text;
    private bool inRange;
    private Vector2 playerDirect;
    private PlayerMovement pm;
    private Animator animator;

    public GameObject optionUI;
    public GameObject inventory_gem;
    private string reportText;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();  
    }

    // Update is called once per frame
    void Update()
    {
        if(inRange && (pm.getCurrentState() == "Normal" || pm.getCurrentState() == "Standing" )) {
            playerDirect = pm.getDirects()[0];
    
            if(Input.GetKeyDown(KeyCode.Z) && playerDirect.x == 1) {
                if(!dialogBox.activeInHierarchy) {
                    pm.setState(1);
                    animator.SetBool("Greeting", true);
                    optionUI.SetActive(true);
                    GameObject button_option = (optionUI.transform.Find("Upgrade Sword")).gameObject;
                    EventSystem.current.SetSelectedGameObject(button_option);
                    dialogBox.SetActive(true);
                    signText.text = text;
                }
                else {
                    signText.text = reportText;
                }
            }
            if(Input.GetKeyDown(KeyCode.X) && playerDirect.x == 1){
                if(dialogBox.activeInHierarchy) {
                    pm.setState(0);
                    optionUI.SetActive(false);
                    dialogBox.SetActive(false);
                    inventory_gem.SetActive(false);
                    animator.SetBool("Greeting", false);
                }
            }
        }
    }


    public void OptionButton() {
        InventoryForgerUI inventoryForgerUI = inventory_gem.GetComponentInChildren<InventoryForgerUI>(true);
        inventory_gem.SetActive(true);
        inventoryForgerUI.InventoryReorder(false);
        reportText="";
    }

    private void OnTriggerEnter2D(Collider2D other) {
       if(other.CompareTag("Player")) {
           inRange = true;
           pm = other.GetComponent<PlayerMovement>();
       }
    }
    public void setText(string text) {
        reportText = text;
    }
    
    private void OnTriggerExit2D(Collider2D other) {
      if(other.CompareTag("Player")) {
          inRange = false;
          dialogBox.SetActive(false);
      }
    }
}
