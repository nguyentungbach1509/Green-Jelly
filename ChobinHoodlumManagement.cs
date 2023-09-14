using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChobinHoodlumManagement : MonoBehaviour
{
    public static ChobinHoodlumManagement Instance = null;
     private List<Vector3[]> chobins = new List<Vector3[]>();
     private List<int> healths = new List<int>();
     private ChobinHoodlum chl;

    [SerializeField]
    GameObject chobinHoodlum;

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
        Instantiate(chobinHoodlum, new Vector2 (-9900f, -5055f), chobinHoodlum.transform.rotation);
    
    }

    // Update is called once per frame
    IEnumerator SpawnChobinHoodlum(Vector2 spawnPosition)
	{
		yield return new WaitForSeconds (8f);
		chl = (Instantiate (chobinHoodlum, spawnPosition, chobinHoodlum.transform.rotation)).GetComponent<ChobinHoodlum>();
        setNewChobinHoodlum(chl);
	}


    public void saveVector(Vector3[] old, int health) {
        chobins.Add(old);
        healths.Add(health);
    }

    void setNewChobinHoodlum(ChobinHoodlum chobinhoodlum) {
        chobinhoodlum.initialPosition = chobins[0];
        chobinhoodlum.currentHealth = healths[0];
        chobinhoodlum.enemyHealthBar = enemySpawnHealthBar;
        chobins.RemoveAt(0);
        healths.RemoveAt(0);
    }
}
