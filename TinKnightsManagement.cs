using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Manage to respawn the tin knight enemies at correct place
after being killed
*/
public class TinKnightsManagement : MonoBehaviour
{
    public static TinKnightsManagement Instance = null;
     private List<Vector3[]> tinKnights = new List<Vector3[]>();
     private List<int> healths = new List<int>();
     private TinKnight tk;

    [SerializeField]
    GameObject tinKnight;

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
        Instantiate(tinKnight, new Vector2 (-9989f, -5255f), tinKnight.transform.rotation);
    
    }

    // Update is called once per frame
    IEnumerator SpawnTinKnight(Vector2 spawnPosition)
	{
		yield return new WaitForSeconds (8f);
		tk = (Instantiate (tinKnight, spawnPosition, tinKnight.transform.rotation)).GetComponent<TinKnight>();
        setNewTinKnight(tk);
	}


    public void saveVector(Vector3[] old, int health) {
        tinKnights.Add(old);
        healths.Add(health);
    }

    void setNewTinKnight(TinKnight tkt) {
        tkt.initialPosition = tinKnights[0];
        tkt.currentHealth = healths[0];
        tkt.enemyHealthBar = enemySpawnHealthBar;
        tinKnights.RemoveAt(0);
        healths.RemoveAt(0);
    }
}
