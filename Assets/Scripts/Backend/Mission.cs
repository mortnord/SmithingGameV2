using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission : MonoBehaviour
{
    public float Time_remaining = 0;
    public int type_of_object_for_mission = 0;
    public int quality_of_object_for_mission = 0;
    public float time_for_mission = 0;

    public SpriteRenderer spriteRenderer;

    public void setTime(float time_remaining)
    {
        Time_remaining = time_remaining; //Tiden vi har igjen
        time_for_mission = Time_remaining - 180; //Rewrite all this shit. 
        print("i am alive");
    }
    
    
    
}
