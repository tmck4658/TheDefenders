using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defender2Controls : MonoBehaviour
{
    #region Variable init
    //public variable init

    //priavte variable init
    private bool attack;
    private bool lookingRight;
    private GameObject parentObj;
    private Animator anim;     

    #endregion //end of variable init


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        parentObj = gameObject.transform.parent.gameObject;
        lookingRight = true;

    }

    // Update is called once per frame
    void Update()
    {
        if(Manager.defender == -1){
            AnimateDefender();
            FaceMouse();
        }
        
    }

    
    //Player movement settings and animations
    void AnimateDefender(){
        float hMovement = parentObj.GetComponent<Character2Controls>().GetHSpeed();
        float vMovement = parentObj.GetComponent<Character2Controls>().GetVSpeed();

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
    }
}
