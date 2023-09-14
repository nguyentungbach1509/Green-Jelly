using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Foods Item", menuName = "ItemSystem/Items/FoodItem")]
public class FoodItem : Itemdb
{   
    public int healthRegen;
    void Awake(){ 
        type = ItemType.food;
    }
}
