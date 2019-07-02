using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defender3Controls : MonoBehaviour
{
    #region Variable init
    //public variable init
    public float startTimeBtwAttack;


    //priavte variable init
    private float timeBtwAttack;
    private GameObject parentObj;
    private bool lookingRight;
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
        if(Manager.defender == 1){
            AnimateDefender();
        }
    }

    //Player movement settings and animations
    void AnimateDefender(){
        float hMovement = parentObj.GetComponent<Character1Controls>().GetHSpeed();
        float vMovement = parentObj.GetComponent<Character1Controls>().GetVSpeed();

        //movement animation
        if(hMovement == 0 && vMovement == 0){
            anim.SetBool("isWalking", false);
        }else{
            anim.SetBool("isWalking", true);
        }
    }
}
