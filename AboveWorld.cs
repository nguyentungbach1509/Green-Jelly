using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Change the area on map with suitable camera's position
*/
public class AboveWorld : MonoBehaviour
{
    public string abovePassword;
    public Vector2 updateMaxBorder;
    public Vector2 updateMinBorder;
    private CameraMovement camera;

    void Awake() {

        camera = Camera.main.GetComponent<CameraMovement>();
    }

    // Start is called before the first frame update
    void Start()
    {

        if(PlayerMovement.instance.scenePassword == abovePassword) {
            PlayerMovement.instance.transform.position = new Vector3(transform.position.x, transform.position.y, PlayerMovement.instance.transform.position.z);
            camera.minBorder = updateMinBorder;
            camera.maxBorder = updateMaxBorder;
        }

    }

   
}
