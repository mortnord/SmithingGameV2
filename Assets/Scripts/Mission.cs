using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission : MonoBehaviour
{
    public float Time_remaining = 0;
    public int type_of_object_for_mission = 0;
    public int quality_of_object_for_mission = 0;
    // Start is called before the first frame update
    
    public void setTime(float time_remaining)
    {
        Time_remaining = time_remaining;
        print("i am alive");
    }
    
    // Update is called once per frame
    
}
