using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryForgerUI : MonoBehaviour
{
    public SlotForger slot_forger;
    public Inventorydb player_inventory;

    public void ShowInventory() {
        for(int i  = 0; i < player_inventory.inventory_slots.Count; i++) {
            if(player_inventory.inventory_slots[i].itemQuantity > 0 && player_inventory.inventory_slots[i].item.type == ItemType.gem) {
                SlotForger aSlot = (Instantiate(slot_forger, transform.position, Quaternion.identity)).GetComponent<SlotForger>();
                aSlot.transform.parent = transform;
                aSlot.icon.sprite = player_inventory.inventory_slots[i].item.itemSprite;
                aSlot.setGemSlot(player_inventory.inventory_slots[i]);
            }
        }
    }


    public void InventoryReorder(bool needReselect) {
        int checkFirstItem = 0;
        SlotForger[] allCurrentSlots = gameObject.transform.GetComponentsInChildren<SlotForger>();
        if(allCurrentSlots.Length > 0) {
            if(needReselect) {EventSystem.current.SetSelectedGameObject(null);}
            foreach(SlotForger slt in allCurrentSlots) {
                Destroy(slt.gameObject);
            }
        }
        for(int i = 0; i < player_inventory.inventory_slots.Count; i++) {
            if(player_inventory.inventory_slots[i].itemQuantity > 0 && player_inventory.inventory_slots[i].item.type == ItemType.gem) {
                SlotForger aSlot = (Instantiate(slot_forger, transform.position, Quaternion.identity)).GetComponent<SlotForger>();
                aSlot.transform.parent = transform;
                aSlot.icon.sprite = player_inventory.inventory_slots[i].item.itemSprite;
                aSlot.setGemSlot(player_inventory.inventory_slots[i]);
                if(needReselect && checkFirstItem == 0) {
                    EventSystem.current.SetSelectedGameObject(aSlot.gameObject);
                    checkFirstItem++;
                }
            }
        }
    }
}
