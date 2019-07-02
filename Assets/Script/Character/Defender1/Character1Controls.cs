using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character1Controls : MonoBehaviour
{
    //public variable
    public float speed = 5.0f;
    public int frostTolerence = 2;
    public GameObject defender1, defender2;

    //private variable
    private float hMovement;
    private float vMovement;
    private int curDefender = 1;

    // Start is called before the first frame update
    void Start()
    {
        defender1.gameObject.SetActive(true);
        defender2.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //make sure player is controlling character 1
        if(Manager.defender == 1){
            MoveCharacter();
            CheckForDefenderChange();
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

    void CheckForDefenderChange(){
        bool change = Input.GetKeyDown(KeyCode.E);
        
        if(change){
            ChangeDefender();
        }
    }

    void ChangeDefender(){
        switch(curDefender){
            //if curDefender = 1 and E Pressed
            case 1: 
                curDefender = 2;
                defender1.gameObject.SetActive(false);
                defender2.gameObject.SetActive(true);
                break;
            //if curDefender = 2 and E Pressed
            case 2:
                curDefender = 1;
                defender1.gameObject.SetActive(true);
                defender2.gameObject.SetActive(false);
                break;
        }
    }

    //make player imobile if frozen
    public void Chilled(float amount){

    }

    //slow down the character
    public void Slowdown(float time){
        speed -= 2;
    }

    public float GetHSpeed(){
        return this.hMovement;
    }

    public float GetVSpeed(){
        return this.vMovement;
    }
}
