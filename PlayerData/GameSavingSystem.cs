using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;

/*
Handle the game saving/loading system
*/
public static class GameSavingSystem {

    public static string save_file = "/player.sav";

    public static void SaveGame(PlayerMovement player, string _scene, DateTime dte){
        BinaryFormatter formatter = new BinaryFormatter();

        string local_path = Application.persistentDataPath + save_file;
        FileStream fileStream = new FileStream(local_path, FileMode.Create);

        PlayerData playerData = new PlayerData(player, _scene, dte);
        formatter.Serialize(fileStream, playerData);
        
        fileStream.Close();
    }

    public static PlayerData LoadGame() {
        
        string local_path = Application.persistentDataPath + save_file;
        if(File.Exists(local_path)) {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream fileStream = new FileStream(local_path, FileMode.Open);

            PlayerData playerData = formatter.Deserialize(fileStream) as PlayerData;
            fileStream.Close();

            return playerData;
        }
        else {
            Debug.Log("No found file!!");
            return null;
        }
    }
}
