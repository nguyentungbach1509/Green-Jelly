using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/*
Controll the states and movement of NPC kid girl
*/
public class KidGirl : MonoBehaviour
{
    private Vector3 vectorDirection;
    private Transform girlTransform;
    public float moveSpeed;
    private Rigidbody2D rb;
    private Animator animator;
    public Collider2D borders;
    private bool isMoving;
    public float minMovingTime;
    public float maxMovingTime;
    private float movingTimeSeconds;
    public float minDelayTime;
    public float maxDelayTime;
    private float delayTimeSeconds;
    public GameObject dialogBox;
    public Text signText;
    public bool signActive;
    public string text;
    private bool inRange;
    private Vector2 playerDirect;
    private PlayerMovement pm;
    private SpriteRenderer sprite;



    // Start is called before the first frame update
    void Start()
    {
        movingTimeSeconds = Random.Range(minMovingTime, maxMovingTime);
        delayTimeSeconds = Random.Range(minDelayTime, maxDelayTime);
        girlTransform = GetComponent<Transform>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        DirectionChange();
    }

    // Update is called once per frame
    void Update()
    {

        ShowDialog();

        if(isMoving) {
            movingTimeSeconds -= Time.deltaTime;

            if(movingTimeSeconds <= 0) {
                movingTimeSeconds = Random.Range(minMovingTime, maxMovingTime);
                isMoving = false;
            }

            if(!inRange) {
                Move();
            }
        }
        else {
            delayTimeSeconds -= Time.deltaTime;

            if(delayTimeSeconds <= 0) {
                if(!inRange) {
                    DirectionSelection();
                
                }
                
                isMoving = true;
                delayTimeSeconds = Random.Range(minDelayTime, maxDelayTime);
            }
        }

    }

    private void Move() {

        Vector3 temp = girlTransform.position + vectorDirection * moveSpeed * Time.deltaTime;

        if(borders.bounds.Contains(temp)) {

            rb.MovePosition(temp);
        }
        else {

            DirectionChange();
        }

        
    }
 
    void DirectionChange() {

        int direction = Random.Range(0, 4);
        

        switch(direction) {
            case 0:
                vectorDirection = Vector3.right;
                break;
            case 1:
                vectorDirection = Vector3.left;
                break;
            case 2:
                vectorDirection = Vector3.down;
                break;
            case 3:
                vectorDirection = Vector3.up;
                break;
            default:
                break;
        }

        UpdateAnimation();
    }

    void UpdateAnimation() {
        animator.SetFloat("Horizontal", vectorDirection.x);
        animator.SetFloat("Vertical", vectorDirection.y);
    }


    private void DirectionSelection() {
        Vector3 temp = vectorDirection;
        DirectionChange();
        int count = 0;
        while(temp == vectorDirection && count < 100) {
            
            DirectionChange();
            count++;

        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        
        DirectionSelection();
    }


    private void ShowDialog() {
        if(inRange && (pm.getCurrentState() == "Normal" || pm.getCurrentState() == "Standing" )) {
            playerDirect = pm.getDirects()[0];
            if(Input.GetKeyDown(KeyCode.Z)) {
                if(dialogBox.activeInHierarchy) {
                    pm.setState(0);
                    dialogBox.SetActive(false);
                }
                else {
                    pm.setState(1);

                    if(playerDirect.x == -1) {
                        Debug.Log("Right");
                        vectorDirection = Vector3.right;
                        
                    }
                    else if(playerDirect.x == 1) {
                        Debug.Log("left");
                        vectorDirection = Vector3.left;
                    }
                    else if(playerDirect.y == 1) {
                        Debug.Log("down");
                        vectorDirection = Vector3.down;
                        
                    }
                    else if(playerDirect.y == -1) {
                        Debug.Log("up");
                        vectorDirection = Vector3.up;
                    }

                    UpdateAnimation();

                    dialogBox.SetActive(true);
                    signText.text = text;
                }
            }

        }
    }


    private void OnTriggerEnter2D(Collider2D other) {
       if(other.CompareTag("Player")) {
           inRange = true;
           rb.bodyType = RigidbodyType2D.Static;
           pm = other.GetComponent<PlayerMovement>();
           if(girlTransform.position.y < pm.transform.position.y) {
               sprite.sortingOrder = 1;
           }
           else {
               sprite.sortingOrder = -1;
           }
       }
    }


    private void OnTriggerExit2D(Collider2D other) {
      if(other.CompareTag("Player")) {
          rb.bodyType = RigidbodyType2D.Dynamic;
          rb.gravityScale = 0f;
          inRange = false;
          dialogBox.SetActive(false);
      }
    }
}
