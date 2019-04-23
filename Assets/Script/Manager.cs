using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    #region Variable init
    //static variable
    public static int defender;
    public static int playerHealth;
    public static Animator CameraAnim;
    public Slider healthBar;
    public static float inviTime = 2.0f;
    public static float inviTimer;
    public static bool playerInvincable;

    //public variable
    
    //private variable init
    
    
    #endregion // end of the variable init

    // Start is called before the first frame update
    void Start()
    {
        defender = 1;
        playerHealth = 100;
        playerInvincable = false;
        CameraAnim = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.value = playerHealth;
        ChangeDefenderControl();

        /*if(playerInvincable){
            inviTimer -= Time.deltaTime;
            if(inviTimer < 0){
                playerInvincable = false;
            }
        }*/
    }

    void ChangeDefenderControl(){
        if (Input.GetKeyDown("space")){
            defender *= -1;
        }
    }

    public static void PlayerTakeDamage(int amount){
        /*if(playerInvincable){
            return;
        }else{
            playerInvincable = true;
            inviTimer = inviTime;
        }*/
        CameraAnim.SetTrigger("shake");
        playerHealth -= amount;
    }


}
