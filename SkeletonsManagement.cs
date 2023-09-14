using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonsManagement : MonoBehaviour
{
    public static SkeletonsManagement Instance = null;
     private List<Vector3[]> skeletons = new List<Vector3[]>();
     private List<int> healths = new List<int>();
     private Skeleton sk;

    [SerializeField]
    GameObject skeleton;

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
        Instantiate(skeleton, new Vector2 (-9909f, -5155f), skeleton.transform.rotation);
    
    }

    // Update is called once per frame
    IEnumerator SpawnSkeleton(Vector2 spawnPosition)
	{
		yield return new WaitForSeconds (8f);
		sk = (Instantiate (skeleton, spawnPosition, skeleton.transform.rotation)).GetComponent<Skeleton>();
        setNewSkeleton(sk);
	}


    public void saveVector(Vector3[] old, int health) {
        skeletons.Add(old);
        healths.Add(health);
    }

    void setNewSkeleton(Skeleton skton) {
        skton.initialPosition = skeletons[0];
        skton.currentHealth = healths[0];
        skton.enemyHealthBar = enemySpawnHealthBar;
        skeletons.RemoveAt(0);
        healths.RemoveAt(0);
    }
}
