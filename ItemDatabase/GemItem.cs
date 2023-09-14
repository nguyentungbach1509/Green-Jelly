using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Gem Item", menuName = "ItemSystem/Items/GemItem")]
public class GemItem : Itemdb
{
    public int level;
    public int dmg_bonus;
    
    void Awake(){ 
        type = ItemType.gem;
    }
}
