using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchaBodyAnim : MonoBehaviour
{
    private bool lookingRight;

    // Start is called before the first frame update
    void Start()
    {   
        lookingRight = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(Manager.defender == 1){
            FaceMouse();
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
