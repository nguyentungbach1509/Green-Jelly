using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


public class SlotItemController : MonoBehaviour {
    
    Button _button;

    private bool duringSell;
    private InventorySlot inven_slot;
    private PlayerMovement playerMovement;

    void Start()
    {   
       gameObject.GetComponent<Button>().onClick.AddListener(SelectItem);
       if(GameObject.FindGameObjectWithTag("Player") != null) {
           playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
       }
    }

    void SelectItem() {
        InventoryUI inventoryUI = transform.parent.gameObject.GetComponent<InventoryUI>();
        if(playerMovement != null && playerMovement.playerBeKilled == false 
        && playerMovement.playerStats.getPlayerCurrentHealth() < playerMovement.playerStats.getPlayerMaxHealth()){
            if(!duringSell) {
                 if(inven_slot.item.type == ItemType.food) {
                    FoodItem foodItem = inven_slot.item as FoodItem;
                    inven_slot.QuantityStack(-1);
                    playerMovement.playerStats.setPlayerRestoreHealth((foodItem.healthRegen));
                    if(inven_slot.itemQuantity == 0) {
                        playerMovement.inventorydb.inventory_slots.Remove(inven_slot);
                    }
                    inventoryUI.InventoryReorder(true);
                }
            }
           
        }
        if(duringSell) {
            inven_slot.QuantityStack(-1);
            playerMovement.playerStats.gainPlayerGold(inven_slot.item.item_sell_price);
            if(inven_slot.itemQuantity == 0) {
                playerMovement.inventorydb.inventory_slots.Remove(inven_slot);
            }
            inventoryUI.InventoryReorder(true);
        }
    }

    public void setSlot(InventorySlot newSlot) {
        inven_slot = newSlot;
    }

    public void setDuringSell(bool sell) {
        duringSell = sell;
    }
}
