using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
Handle the sign board of the game to show the direction when player interact
*/
public class ReadingSigns : MonoBehaviour
{

    public GameObject dialogBox;
    public Text signText;
    public bool signActive;
    public string text;
    private bool inRange;
    private Vector2 playerDirect;
    private PlayerMovement pm;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(inRange && (pm.getCurrentState() == "Normal" || pm.getCurrentState() == "Standing" )) {
            playerDirect = pm.getDirects()[0];
            if(Input.GetKeyDown(KeyCode.Z) && playerDirect.y == 1) {
                if(dialogBox.activeInHierarchy) {
                    pm.setState(0);
                    dialogBox.SetActive(false);
                }
                else {
                    pm.setState(1);
                    dialogBox.SetActive(true);
                    signText.text = text;
                }
            }

        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
       if(other.CompareTag("Player")) {
           inRange = true;
           pm = other.GetComponent<PlayerMovement>();
       }
    }


    private void OnTriggerExit2D(Collider2D other) {
      if(other.CompareTag("Player")) {
          inRange = false;
          dialogBox.SetActive(false);
      }
    }
}
