using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill3_Illusions : MonoBehaviour
{
    private Vector2 illuDirection;
    private Animator animator;
    private Vector2 target;
    private Transform playerTransform;
    private bool explosion;
    public float speed;
    private FinalBoss finalBoss;
    private bool charge;
    
    
    // Start is called before the first frame update
    void Start()
    {   
        if(GameObject.FindGameObjectWithTag("Player") != null) {
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
            Vector3 direct = (playerTransform.position - transform.position).normalized;
            illuDirection = new Vector2(direct.x, direct.y);
            Ray2D aline = new Ray2D(transform.position, illuDirection);
            target = aline.GetPoint(Vector2.Distance(playerTransform.position, transform.position) + 3f);
        }

        if(GameObject.Find("FinalBoss") != null) {
            finalBoss = GameObject.Find("FinalBoss").GetComponent<FinalBoss>();
        }

        charge = false;
        animator = GetComponent<Animator>();   
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x == target.x && transform.position.y == target.y) {
            animator.SetTrigger("Idle");
            if(transform.position.x >= -46f || transform.position.x <= - 75 || 
            transform.position.y >= 19f || transform.position.y <= 3f) {

            }
            else {
                if(finalBoss != null) {
                    finalBoss.setTeleportPoint(transform.position);
                    Destroy(gameObject);
                }
            }
        }   
        else {
            transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
            
        }

        ControllIlluDirect();
    }

    public void ControllIlluDirect() {
        animator.SetFloat("Horizontal", illuDirection.x);
        animator.SetFloat("Vertical", illuDirection.y);
    }

    public void setIlluDirection(Vector2 drect) {
        illuDirection = drect;
    }

    public void setExplosion(bool value) {
        explosion = value;
    }
}
