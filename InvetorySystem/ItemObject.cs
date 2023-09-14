using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemObject : MonoBehaviour {
   public Itemdb itemdb;

   void Update() {
      StartCoroutine(DestroyItemOnTheGround());
   }

   private IEnumerator DestroyItemOnTheGround() {
      yield return new WaitForSeconds(8f);
      Destroy(gameObject, 0.5f);
   }
   
}
