using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
The class manages to respawn the ApeMummy when it is be killed
at correct position.
*/
public class ApeMummyManagement : MonoBehaviour
{
    public static ApeMummyManagement Instance = null;
     private List<Vector3[]> apes = new List<Vector3[]>();
     private List<int> healths = new List<int>();
     private ApeMummy am;

    [SerializeField]
    GameObject apeMummy;

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
        Instantiate(apeMummy, new Vector2 (-9999f, -5555f), apeMummy.transform.rotation);
    
    }

    // Update is called once per frame
    IEnumerator SpawnApeMummy(Vector2 spawnPosition)
	{
		yield return new WaitForSeconds (10f);
		am = (Instantiate (apeMummy, spawnPosition, apeMummy.transform.rotation)).GetComponent<ApeMummy>();
        setNewApeMummy(am);
	}


    public void saveVector(Vector3[] old, int health) {
        apes.Add(old);
        healths.Add(health);
    }

    void setNewApeMummy(ApeMummy apemummy) {
        apemummy.initialPosition = apes[0];
        apemummy.currentHealth = healths[0];
        apemummy.enemyHealthBar = enemySpawnHealthBar;
        apes.RemoveAt(0);
        healths.RemoveAt(0);
    }
}
