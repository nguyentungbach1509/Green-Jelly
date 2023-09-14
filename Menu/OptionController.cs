using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OptionController : MonoBehaviour
{
    public GameObject backButton;
    // Start is called before the first frame update
     void Update() 
    {
        EventSystem.current.SetSelectedGameObject(backButton);
    }

  
}
