using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defender1Controls : MonoBehaviour
{
    #region Variable init
    //public variable init
    public float speed = 5.0f;
    public float startTimeBtwAttack;
    public Transform attackPos;
    public float attackRange;
    public LayerMask whatIsEnemies;
    public int damage;


    //priavte variable init
    private float hMovement;
    private float vMovement;
    private bool attack;
    private float timeBtwAttack;
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
        if(Manager.defender == 1){
            MovePlayer();
            CheckForAttack();
            FaceMouse();
        }

        
    }

    //Player movement settings and animations
    void MovePlayer(){
        #region Movement Setting
        hMovement = Input.GetAxisRaw("Horizontal"); 
        vMovement = Input.GetAxisRaw("Vertical");
        


        Vector2 position = transform.position;

        position.x = position.x + speed * hMovement * Time.deltaTime;
        position.y = position.y + speed * vMovement * Time.deltaTime;
        transform.position = position;
        #endregion // end of movement settings


        #region  Movement Animation Control
        //movement animation
        if(hMovement == 0 && vMovement == 0){
            anim.SetBool("isWalking", false);
        }else{
            anim.SetBool("isWalking", true);
        }

        

        #endregion //end of animation control
    }

    //Checks to see if mouse button is pressed. If it is then attack
    void CheckForAttack(){
        attack = Input.GetMouseButton(0);

        //Sets the time between each attack
        if(timeBtwAttack <= 0){
            //attack animation
            if(attack){
                anim.SetTrigger("attacking");
                timeBtwAttack = startTimeBtwAttack;

                
            }
        } else {
            timeBtwAttack -=  Time.deltaTime;
        }
    }

    //Damage Enemy
    void DamageEnemy(){
        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemies);      //Seeking out all enemy in Enemies Layer
                
        for(int i = 0; i < enemiesToDamage.Length; i++){                                                                //Loops to damage all enemies in "Enemies" Layer
            enemiesToDamage[i].GetComponent<EnemyMovement>().TakeDamage(damage);                                        //Call TakeDamage function in EnemyMovement script to give damage equal to value assigned in "damage"
        }
    }
    //Draw Gizmos to help visualize attack area
    void OnDrawGizmosSelected(){
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
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
