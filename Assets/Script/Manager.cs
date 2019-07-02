using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
    public static int level;

    //public variable
    public TextMeshProUGUI levelText;
    
    //private variable init
    [SerializeField]
    private GameObject gameOverUI;
    
    
    #endregion // end of the variable init

    // Start is called before the first frame update
    void Start()
    {
        level = 1;
        defender = 1;
        playerHealth = 100;
        playerInvincable = false;
        CameraAnim = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.value = playerHealth;
        levelText.text = "Level: " + level;
        ChangeDefenderControl();

        if(playerHealth <= 0){
            EndGame();
        }
        /*if(playerInvincable){
            inviTimer -= Time.deltaTime;
            if(inviTimer < 0){
                playerInvincable = false;
            }
        }*/
    }

    //change the controle between the player's charaters
    void ChangeDefenderControl(){
        if (Input.GetKeyDown("space")){
            defender *= -1;
        }
    }

    //show the end game screen
    void EndGame(){
        gameOverUI.SetActive(true);
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
