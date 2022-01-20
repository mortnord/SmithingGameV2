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
        Missions_in_UI.Add(mission);
        mission.GetComponent<Mission>().quality_of_object_for_mission = mission.GetComponentInChildren<Sword>().quality;
        mission.GetComponent<Mission>().type_of_object_for_mission = (int)Generation_Object.get_random_type();
        print("added");
    }
    public void check_mission_success(GameObject Object_to_check)
    {
        print("1");
        if (Missions_in_UI.Count > 0)
        {
            print("2");
            for (int i = 0; i < Missions_in_UI.Count; i++)
            {
                print("3");
                if (Object_to_check.GetComponent<Sword>())
                {
                    print("4");
                    if (Missions_in_UI[i].GetComponent<Mission>().quality_of_object_for_mission == Object_to_check.GetComponent<Sword>().quality)
                    {
                        print("5");
                        if (Missions_in_UI[i].GetComponent<Mission>().type_of_object_for_mission == (int)Object_to_check.GetComponent<Sword>().mission_tag)
                        {
                            print("Time to kill");
                            Destroy(Missions_in_UI[i]);
                            Missions_in_UI.Remove(Missions_in_UI[i]);
                            created_mission = true;
                        }
                    }
                }
            }
        }
    }
}
