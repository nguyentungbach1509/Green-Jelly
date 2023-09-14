using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*Manage to respawn the wood enemies after being killed*/
public class WoodManagement : MonoBehaviour
{
    public static WoodManagement Instance = null;
    private List<Vector3[]> woods = new List<Vector3[]>();
    private List<int> healths = new List<int>();
    private WoodMovement wm;

    [SerializeField]
    GameObject woodMovement;

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
        Instantiate(woodMovement, new Vector2 (-9999f, -4055f), woodMovement.transform.rotation);
    
    }

    // Update is called once per frame
    IEnumerator SpawnWoodMovement(Vector2 spawnPosition)
	{
		yield return new WaitForSeconds (8f);
		wm = (Instantiate (woodMovement, spawnPosition, woodMovement.transform.rotation)).GetComponent<WoodMovement>();
        setNewWoodMovement(wm);
	}


    public void saveVector(Vector3[] old, int health) {
        woods.Add(old);
        healths.Add(health);
    }

    void setNewWoodMovement(WoodMovement snake) {
        snake.initialPosition = woods[0];
        snake.currentHealth = healths[0];
        snake.enemyHealthBar = enemySpawnHealthBar;
        woods.RemoveAt(0);
        healths.RemoveAt(0);
    }
}
