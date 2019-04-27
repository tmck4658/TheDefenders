using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameWalkerAbility : MonoBehaviour
{
     #region Variable init
    //public variable
    public GameObject projectile;
    public float shootingSpeed;

    //private variable
    private EnemyMovement myMoveScript;
    private float shootingSpeedCounter;
    private Animator anim; 
    
    #endregion Variable init

    // Start is called before the first frame update
    void Start()
    {
        myMoveScript = gameObject.GetComponent<EnemyMovement>();
        shootingSpeedCounter = shootingSpeed;
        anim = anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(shootingSpeedCounter < 0){
            myMoveScript.SetAttack(true);
            anim.SetTrigger("attack");
            shootingSpeedCounter = shootingSpeed;
        }else{
            shootingSpeedCounter-= Time.deltaTime;
            myMoveScript.SetAttack(false);
        }
    }

    void Attack(){
        GameObject flame = Instantiate(projectile, transform.position, Quaternion.identity);
        flame.GetComponent<ProjectileScript>().SetTarget(myMoveScript.curTarget.GetComponent<Transform>());
    }
        
        
    
}
