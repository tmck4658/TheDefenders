using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defender1Control : MonoBehaviour
{
    #region Variable init
    //public variable init
    public float speed = 5.0f;


    //priavte variable init
    private float hMovement;
    private float vMovement;
    private bool attack;
    private Animator anim;     

    #endregion //end of variable init


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        if(Manager.defender == 1){
            MovePlayer();
        }
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

        //attack animation
        if(attack){
            anim.SetTrigger("attacking");
        }

        #endregion //end of animation control
    }

    void ResetAnimation(){
        anim.SetBool("isWalking", false);
    }
}
