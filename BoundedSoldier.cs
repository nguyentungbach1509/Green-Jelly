using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Controll the states and movement of NPC soliders.
*/
public class BoundedSoldier : MonoBehaviour
{

    private Vector3 vectorDirection;
    private Transform soldierTransform;
    public float moveSpeed;
    private Rigidbody2D rb;
    private Animator animator;
    public Collider2D borders;

    // Start is called before the first frame update
    void Start()
    {
        soldierTransform = GetComponent<Transform>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        DirectionChange();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move() {

        Vector3 temp = soldierTransform.position + vectorDirection * moveSpeed * Time.deltaTime;

        if(borders.bounds.Contains(temp)) {

            rb.MovePosition(temp);
        }
        else {

            DirectionChange();
        }

        
    }
 
    void DirectionChange() {
        int direction = Random.Range(0, 2);

        switch(direction) {
            case 0:
                vectorDirection = Vector3.right;
                break;
            case 1:
                vectorDirection = Vector3.left;
                break;
            default:
                break;
        }

        UpdateAnimation();
    }

    void UpdateAnimation() {
        animator.SetFloat("Horizontal", vectorDirection.x);
    }
}
