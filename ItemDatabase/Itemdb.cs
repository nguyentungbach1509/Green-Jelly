using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType {
    food,
    gem,

}
public abstract class Itemdb : ScriptableObject {
    public Sprite itemSprite;
    public ItemType type;
    //public int itemID;

    [TextArea(5,15)]
    public string itemName;

    public int item_sell_price;
    public int item_buy_price;
    

}
