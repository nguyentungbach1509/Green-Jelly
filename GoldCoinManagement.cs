using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
Manage to spawn the coin at correct position
*/
public class GoldCoinManagement : MonoBehaviour
{
    public static GoldCoinManagement Instance = null;

    [SerializeField]
    GameObject goldCoin;
    
    void Awake()
	{
		if (Instance == null) {
            Instance = this;
        }
		else if (Instance != this){
            Destroy (gameObject);
        }	
		
	}

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(goldCoin, new Vector2 (-4800f, -4055f), goldCoin.transform.rotation);
    
    }

    // Update is called once per frame
    IEnumerator SpawnGoldCoin(Vector2 spawnPosition)
	{
		yield return new WaitForSeconds (.5f);
		Instantiate (goldCoin, spawnPosition, goldCoin.transform.rotation);
	}

}
