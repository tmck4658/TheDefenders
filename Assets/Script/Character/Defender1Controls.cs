using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defender1Controls : MonoBehaviour
{
    #region Variable init
    //public variable init
    public float startTimeBtwAttack;
    public Transform attackPos;
    public float attackRange;
    public LayerMask whatIsEnemies;
    public int damage;


    //priavte variable init
    private bool attack;
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
            CheckForAttack();
            FaceMouse();
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
