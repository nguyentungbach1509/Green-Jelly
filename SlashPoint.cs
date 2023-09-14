using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Handle and respawn slash object
*/
public class SlashPoint : MonoBehaviour
{   

    public SlashEffect slash;
    public string directionName;
    private WolfBoss wb;
    private bool spawnSlash = false;

    // Start is called before the first frame update
    void Start()
    {
        wb = transform.parent.gameObject.GetComponent<WolfBoss>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate() {
        if(spawnSlash == false) {
            spawnSlash = true;
            ((Instantiate(slash, transform.position, Quaternion.identity)).GetComponent<SlashEffect>()).positionTarget = wb.getSlashPoints()[directionName];
        }
    }
    

    public void setSpawnSlash(bool spawn) {
        spawnSlash = spawn;
    }

}
