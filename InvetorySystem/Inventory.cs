using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    private List<Item> items;

    public Inventory() {
        items = new List<Item>();
        AddItem(new Item{type = Item.ItemType.Food, quantity = 1, itemName="Apple", itemPrice=20, healthRegen=15});
        AddItem(new Item{type = Item.ItemType.Food, quantity = 5, itemName="Carrot", itemPrice=25, healthRegen=20});
        
    }

    public void AddItem(Item item) {
        items.Add(item);
    }

    public List<Item> GetItems() {
        return items;
    }
}
