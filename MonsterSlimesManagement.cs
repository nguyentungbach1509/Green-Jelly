using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Respawn the monster slime at correct location after being killed
*/
public class MonsterSlimesManagement : MonoBehaviour
{
     public static MonsterSlimesManagement Instance = null;
     private List<Vector3[]> slimes = new List<Vector3[]>();
     private List<int> healths = new List<int>();
     private MonsterSlime ms;

    [SerializeField]
    GameObject monsterSlime;

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
        Instantiate(monsterSlime, new Vector2 (-9999f, -4055f), monsterSlime.transform.rotation);
    
    }

    // Update is called once per frame
    IEnumerator SpawnMonsterSlime(Vector2 spawnPosition)
	{
		yield return new WaitForSeconds (5f);
		ms = (Instantiate (monsterSlime, spawnPosition, monsterSlime.transform.rotation)).GetComponent<MonsterSlime>();
        setNewMonsterSlime(ms);
	}


    public void saveVector(Vector3[] old, int health) {
        slimes.Add(old);
        healths.Add(health);
    }

    void setNewMonsterSlime(MonsterSlime slime) {
        slime.initialPosition = slimes[0];
        slime.currentHealth = healths[0];
        slime.enemyHealthBar = enemySpawnHealthBar;
        slimes.RemoveAt(0);
        healths.RemoveAt(0);
    }
}
