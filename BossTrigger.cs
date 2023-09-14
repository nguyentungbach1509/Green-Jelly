using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
Trigger the appearance of the boss when the player 
start the trigger.
*/
public class BossTrigger : MonoBehaviour
{
    bool activeTriggerOnlyOnce;
    
    [SerializeField]
    public string bossName;
    [SerializeField]
    public Camera camera;
    [SerializeField]
    public CameraMovement cameraMovement;
    [SerializeField]
    GameObject rocks;
    [SerializeField]
    public FinalBoss finalBoss;
    [SerializeField]
    public WolfBoss wolfBoss;
    [SerializeField]
    public Drakgon drakgon;
    
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player") && !activeTriggerOnlyOnce) {
           PlayerMovement pm = other.gameObject.GetComponent<PlayerMovement>();
           AudioManager audioManager = FindObjectOfType<AudioManager>();
           if(pm.playerStats.getQuests().ContainsKey(bossName) && pm.playerStats.getQuests()[bossName] == 0){
                activeTriggerOnlyOnce = true;
                rocks.gameObject.SetActive(true);
                switch(bossName){
                    case "Drakgon":
                        audioManager.PlaySound(bossName);
                        cameraMovement.maxBorder = new Vector2(6.35f, 31.5f);
                        cameraMovement.minBorder = new Vector2(6.35f, 31.5f);
                        camera.orthographicSize = 14f;
                        drakgon.gameObject.SetActive(true);
                        break;
                    case "Wolf Lord":
                        audioManager.PlaySound(bossName);
                        cameraMovement.maxBorder = new Vector2(2.5f, -10f);
                        cameraMovement.minBorder = new Vector2(2.5f, -10f);
                        camera.orthographicSize = 14f;
                        wolfBoss.gameObject.SetActive(true);
                        break;
                    default:
                        audioManager.StopSound("Mountain");
                        audioManager.PlaySound(bossName);
                        cameraMovement.maxBorder = new Vector2(-60.5f, 11.5f);
                        cameraMovement.minBorder = new Vector2(-60.5f, 11.5f);
                        camera.orthographicSize = 14f;
                        finalBoss.gameObject.SetActive(true);
                        break;
                }
           }
           
        }
    }
}
