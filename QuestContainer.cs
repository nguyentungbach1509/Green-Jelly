using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class QuestContainer : MonoBehaviour
{
    public GameObject accept_button;

    void Start() {
        EventSystem.current.SetSelectedGameObject(accept_button);
    }
}
