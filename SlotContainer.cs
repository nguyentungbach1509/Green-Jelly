using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotContainer : MonoBehaviour
{
    private PlayerMovement player;
    private InventorySlot gem;
    public Text price;
    public Image icon;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }

    public void setGem(InventorySlot slot) {
        gem = slot;
    }

    public void BuyGem() {
        ItemContainerUI container = transform.parent.gameObject.GetComponent<ItemContainerUI>();
        if(player.playerStats.getPlayerGold() >= gem.item.item_buy_price) {
            player.playerStats.usePlayerGold(gem.item.item_buy_price);
            player.inventorydb.AddItem(gem.item, 1);
            gem.QuantityStack(-1);

            container.RerenderShopItems(true);
        }
    }
}
