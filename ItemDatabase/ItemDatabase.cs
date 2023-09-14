using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Item Database", menuName = "InventorySystem/Items/Database")]
public class ItemDatabase : ScriptableObject, ISerializationCallbackReceiver {
    
    public Itemdb[] Items;
    public Dictionary<Itemdb, int> GetID = new Dictionary<Itemdb, int>();
    public Dictionary<int, Itemdb> GetItemWithID = new Dictionary<int, Itemdb>();

    public void OnAfterDeserialize(){
        GetID = new Dictionary<Itemdb, int>();
        GetItemWithID = new Dictionary<int, Itemdb>();
        for(int i =0; i < Items.Length; i++) {
            GetID.Add(Items[i], i);
            GetItemWithID.Add(i, Items[i]);
        }
    }

    public void OnBeforeSerialize() {

    }
}
