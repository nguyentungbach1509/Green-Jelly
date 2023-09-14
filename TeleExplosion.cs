using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Boss's teleport skill and effect
*/
public class TeleExplosion : MonoBehaviour
{   
    [SerializeField]
    public SlashEffect slashfx;
    
    // Start is called before the first frame update
    void Start()
    {
        Explosion();
    }

    void Explosion() {
        ((Instantiate(slashfx, transform.position, Quaternion.identity)).GetComponent<SlashEffect>()).positionTarget = new Vector2(-90f, transform.position.y);
        ((Instantiate(slashfx, transform.position, Quaternion.identity)).GetComponent<SlashEffect>()).positionTarget = new Vector2(-30f, transform.position.y);
        ((Instantiate(slashfx, transform.position, Quaternion.identity)).GetComponent<SlashEffect>()).positionTarget = new Vector2(transform.position.x, 40f);
        ((Instantiate(slashfx, transform.position, Quaternion.identity)).GetComponent<SlashEffect>()).positionTarget = new Vector2(transform.position.x, -16f);
        ((Instantiate(slashfx, transform.position, Quaternion.identity)).GetComponent<SlashEffect>()).positionTarget = new Vector2(-90f, 40f);
        ((Instantiate(slashfx, transform.position, Quaternion.identity)).GetComponent<SlashEffect>()).positionTarget = new Vector2(-30f, 40f);
        ((Instantiate(slashfx, transform.position, Quaternion.identity)).GetComponent<SlashEffect>()).positionTarget = new Vector2(-90f, -16f);
        ((Instantiate(slashfx, transform.position, Quaternion.identity)).GetComponent<SlashEffect>()).positionTarget = new Vector2(-30f, -16f);
        Destroy(gameObject);
    } 
}
