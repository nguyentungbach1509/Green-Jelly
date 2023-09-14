using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemContainerUI : MonoBehaviour
{
    public ShopInventorydb shopInventory;
    public SlotContainer slotContainer;
    public GameObject soldOut;
    
    
    // Start is called before the first frame update
    void Start()
    {
        RerenderShopItems(false);
    }

    public void RerenderShopItems(bool needSelect) {
        ClearItemUI(needSelect);
        int no_slot = 0;
        foreach(InventorySlot slot in shopInventory.inventory_slots) {
            if(slot.itemQuantity > 0) {
                SlotContainer aSlot = (Instantiate(slotContainer, transform.position, Quaternion.identity)).GetComponent<SlotContainer>();
                aSlot.transform.parent = transform;
                aSlot.setGem(slot);
                aSlot.icon.sprite = slot.item.itemSprite;
                aSlot.price.text = slot.item.item_buy_price+"G";
            }
            else {
                GameObject so = Instantiate(soldOut, transform.position, Quaternion.identity) as GameObject;
                so.transform.parent = transform;
                if(no_slot == 0 && needSelect) {
                    GameObject slot_button = (so.transform.Find("Shopper Slot")).gameObject;
                    EventSystem.current.SetSelectedGameObject(slot_button);
                }
                no_slot++;
            }
        }
    }

    private void ClearItemUI(bool needSelect) {
        
        if(needSelect){
            EventSystem.current.SetSelectedGameObject(null);
        }

        foreach(Transform child in transform) {
            Destroy(child.gameObject);
        }
        
    }


    
}
