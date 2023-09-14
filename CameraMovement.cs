using System.Collections;
using System.Collections.Generic;
using UnityEngine;



/*
Handle the movement of the camera
*/
public class CameraMovement : MonoBehaviour
{

    private Transform player;
    public float smoothing;
    public Vector2 maxBorder;
    public Vector2 minBorder;


      // Start is called before the first frame update
    void Start()
    {
        player = PlayerMovement.instance.transform;
    }
 

    // Update is called once per frame
    void LateUpdate()
    {
        
        if(transform.position != player.position) {
            Vector3 playerPostion = new Vector3(player.position.x, player.position.y, transform.position.z);

            playerPostion.x = Mathf.Clamp(playerPostion.x, minBorder.x, maxBorder.x);
            playerPostion.y = Mathf.Clamp(playerPostion.y, minBorder.y, maxBorder.y);

            transform.position = Vector3.Lerp(transform.position, playerPostion, smoothing);
        }
        
    } 

}
