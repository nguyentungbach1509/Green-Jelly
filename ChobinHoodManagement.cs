using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChobinHoodManagement : MonoBehaviour
{
     public static ChobinHoodManagement Instance = null;
     private List<Vector3[]> chobins = new List<Vector3[]>();
     private List<int> healths = new List<int>();
     private ChobinHood ch;

    [SerializeField]
    GameObject chobinHood;

    [SerializeField]
    EnemyHealthBar enemySpawnHealthBar;

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
        Instantiate(chobinHood, new Vector2 (-9999f, -5055f), chobinHood.transform.rotation);
    
    }

    // Update is called once per frame
    IEnumerator SpawnChobinHood(Vector2 spawnPosition)
	{
		yield return new WaitForSeconds (8f);
		ch = (Instantiate (chobinHood, spawnPosition, chobinHood.transform.rotation)).GetComponent<ChobinHood>();
        setNewChobinHood(ch);
	}


    public void saveVector(Vector3[] old, int health) {
        chobins.Add(old);
        healths.Add(health);
    }

    void setNewChobinHood(ChobinHood chobinhood) {
        chobinhood.initialPosition = chobins[0];
        chobinhood.currentHealth = healths[0];
        chobinhood.enemyHealthBar = enemySpawnHealthBar;
        chobins.RemoveAt(0);
        healths.RemoveAt(0);
    }
}
