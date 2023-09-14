using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/*
Handle changing the area
*/
public class AreaChange : MonoBehaviour
{

    public Vector3 playerMove;
    public Vector2 updateMaxBorder;
    public Vector2 updateMinBorder;
    private CameraMovement camera;
    public bool needText;
    public GameObject text;
    public string areaTitle;
    public string name_stop_audio;
    public string name_play_audio;
    public Text title;
    private AudioManager audioManager;

    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main.GetComponent<CameraMovement>();
        audioManager = FindObjectOfType<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")) {
            camera.minBorder = updateMinBorder;
            camera.maxBorder = updateMaxBorder;
            other.transform.position += playerMove;
            audioManager.StopSound(name_stop_audio);
            if(name_play_audio.Trim().Length > 0) {
                audioManager.PlaySound(name_play_audio);
            }
            
            if(needText) {
                text.SetActive(true);
                StartCoroutine(showTitle());
            }
        }
    }

    private IEnumerator showTitle() {
        title.text = areaTitle;
        yield return new WaitForSeconds(3f);
        text.SetActive(false);
    }
}
