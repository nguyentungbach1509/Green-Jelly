using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class WolfEnergyBallManagement : MonoBehaviour
{

  
    private Transform playerTransform;
    private Vector2 target;
    public WolfEnergyBall energyBall;
   
 
    // Start is called before the first frame update
    void Start()
    {   
        if(GameObject.FindGameObjectWithTag("Player") != null) {
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
            target = new Vector2(playerTransform.position.x, playerTransform.position.y);
            
            if(Mathf.Abs(target.x - transform.position.x) > Mathf.Abs(target.y - transform.position.y)) {
                
                
                Instantiate(energyBall, transform.position, Quaternion.identity);
                ((Instantiate(energyBall, transform.position, Quaternion.identity)).GetComponent<WolfEnergyBall>()).setAnotherTarget(new Vector2(target.x, target.y + 3f));
                ((Instantiate(energyBall, transform.position, Quaternion.identity)).GetComponent<WolfEnergyBall>()).setAnotherTarget(new Vector2(target.x, target.y - 3));
                ((Instantiate(energyBall, transform.position, Quaternion.identity)).GetComponent<WolfEnergyBall>()).setAnotherTarget(new Vector2(target.x, target.y + 6));
                ((Instantiate(energyBall, transform.position, Quaternion.identity)).GetComponent<WolfEnergyBall>()).setAnotherTarget(new Vector2(target.x, target.y - 6));
            }
            else if(Mathf.Abs(target.x - transform.position.x) < Mathf.Abs(target.y - transform.position.y)) {
            
                Instantiate(energyBall, transform.position, Quaternion.identity);
                ((Instantiate(energyBall, transform.position, Quaternion.identity)).GetComponent<WolfEnergyBall>()).setAnotherTarget(new Vector2(target.x + 3f, target.y));
                ((Instantiate(energyBall, transform.position, Quaternion.identity)).GetComponent<WolfEnergyBall>()).setAnotherTarget(new Vector2(target.x - 3f, target.y));
                ((Instantiate(energyBall, transform.position, Quaternion.identity)).GetComponent<WolfEnergyBall>()).setAnotherTarget(new Vector2(target.x + 6, target.y));
                ((Instantiate(energyBall, transform.position, Quaternion.identity)).GetComponent<WolfEnergyBall>()).setAnotherTarget(new Vector2(target.x - 6, target.y));     
            
            }
        }
        
    }


    void Update()  {
        Destroy(gameObject);
    }

}
