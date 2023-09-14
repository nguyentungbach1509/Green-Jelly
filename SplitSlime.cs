using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplitSlime : SimpleSlime
{
    
    void Update() {
        if(currentHealth <= 0) {
            DestroySlime();
            Destroy(gameObject, 1f);
        }
    }

    // Start is called before the first frame update
    void FixedUpdate()
    {
        if(currentHealth > 0) {
            DistanceChecking();
        }
       
    }
}
