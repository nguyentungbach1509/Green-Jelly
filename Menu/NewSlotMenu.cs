using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class NewSlotMenu : MonoBehaviour
{
    public GameObject saveSlotButton;
    public int onceTime = 0;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(gameObject.activeInHierarchy){
            if(onceTime == 0) {
                EventSystem.current.SetSelectedGameObject(saveSlotButton);
                onceTime++;
            }
        }
    }
}
