using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthBarManagement : MonoBehaviour
{
    public static EnemyHealthBarManagement Instance = null;
    private int savingHealth;
    
    private EnemyHealthBar ehb;

    [SerializeField]
    GameObject enemyHealthBar;
    [SerializeField]
    GameObject canvas;

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
        Instantiate(enemyHealthBar, new Vector2 (-9999f, -9999f), enemyHealthBar.transform.rotation);
    
    }

    // Update is called once per frame
    IEnumerator SpawnEnemyHealthBar(Vector2 spawnPosition)
	{
		yield return new WaitForSeconds (5f);
		ehb = (Instantiate (enemyHealthBar, spawnPosition, enemyHealthBar.transform.rotation)).GetComponent<EnemyHealthBar>();
        putToCanvas(ehb);
    }

    public void setSpawnHealthBar(int health) {
        savingHealth = health;
    }

    private void putToCanvas(EnemyHealthBar temp) {
        temp.setMaxHealth(savingHealth);
        temp.transform.parent = canvas.transform;
    }
}
