using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopKnightsManagement : MonoBehaviour
{
    public static CopKnightsManagement Instance = null;
     private List<Vector3[]> copKnights = new List<Vector3[]>();
     private List<int> healths = new List<int>();
     private CopKnight ck;

    [SerializeField]
    GameObject copKnight;

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
        Instantiate(copKnight, new Vector2 (-9989f, -5285f), copKnight.transform.rotation);
    
    }

    // Update is called once per frame
    IEnumerator SpawnCopKnight(Vector2 spawnPosition)
	{
		yield return new WaitForSeconds (8f);
		ck = (Instantiate (copKnight, spawnPosition, copKnight.transform.rotation)).GetComponent<CopKnight>();
        setNewCopKnight(ck);
	}


    public void saveVector(Vector3[] old, int health) {
        copKnights.Add(old);
        healths.Add(health);
    }

    void setNewCopKnight(CopKnight ckt) {
        ckt.initialPosition = copKnights[0];
        ckt.currentHealth = healths[0];
        ckt.enemyHealthBar = enemySpawnHealthBar;
        copKnights.RemoveAt(0);
        healths.RemoveAt(0);
    }
}
