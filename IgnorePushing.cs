using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnorePushing : MonoBehaviour
{

    public BoxCollider2D NPC;
    public CircleCollider2D Player;

    // Start is called before the first frame update
    void Start()
    {
        Physics2D.IgnoreCollision(NPC, Player, true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
