using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetupScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        find_Timer_Object();
    }

    private void find_Timer_Object()
    {
        GameObject timer_object = GameObject.Find("TimerText");
        TimerScript timer = timer_object.GetComponent<TimerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
