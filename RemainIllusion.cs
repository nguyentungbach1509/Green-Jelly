using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemainIllusion : MonoBehaviour
{
    private Animator animator;
    public Vector2 illuDirect;
   
    // Start is called before the first frame update
    void Start()
    {   
        animator = GetComponent<Animator>();
        setIlluDirection(illuDirect);
    }


    private void setIlluDirection(Vector2 direct) {
        animator.SetFloat("Horizontal", direct.x);
        animator.SetFloat("Vertical", direct.y);
    }


    public void DestroyIllu() {
        Destroy(gameObject);
    }

}
