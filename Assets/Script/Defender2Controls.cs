using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defender2Controls : MonoBehaviour
{
    #region Variable init
    //public variable init
    public float speed = 5.0f;

    //priavte variable init
    private float hMovement;
    private float vMovement;
    private bool attack;
    private bool lookingRight;
    private Animator anim;     

    #endregion //end of variable init


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        lookingRight = true;

    }

    // Update is called once per frame
    void Update()
    {
        if(Manager.defender == -1){
            MovePlayer();
        }
        FaceMouse();
    }

    
    void MovePlayer(){
        hMovement = Input.GetAxisRaw("Horizontal"); 
        vMovement = Input.GetAxisRaw("Vertical");
        attack = Input.GetMouseButtonDown(0);


        Vector2 position = transform.position;

        position.x = position.x + speed * hMovement * Time.deltaTime;
        position.y = position.y + speed * vMovement * Time.deltaTime;

        transform.position = position;

        #region Animation Control
        //movement animation
        if(hMovement == 0 && vMovement == 0){
            anim.SetBool("isWalking", false);
        }else{
            anim.SetBool("isWalking", true);
        }
    }

    void FaceMouse(){
        //local variable init
        float dist;
        Vector3 myScale = transform.localScale;
        Vector3 mousePos = Input.mousePosition;

        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        //calculating distance between enemy and the player
        dist = mousePos.x - transform.position.x;

        /* Make enemy look at player
        * If player is to the left of the enemy and enemy is looking to the right flip the x scale to face left
        * If player is to the right of the enemy and enemy is looking to the left flip the x scale to face right
        */
        if((dist <= 0 && lookingRight) || (dist > 0 && !lookingRight)){
            lookingRight = !lookingRight;
            myScale.x *= -1;
        }

        //impliment the transformation
        transform.localScale = myScale;

        #endregion //end of animation control
    }
}
