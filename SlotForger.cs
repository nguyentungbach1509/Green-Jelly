using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/*
Handle the slot of player's item when player goes into the blacksmith's place.
*/
public class SlotForger : MonoBehaviour
{
    private InventorySlot slot;
    private PlayerMovement playerMovement;
    private BlackSmith blackSmith;

    public Image icon;
    
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(SelectGem);  
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        blackSmith = GameObject.Find("BlackSmith").GetComponent<BlackSmith>();
    }

    private void SelectGem() {
        GemItem gem = slot.item as GemItem;
        InventoryForgerUI inventoryForgerUI = transform.parent.gameObject.GetComponent<InventoryForgerUI>();
        if((gem.level - 1) == playerMovement.playerStats.getSwordLevel()) {
            slot.QuantityStack(-1);
            playerMovement.inventorydb.inventory_slots.Remove(slot);
            playerMovement.playerStats.UpdateSword(gem.level, gem.dmg_bonus);
            blackSmith.setText("Blacksmith: Upgrade successfully!!! Your sword is level " + playerMovement.playerStats.getSwordLevel() + " now");
        }
        else {
            blackSmith.setText("Blacksmith: You need to upgrade your sword to level " + (playerMovement.playerStats.getSwordLevel()+1) + " before upgrading it to level " + gem.level);
        }

        inventoryForgerUI.InventoryReorder(true);

    }

    public void setGemSlot(InventorySlot aSlot) {
        slot = aSlot;
    }
}
