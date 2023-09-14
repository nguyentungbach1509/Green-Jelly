using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfBossController : MonoBehaviour
{   

    WolfBoss wolfBoss;

    // Start is called before the first frame update
    void Start()
    {
        if(GameObject.FindGameObjectWithTag("Bosses") != null) {
            wolfBoss = GameObject.FindGameObjectWithTag("Bosses").GetComponent<WolfBoss>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(wolfBoss.getTrackTarget() != Vector2.zero && !wolfBoss.getDuringTrackCooldown()) {
            wolfBoss.gameObject.SetActive(true);
        }
    }
}