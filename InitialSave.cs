using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class InitialSave : MonoBehaviour
{
    public Inventorydb inventorydb;
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player")) {
            PlayerMovement pm = other.GetComponent<PlayerMovement>();
            if(pm != null) {
                GameSavingSystem.SaveGame(pm, "PlayerHouse", DateTime.Now);
                inventorydb.SaveInventory();
            }
        }
    }
}
