using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    #region Variable init
    public static int defender;
    #endregion // end of the variable init

    // Start is called before the first frame update
    void Start()
    {
        defender = 1;
    }

    // Update is called once per frame
    void Update()
    {
        ChangeDefenderControl();
    }

    void ChangeDefenderControl(){
        if (Input.GetKeyDown("space")){
            defender *= -1;
        }
    }
}
