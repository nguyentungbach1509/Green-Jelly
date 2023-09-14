using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodShopUI : MonoBehaviour
{
    public ItemDatabase database;
    public PlayerMovement player;
    public TavernShopSlot shop_slot;

    // Start is called before the first frame update
    void Start()
    {
        foreach(Itemdb item in database.Items){
            if(item.type == ItemType.food) {
                TavernShopSlot tavernShopSlot = (Instantiate(shop_slot, transform.position, Quaternion.identity)).GetComponent<TavernShopSlot>();
                tavernShopSlot.transform.parent = transform;
                tavernShopSlot.setItem(item);
                tavernShopSlot.icon.sprite = item.itemSprite;
                tavernShopSlot.name.text = item.itemName;
                tavernShopSlot.price.text = item.item_buy_price + "G";   
            }
        }

    }
}
