using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetupScript : MonoBehaviour
{
    TimerScript timer_script_object;
    // Start is called before the first frame update
    void Start()
    {
        find_Timer_Object();
    }

    private void find_Timer_Object()
    {
        GameObject timer_object = GameObject.Find("TimerText");
        timer_script_object = timer_object.GetComponent<TimerScript>();

    }

    // Update is called once per frame
    void Update()
    {

    }
}
