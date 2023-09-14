using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TavernShopSlot : MonoBehaviour
{
    private FoodItem food;
    public Text name;
    public Image icon;
    public Text price;
    private PlayerMovement player;


    void Start() {
        gameObject.GetComponent<Button>().onClick.AddListener(BuyItem);
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }

    void BuyItem() {
        if(player.playerStats.getPlayerGold() >= food.item_buy_price) {
            player.playerStats.usePlayerGold(food.item_buy_price);
            player.inventorydb.AddItem(food, 1);
        }
    }
    
    public void setItem(Itemdb item) {
        food = (FoodItem)item;
    }

}
