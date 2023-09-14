using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimesHolderManagement : MonoBehaviour
{
    public static SlimesHolderManagement Instance = null;
    private List<Vector3[]> borders = new List<Vector3[]>();
    private List<int> healths = new List<int>();
    private List<Vector2> positions = new List<Vector2>();
    private SlimeHolder holder;
    private SimpleSlime[] ss;

    [SerializeField]
    GameObject holderObject;
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
        Instantiate(holderObject, new Vector2 (-5000f, -4055f), holderObject.transform.rotation);
        Instantiate(simpleSlime, new Vector2 (-4055f, -5000f), simpleSlime.transform.rotation);
    
    }

    // Update is called once per frame
    IEnumerator SpawnSlimeHolder(Vector2 spawnPosition)
	{
		yield return new WaitForSeconds (0f);
		ss = new SimpleSlime[]{(Instantiate (simpleSlime, new Vector2(spawnPosition.x - 0.5f, spawnPosition.y), simpleSlime.transform.rotation)).GetComponent<SimpleSlime>(), 
        (Instantiate (simpleSlime, new Vector2(spawnPosition.x + 0.5f, spawnPosition.y), simpleSlime.transform.rotation)).GetComponent<SimpleSlime>()};
        
        holder = (Instantiate (holderObject, new Vector2(0,0), holderObject.transform.rotation)).GetComponent<SlimeHolder>();
        
        setNewSmallSlime(ss, holder);
	}


    public void saveStats(Vector3[] old, int health, Vector2 pos) {
        borders.Add(old);
        healths.Add(health);
        positions.Add(pos);
    }

    void setNewSmallSlime(SimpleSlime[] slime, SlimeHolder hold) {
        slime[0].initialPosition = borders[0];
        slime[0].currentHealth = 10;
        slime[0].enemyHealthBar = enemySpawnHealthBar;
        slime[1].initialPosition = borders[0];
        slime[1].currentHealth = 10;
        slime[1].enemyHealthBar = enemySpawnHealthBar;
        slime[1].enemySpeed = slime[0].enemySpeed - 1;

        hold.setStats(healths[0], positions[0], borders[0]);

        slime[0].transform.parent = hold.transform;
        slime[1].transform.parent = hold.transform;

        borders.RemoveAt(0);
        healths.RemoveAt(0);
        positions.RemoveAt(0);
    }
}
