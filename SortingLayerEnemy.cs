using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortingLayerEnemy : MonoBehaviour
{
    private Transform playerTransform;
    private SpriteRenderer sprite;
    

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        SortingLayer();
    }


    void SortingLayer() {

        if(playerTransform != null) {
            if(playerTransform.position.y > transform.position.y){

                sprite.sortingOrder = 1;
            }
            else {
                sprite.sortingOrder = -1;
            }
        }
        
    }
}
