using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
Controll the coin objects
*/
public class GoldCoin : MonoBehaviour
{

    public int cost;
    

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(DestroyGoldCoin());
    }

    void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player")) {
            PlayerStats ps = other.gameObject.GetComponent<PlayerMovement>().playerStats;
            ps.gainPlayerGold(cost);
            Destroy(gameObject);
         
        }
    }

    private IEnumerator DestroyGoldCoin() {

        yield return new WaitForSeconds(8f);
        Destroy(gameObject, 0.5f);
    }
}
