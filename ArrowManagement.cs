using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


/*
Controll to spawn the arrow of the enemy
*/
public class ArrowManagement : MonoBehaviour
{

  
    private Transform playerTransform;
    private Vector2 target;
    public Arrow arrow;
    public int arrowNo;
 
    // Start is called before the first frame update
    void Start()
    {   
        if(GameObject.FindGameObjectWithTag("Player") != null) {
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
            target = new Vector2(playerTransform.position.x, playerTransform.position.y);
            
            if(Mathf.Abs(target.x - transform.position.x) > Mathf.Abs(target.y - transform.position.y)) {
                
                switch(arrowNo) {
                    case 3:
                        Instantiate(arrow, transform.position, Quaternion.identity);
                        ((Instantiate(arrow, transform.position, Quaternion.identity)).GetComponent<Arrow>()).setAnotherTarget(new Vector2(target.x, target.y + 2.5f));
                        ((Instantiate(arrow, transform.position, Quaternion.identity)).GetComponent<Arrow>()).setAnotherTarget(new Vector2(target.x, target.y - 3));
                        break;
                    case 5:
                        Instantiate(arrow, transform.position, Quaternion.identity);
                        ((Instantiate(arrow, transform.position, Quaternion.identity)).GetComponent<Arrow>()).setAnotherTarget(new Vector2(target.x, target.y + 2.5f));
                        ((Instantiate(arrow, transform.position, Quaternion.identity)).GetComponent<Arrow>()).setAnotherTarget(new Vector2(target.x, target.y - 2.5f));
                        ((Instantiate(arrow, transform.position, Quaternion.identity)).GetComponent<Arrow>()).setAnotherTarget(new Vector2(target.x, target.y + 5));
                        ((Instantiate(arrow, transform.position, Quaternion.identity)).GetComponent<Arrow>()).setAnotherTarget(new Vector2(target.x, target.y - 5));
                        break;
                }
            }

            else if(Mathf.Abs(target.x - transform.position.x) < Mathf.Abs(target.y - transform.position.y)) {
                switch(arrowNo) {
                    case 3:
                        Instantiate(arrow, transform.position, Quaternion.identity);
                        ((Instantiate(arrow, transform.position, Quaternion.identity)).GetComponent<Arrow>()).setAnotherTarget(new Vector2(target.x + 2.5f, target.y));
                        ((Instantiate(arrow, transform.position, Quaternion.identity)).GetComponent<Arrow>()).setAnotherTarget(new Vector2(target.x - 2.5f, target.y));
                        break;
                    case 5:
                        Instantiate(arrow, transform.position, Quaternion.identity);
                        ((Instantiate(arrow, transform.position, Quaternion.identity)).GetComponent<Arrow>()).setAnotherTarget(new Vector2(target.x + 2.5f, target.y));
                        ((Instantiate(arrow, transform.position, Quaternion.identity)).GetComponent<Arrow>()).setAnotherTarget(new Vector2(target.x - 2.5f, target.y));
                        ((Instantiate(arrow, transform.position, Quaternion.identity)).GetComponent<Arrow>()).setAnotherTarget(new Vector2(target.x + 5, target.y));
                        ((Instantiate(arrow, transform.position, Quaternion.identity)).GetComponent<Arrow>()).setAnotherTarget(new Vector2(target.x - 5, target.y));
                        break;
                }
            
            }
        }
        
    }


    void Update()  {
        Destroy(gameObject);
    }

}
