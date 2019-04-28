using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character2Controls : MonoBehaviour
{
    //public variable
    public float speed = 4.0f;

    //private variable
    private float hMovement;
    private float vMovement;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //make sure player is controlling character 1
        if(Manager.defender == -1){
            MoveCharacter();
        }
    }

    //character movement settings and animations
    void MoveCharacter(){
        hMovement = Input.GetAxisRaw("Horizontal"); 
        vMovement = Input.GetAxisRaw("Vertical");
        


        Vector2 position = transform.position;

        position.x = position.x + speed * hMovement * Time.deltaTime;
        position.y = position.y + speed * vMovement * Time.deltaTime;
        transform.position = position;
    }

    public float GetHSpeed(){
        return this.hMovement;
    }

    public float GetVSpeed(){
        return this.vMovement;
    }
}
