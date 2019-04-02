using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    #region Variable init
    public static int defender;
    public static int playerHealth;
    public static Animator CameraAnim;
    public Slider healthBar;
    //private variable init
    
    #endregion // end of the variable init

    // Start is called before the first frame update
    void Start()
    {
        defender = 1;
        playerHealth = 100;
        CameraAnim = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.value = playerHealth;
        ChangeDefenderControl();
    }

    void ChangeDefenderControl(){
        if (Input.GetKeyDown("space")){
            defender *= -1;
        }
    }

    public static void PlayerTakeDamage(int amount){
        CameraAnim.SetTrigger("shake");
        playerHealth -= amount;
    }


}
