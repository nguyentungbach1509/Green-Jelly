using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventoryUI : MonoBehaviour {
    
    public Inventorydb inventorydb;
    public Button slot;
    public GameObject itemButton;
    private bool isSelling;
    
    private Text[] txts;


    public void ShowInventory() {
        for(int i  = 0; i < inventorydb.inventory_slots.Count; i++) {
            if(inventorydb.inventory_slots[i].itemQuantity > 0) {
                SlotItemController aSlot = (Instantiate(slot, transform.position, Quaternion.identity)).GetComponent<SlotItemController>();
                txts = aSlot.GetComponentsInChildren<Text>(true);
                txts[0].text = inventorydb.inventory_slots[i].item.itemName + "";
                txts[1].text += inventorydb.inventory_slots[i].itemQuantity;
                aSlot.gameObject.transform.parent = gameObject.transform;
                aSlot.setSlot(inventorydb.inventory_slots[i]);
                if(isSelling) {
                    aSlot.setDuringSell(isSelling);
                }
            }
            
        }
    }


    public void InventoryReorder(bool needReselect) {
        int checkFirstItem = 0;
        SlotItemController[] allCurrentSlots = gameObject.transform.GetComponentsInChildren<SlotItemController>();
        if(allCurrentSlots.Length > 0) {
            if(needReselect) {EventSystem.current.SetSelectedGameObject(null);}
            foreach(SlotItemController slt in allCurrentSlots) {
                Destroy(slt.gameObject);
            }
        }
        for(int i = 0; i < inventorydb.inventory_slots.Count; i++) {
            if(inventorydb.inventory_slots[i].itemQuantity > 0) {
                SlotItemController aSlot = (Instantiate(slot, transform.position, Quaternion.identity)).GetComponent<SlotItemController>();
                if(needReselect && checkFirstItem == 0) {
                    EventSystem.current.SetSelectedGameObject(aSlot.gameObject);
                    checkFirstItem++;
                }
                txts = aSlot.GetComponentsInChildren<Text>(true);
                txts[0].text = inventorydb.inventory_slots[i].item.itemName + "";
                txts[1].text += inventorydb.inventory_slots[i].itemQuantity;
                aSlot.setSlot(inventorydb.inventory_slots[i]);
                aSlot.gameObject.transform.parent = gameObject.transform;
                if(isSelling) {
                    aSlot.setDuringSell(isSelling);
                }
                
            }
        }

        allCurrentSlots = gameObject.transform.GetComponentsInChildren<SlotItemController>();
        if(allCurrentSlots.Length == 1) {
            EventSystem.current.SetSelectedGameObject(itemButton);
        }
    
    }

    public void setSelling(bool selling) {
        isSelling = selling;
    }
}
