﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/*
Handle and controll the NPC king
*/
public class King : MonoBehaviour
{
    public GameObject dialogBox;
    public GameObject require_quest;
    public GameObject receive_quest;
    public Text signText;
    public bool signActive;
    public string text;
    private bool inRange;
    private Vector2 playerDirect;
    private PlayerMovement pm;

    public Quest quest;
    public GameObject quest_ui;
    public Text quest_title;
    public Text quest_describ;
    public Text quest_gold;
    public Text quest_exp;

    // Start is called before the first frame update
    void Start()
    {
        quest_title.text = quest.qst_title;
        quest_describ.text = quest.qst_description;
        quest_gold.text += " " + quest.qst_gold+"(G)";
        quest_exp.text += " " + quest.qst_exp+"";
    }

    // Update is called once per frame
    void Update()
    {
       
        if(inRange && (pm.getCurrentState() == "Normal" || pm.getCurrentState() == "Standing" )) {
            playerDirect = pm.getDirects()[0];
            if(Input.GetKeyDown(KeyCode.Z) && playerDirect.y == 1) {
                if(pm.playerStats.getPlayerLevel() >= 30){
                    if(!pm.playerStats.getQuests().ContainsKey(quest.qst_title)){
                        if(!dialogBox.activeInHierarchy) {
                            pm.setState(1);
                            quest_ui.SetActive(true);
                            dialogBox.SetActive(true);
                            signText.text = text;
                        }
                    }
                    else {
                        if(!dialogBox.activeInHierarchy) {
                            pm.setState(1);
                            dialogBox.SetActive(true);
                            switch(pm.playerStats.getQuests()[quest.qst_title]) {
                                case 0:
                                    signText.text = "King: What are you looking for, hero? my soldiers cant defeat him";
                                    break;
                                case 1:
                                    receive_quest.SetActive(false);
                                    pm.playerStats.gainPlayerGold(quest.qst_gold);
                                    pm.playerStats.gainCurrentEXP(quest.qst_exp);
                                    pm.playerStats.getQuests()[quest.qst_title] = -1;
                                    signText.text = "King: HOHOH!! you are really strong, hero. Stronger than my all soliders!!!. HOHOHOHO!! Here is your reward as I promised, thank you! HOHOHOHOHO!!!";
                                    break;
                                default:
                                    signText.text = "King: HOHOHOH!!! young hero, Welcome to my castle!!!! HOHOHO You look quite small, hero";
                                    break;
                            }
                        }
                        else {
                            pm.setState(0);
                            dialogBox.SetActive(false);
                        }
                    }
                }
                else {
                    if(!dialogBox.activeInHierarchy) {
                        pm.setState(1);
                        dialogBox.SetActive(true);
                        signText.text = "King: HOHOHOH!!! young hero, Welcome to my castle!!!! HOHOHO You look quite small, hero";
                    }
                    else {
                        pm.setState(0);
                        dialogBox.SetActive(false);
                    }
                }
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D other) {
       if(other.CompareTag("Player")) {
            inRange = true;
            pm = other.GetComponent<PlayerMovement>();
            if(pm.playerStats.getPlayerLevel() >= 30 && !pm.playerStats.getQuests().ContainsKey(quest.qst_title)){
                require_quest.SetActive(true);
            }
            else {
                require_quest.SetActive(false);
            }

            if(pm.playerStats.getQuests().ContainsKey(quest.qst_title) && pm.playerStats.getQuests()[quest.qst_title] == 1){
                receive_quest.SetActive(true);
            }
            else {
                receive_quest.SetActive(false);
            }
       }
    }
    
    public void OnAcceptrButton() {
        pm.setState(0);
        dialogBox.SetActive(false);
        pm.playerStats.AddQuest(quest.qst_title, 0);
        require_quest.SetActive(false); 
        quest_ui.SetActive(false);
    }

    private void OnTriggerExit2D(Collider2D other) {
      if(other.CompareTag("Player")) {
          inRange = false;
          dialogBox.SetActive(false);
      }
    }
}
