using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Keeping the canvas when move into new scene
*/
public class KeepingCanvas : MonoBehaviour
{
   public static KeepingCanvas instance;

   void Awake() {
        
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            if(instance != this)
            {
                Destroy(gameObject);
            }
        }

        DontDestroyOnLoad(gameObject);
    
    }
}
