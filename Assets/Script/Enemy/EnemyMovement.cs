using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    #region Variable init
    //public variable
    public float attackSpeed;
    public bool lookingRight;
    public float stoppingDist;
    public float retreatDist;
    public float speed;
    public GameObject curTarget;

    //private variable
    private float attackSpeedCounter;
    private int knockBackAmount = 20;
    private float health;
    private bool stop = false;
    private bool attack = false;
    private Animator anim; 
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        health = 100;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        curTarget = FindTarget();
        FollowTarget(curTarget);
        CheckForDeath();
    }

    //find the closes target
    GameObject FindTarget(){
        //defining target objects
        GameObject char1 = GameObject.FindGameObjectWithTag("Character1");
        GameObject char2 = GameObject.FindGameObjectWithTag("Character2");
        //calculaiting the distance between enemy and character(defenders)
        float distToChar1 = Vector2.Distance(char1.GetComponent<Transform>().position, transform.position);
        float distToChar2 = Vector2.Distance(char2.GetComponent<Transform>().position, transform.position);

        // checking to see which defender is closer
        if(distToChar1 <= distToChar2){
            return char1;
        }else{
            return char2;
        }
    }

    //follow taret
    void FollowTarget(GameObject target){
        Transform targetTransform = target.GetComponent<Transform>();
        float dist = Vector2.Distance(targetTransform.position, transform.position);

        if(!attack){
            if (dist > stoppingDist){
                stop = false;
                FaceTarget(targetTransform);
                MoveTo(targetTransform);
            }else if(dist < stoppingDist && dist > retreatDist){
                stop = true;
                anim.SetBool("isWalking", false);
            }else if(dist < retreatDist){
                stop = false;
                FaceTarget(targetTransform);
                MoveAway(targetTransform);
            }
        }
        
    }

    //move to target
    void MoveTo(Transform pos){
        if(!stop){
            anim.SetBool("isWalking", true);
            transform.position = Vector2.MoveTowards(transform.position, pos.position, speed * Time.deltaTime);
        }
    }

    //move away from target
    void MoveAway(Transform pos){
        if(!stop){
            anim.SetBool("isWalking", true);
            transform.position = Vector2.MoveTowards(transform.position, pos.position, -speed * Time.deltaTime);
        }
    }

    // Make enemy face the player
    void FaceTarget(Transform target)
    {
        //local variable init
        float dist;
        Vector3 myScale = transform.localScale;

        //calculating distance between enemy and the player
        dist = target.position.x - transform.position.x;

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

    //Damages enemy 
    public void TakeDamage(int amount){
        //reduce health amount
        health -= amount;

        //knockback enemy
        Vector2 position = transform.position;
        if(lookingRight){
            position.x = position.x - knockBackAmount * Time.deltaTime;
        }else{
            position.x = position.x + knockBackAmount * Time.deltaTime;
        }
        transform.position = position;

        Debug.Log("health: " + health);
    }

    //Deal damage to player if in contact with the enemy
    void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Character1") || other.CompareTag("Character2")){
            Manager.PlayerTakeDamage(10);
        }
    }

    //deal damage to the player every second = attack speed if still in contact
    void OnTriggerStay2D(Collider2D other){
        //Manager.PlayerTakeDamage(10);
        if(other.CompareTag("Character1") || other.CompareTag("Character2")){
            if(attackSpeedCounter < 0){
                Manager.PlayerTakeDamage(10);
                attackSpeedCounter = attackSpeed;
            }else{
                attackSpeedCounter-= Time.deltaTime;
            }
        }
    }   

    private void CheckForDeath(){
        if(health <= 0){
            Destroy(gameObject);
        }
    }

    #region Getters
    public float GetHealth(){
        return this.health;
    }

    public float GetSpeed(){
        return this.speed;
    }

    public GameObject GetTarget(){
        return this.curTarget;
    }

    #endregion end of Getters

    #region Setters
    public void SetStop(bool stop){
        this.stop = stop;
    }

    public void SetAttack(bool attack){
        this.attack = attack;
    }

    public void SetSpeed(float speed){
        this.speed = speed;
    }
    #endregion end of Setters
}
