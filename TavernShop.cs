using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TavernShop : MonoBehaviour
{
    public GameObject tavernShop_UI;
    public Inventorydb inventorydb;
    
    public void BuyFoodButton() {
        tavernShop_UI.SetActive(true);
    }

}
