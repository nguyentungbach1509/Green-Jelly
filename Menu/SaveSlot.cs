using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveSlot : MonoBehaviour
{
    public Text newText;
    public Text slotNo;
    public Text slotLv;
    public Text slotDate;

    [SerializeField]
    public string path;
    // Start is called before the first frame update
    void Start()
    {
        GameSavingSystem.save_file = path;
        PlayerData data = GameSavingSystem.LoadGame();
        if(data != null) {
            newText.gameObject.SetActive(false);
            slotNo.gameObject.SetActive(true);
            slotLv.gameObject.SetActive(true);
            slotDate.gameObject.SetActive(true);
            slotDate.text += " " + data.date;
            slotLv.text += " " + data.level;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
