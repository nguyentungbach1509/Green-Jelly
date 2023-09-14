using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public enum ItemType {
        Food,
        HealthPoition,
        Gem
    }

    public int quantity;
    public ItemType type;
    public string itemName;
    public int itemPrice;
    public int healthRegen;
}
