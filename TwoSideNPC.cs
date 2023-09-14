using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TwoSideNPC : MonoBehaviour
{
    public GameObject dialogBox;
    public Text signText;
    public bool signActive;
    public string text;
    private bool inRange;
    private Vector2 playerDirect;
    private PlayerMovement pm;
    private Transform npcTransform;
    private string textDirect;

    // Start is called before the first frame update
    void Start()
    {
        npcTransform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if(inRange && (pm.getCurrentState() == "Normal" || pm.getCurrentState() == "Standing" )) {
            playerDirect = pm.getDirects()[0];
            switch(textDirect) {
                case "Left":
                    if(Input.GetKeyDown(KeyCode.Z) && playerDirect.x == 1) {
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

                    break;

                case "Right":

                    if(Input.GetKeyDown(KeyCode.Z) && playerDirect.x == -1) {
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

                    break;
            }

        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
       if(other.CompareTag("Player")) {
           inRange = true;
           pm = other.GetComponent<PlayerMovement>();

           if(npcTransform.position.x < pm.transform.position.x) {
               textDirect = "Right";
           }
           if(npcTransform.position.x > pm.transform.position.x) {
               textDirect = "Left";
           }
       }
    }


    private void OnTriggerExit2D(Collider2D other) {
      if(other.CompareTag("Player")) {
          inRange = false;
          dialogBox.SetActive(false);
      }
    }
}
