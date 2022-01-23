using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission_System : MonoBehaviour
{

    TimerScript Timer_Object;
    Object_Creation Generation_Object;
    public float time_remaining;
    public bool completed_mission = true;
    public int amount_of_completed_missions = 0;
    public int difficulty = 0;

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
        if(Timer_Object.reset == true && completed_mission == true)
        {
            create_mission();
            Timer_Object.reset = false;
            completed_mission = false;
        }
        else if(Timer_Object.ekstra_mission_reset == true)
        {
            create_mission();
            Timer_Object.ekstra_mission_spawn = 30 - amount_of_completed_missions;
            if(Timer_Object.ekstra_mission_spawn < 10)
            {
                Timer_Object.ekstra_mission_spawn = 10;
            }
            Timer_Object.reset = false;
            Timer_Object.ekstra_mission_reset = false;
            completed_mission = false;
        }
        
    }
    void create_mission()
    {
        print("create mission");
        time_remaining = Timer_Object.time_Remaining;
        GameObject mission = Generation_Object.create_card_with_mission(time_remaining, new Vector3(0, 0, 0));
        Missions_in_UI.Add(mission);
        mission.GetComponent<Mission>().quality_of_object_for_mission = mission.GetComponentInChildren<Sword>().quality;
        mission.GetComponent<Mission>().type_of_object_for_mission = (int)Generation_Object.get_random_type();

        Timer_Object.reset_timer = 10;
        print("added");
    }
    public bool check_mission_success(GameObject Object_to_check)
    {
        if (Missions_in_UI.Count > 0)
        {
            for (int i = 0; i < Missions_in_UI.Count; i++)
            {
                if (Object_to_check.GetComponent<Sword>())
                {
                    if (Missions_in_UI[i].GetComponent<Mission>().quality_of_object_for_mission == Object_to_check.GetComponent<Sword>().quality)
                    {
                        if (Missions_in_UI[i].GetComponent<Mission>().type_of_object_for_mission == (int)Object_to_check.GetComponent<Sword>().mission_tag)
                        {
                            Destroy_Object(i);

                            return true;
                        }

                    }
                }
            }
        }return false;
    }

    private void Destroy_Object(int i)
    {
        Destroy(Missions_in_UI[i]);
        Missions_in_UI.Remove(Missions_in_UI[i]);
        completed_mission = true;
        amount_of_completed_missions++;
    }
}
