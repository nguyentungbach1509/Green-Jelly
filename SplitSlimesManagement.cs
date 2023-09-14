using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplitSlimesManagement : MonoBehaviour
{
     public static SplitSlimesManagement Instance = null;
     private List<Vector3[]> slimes = new List<Vector3[]>();
     private List<int> healths = new List<int>();
     private SplitSlime ss;

    [SerializeField]
    GameObject splitSlime;

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
        Instantiate(splitSlime, new Vector2 (-4555f, -4555f), splitSlime.transform.rotation);
    
    }

    // Update is called once per frame
    IEnumerator SpawnSplitSlime(Vector2 spawnPosition)
	{
		yield return new WaitForSeconds (5f);
		ss = (Instantiate (splitSlime, spawnPosition, splitSlime.transform.rotation)).GetComponent<SplitSlime>();
        setNewSlime(ss);
	}


    public void saveVector(Vector3[] old, int health) {
        slimes.Add(old);
        healths.Add(health);
    }

    void setNewSlime(SplitSlime slime) {
        slime.initialPosition = slimes[0];
        slime.currentHealth = healths[0];
        slime.enemyHealthBar = enemySpawnHealthBar;
        slimes.RemoveAt(0);
        healths.RemoveAt(0);
    }

   
}
