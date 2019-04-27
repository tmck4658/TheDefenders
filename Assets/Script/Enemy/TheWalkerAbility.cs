using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheWalkerAbility : MonoBehaviour
{
    private EnemyMovement myMoveScript;
    // Start is called before the first frame update
    void Start()
    {
        myMoveScript = gameObject.GetComponent<EnemyMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        RageCheck();
    }

    //if bellow half health increas enemy's speed
    void RageCheck(){

        if(myMoveScript.GetHealth() < 50){
            myMoveScript.SetSpeed(3f);
        }
    }
}
