using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleSlimesManagement : MonoBehaviour
{
    public static SimpleSlimesManagement Instance = null;
    private List<Vector3[]> slimes = new List<Vector3[]>();
    private List<int> healths = new List<int>();
    private SimpleSlime ss;

    [SerializeField]
    GameObject simpleSlime;
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
        Instantiate(simpleSlime, new Vector2 (-4055f, -4055f), simpleSlime.transform.rotation);
    
    }

    // Update is called once per frame
    IEnumerator SpawnSimpleSlime(Vector2 spawnPosition)
	{
		yield return new WaitForSeconds (5f);
		ss = (Instantiate (simpleSlime, spawnPosition, simpleSlime.transform.rotation)).GetComponent<SimpleSlime>();
        setNewSlime(ss);
	}


    public void saveVector(Vector3[] old, int health) {
        slimes.Add(old);
        healths.Add(health);
    }

    void setNewSlime(SimpleSlime slime) {
        slime.initialPosition = slimes[0];
        slime.currentHealth = healths[0];
        slime.enemyHealthBar = enemySpawnHealthBar;
        slimes.RemoveAt(0);
        healths.RemoveAt(0);
    }
}
