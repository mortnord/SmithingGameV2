using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission_System : MonoBehaviour
{

    TimerScript Timer_Object;
    Object_Creation Generation_Object;
    public float time_remaining;
    public bool created_mission = true;

    public List<GameObject> Missions_in_UI = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        Timer_Object = Find_Components.find_Timer_Object();
        Generation_Object = Find_Components.find_Object_Creation();
    }

    // Update is called once per frame
    void Update()
    {
        if(created_mission)
        {
            created_mission = false;
            create_mission();
        }
    }
    void create_mission()
    {
        print("create mission");
        time_remaining = Timer_Object.time_Remaining;
        GameObject mission = Generation_Object.create_card_with_mission(time_remaining, new Vector3(0,0,0));
        mission.GetComponent<Mission>().quality_of_object_for_mission = Generation_Object.get_random_quality();
        mission.GetComponent<Mission>().type_of_object_for_mission = Generation_Object.get_random_type();
    }
}
