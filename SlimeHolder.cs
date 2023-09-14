using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeHolder : MonoBehaviour
{
    
    public int numberSlime;
    private Vector3[] oldBorders;
    private Vector2 oldPos;
    private int oldHealth;

    
    // Start is called before the first frame update
    void Start()
    {
        numberSlime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
        if(numberSlime == 2) {
            StartCoroutine("SpawnSimpleSlime");
        }
    }

    IEnumerator SpawnSimpleSlime()
	{

        yield return new WaitForSeconds (3f);
        SplitSlimesManagement.Instance.saveVector(oldBorders, oldHealth);
        SplitSlimesManagement.Instance.StartCoroutine("SpawnSplitSlime", oldPos);
		Destroy(gameObject, 0f);
	}


    public void setStats(int health, Vector2 position, Vector3[] borders) {
        oldHealth = health;
        oldPos = position;
        oldBorders = borders;
    }   
}
