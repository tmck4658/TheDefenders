using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    #region Variable init
    //public variable
    

    //private variable
    private Transform target;
    private bool lookingRight; 
    private int knockBackAmount = 20;
    private float speed = 1f;
    private int health;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        health = 100;
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        lookingRight = true;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        FaceTarget(target);
        RageCheck();
        CheckForDeath();

    }

    // Make enemy face the player
    private void FaceTarget(Transform target)
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

    //if bellow half health increas enemy's speed
    private void RageCheck(){
        if(health < 50){
            speed = 3;
        }
    }

    private void CheckForDeath(){
        if(health <= 0){
            Destroy(gameObject);
        }
    }
}
