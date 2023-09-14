using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
The class to manage for respawning the basic cobra when it is killed
at correct position.
*/
public class BasicCobraManagement : MonoBehaviour
{
    public static BasicCobraManagement Instance = null;
     private List<Vector3[]> snakes = new List<Vector3[]>();
     private List<int> healths = new List<int>();
     private BasicCobra bs;

    [SerializeField]
    GameObject basicCobra;

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
        Instantiate(basicCobra, new Vector2 (-9999f, -4055f), basicCobra.transform.rotation);
    
    }

    // Update is called once per frame
    IEnumerator SpawnBasicCobra(Vector2 spawnPosition)
	{
		yield return new WaitForSeconds (8f);
		bs = (Instantiate (basicCobra, spawnPosition, basicCobra.transform.rotation)).GetComponent<BasicCobra>();
        setNewBasicCobra(bs);
	}


    public void saveVector(Vector3[] old, int health) {
        snakes.Add(old);
        healths.Add(health);
    }

    void setNewBasicCobra(BasicCobra snake) {
        snake.initialPosition = snakes[0];
        snake.currentHealth = healths[0];
        snake.enemyHealthBar = enemySpawnHealthBar;
        snakes.RemoveAt(0);
        healths.RemoveAt(0);
    }
}
