using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/*
Store player's data to save and load game
*/
[System.Serializable]
public class PlayerData {
    public float health;
    public float max_health;
    public int dmg;
    public int gold;
    public int armor;
    public float exp;
    public float max_exp;
    public int sword_lv;
    public int sword_dmg;
    public int level;
    
    public float[] position;
    public Dictionary<string, int> qsts;

    public float[] maxBorder;
    public float[] minBorder;
    public float[] currentPos;
    public string sceneName;
    public int difficult;

    public DateTime date;

    public PlayerData() {

    }

    public PlayerData(PlayerMovement player, string _scene, DateTime dte){ 
        health = player.playerStats.getPlayerCurrentHealth();
        max_health = player.playerStats.getPlayerMaxHealth();
        exp = player.playerStats.getPlayerCurrentEXP();
        max_exp = player.playerStats.getPlayerTotalEXP();
        dmg = player.playerStats.getPlayerDamage();
        armor = player.playerStats.getPlayerArmor();
        sword_lv = player.playerStats.getSwordLevel();
        sword_dmg = player.playerStats.getSwordDmg();
        gold = player.playerStats.getPlayerGold();
        level = player.playerStats.getPlayerLevel();
        qsts = player.playerStats.getQuests();
        difficult = player.playerStats.getDifficult();
        sceneName = _scene;
        position = new float[3];
        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
        position[2] = player.transform.position.z;
        date = dte;
    }
}
