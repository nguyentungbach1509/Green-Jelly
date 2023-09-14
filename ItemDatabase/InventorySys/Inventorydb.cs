using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

/*
Inventory database
*/
[CreateAssetMenu(fileName = "New Inventory", menuName = "InventorySystem/Inventory")]
public class Inventorydb : ScriptableObject, ISerializationCallbackReceiver {
    
    
    private ItemDatabase database;
    public static string inven_save_path = "/inventory.sav";
    public List<InventorySlot> inventory_slots = new List<InventorySlot>();

    private void OnEnable() {
#if UNITY_EDITOR
        database = (ItemDatabase)AssetDatabase.LoadAssetAtPath("Assets/Resources/Database.asset", typeof(ItemDatabase));
#else
        database = Resources.Load<ItemDatabase>("Database");
#endif
    }

    public void AddItem(Itemdb _item, int _quantity) {
        
        for(int i = 0; i < inventory_slots.Count; i++) {
            if(inventory_slots[i].item == _item) {
                inventory_slots[i].QuantityStack(_quantity);
                return;
            }
        }

        inventory_slots.Add(new InventorySlot(database.GetID[_item], _item, _quantity));
        
    }

    public void SaveInventory() {
        string save_data = JsonUtility.ToJson(this, true);
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream fileStream = File.Create(string.Concat(Application.persistentDataPath, inven_save_path));
        formatter.Serialize(fileStream, save_data);
        fileStream.Close();
    }

    public void LoadInventory() {
        if(File.Exists(string.Concat(Application.persistentDataPath, inven_save_path))) {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream fileStream = File.Open(string.Concat(Application.persistentDataPath, inven_save_path), FileMode.Open);
            JsonUtility.FromJsonOverwrite(formatter.Deserialize(fileStream).ToString(), this);
            fileStream.Close();
        }
    }

    public void OnAfterDeserialize(){
       for(int i = 0; i < inventory_slots.Count; i++) {
           inventory_slots[i].item = database.GetItemWithID[inventory_slots[i].itemID];
       }
    }

    public void OnBeforeSerialize() {

    }

}


[System.Serializable]
public class InventorySlot {

    public int itemID;
    public Itemdb item;
    public int itemQuantity;

    public InventorySlot(int id, Itemdb _item, int quantity) {
        itemID = id;
        item = _item;
        itemQuantity = quantity;
    }

    public void QuantityStack(int value) {
        itemQuantity += value;
    }
}
