using System.Collections.Generic;
using UnityEngine;

public class Mission_System : MonoBehaviour, IIData_transfer
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
        Loading(); 
    }
    // Update is called once per frame
    void Update()
    {
        if (Timer_Object.reset == true && completed_mission == true) //Vis man har brukt mer enn 10 sekund, men mindre en maks tid mellom missions
                                                                     //, og har gjort ett mission får du ett nytt
        {
            create_mission(); //Lag ny mission
            Timer_Object.reset = false; //Reset
            completed_mission = false;
        }
        else if (Timer_Object.ekstra_mission_reset == true) //Vis du bruker maks tid mellom missions, får du ett nytt, ala scaling difficulty
        {
            create_mission(); //Lag nytt mission 
            Timer_Object.ekstra_mission_spawn = 30 - amount_of_completed_missions; //Øk difficulty
            if (Timer_Object.ekstra_mission_spawn < 10) //Ikke for vansklig da
            {
                Timer_Object.ekstra_mission_spawn = 10;
            }
            Timer_Object.reset = false; //Reset
            Timer_Object.ekstra_mission_reset = false;
            completed_mission = false;
        }
    }
    void create_mission() //Her lager mission systemet selve missionet
    {
        time_remaining = StaticData.time_Remaining; //Vi finner tid igjen, men ska vel rewrites?
        GameObject mission = Generation_Object.create_card_with_mission(time_remaining, new Vector3(0, 0, 0)); //Missionet blir laget i create_objekts koden
        Missions_in_UI.Add(mission); //Legges inn i objekter som skal tegnes i UIet
        mission.GetComponent<Mission>().quality_of_object_for_mission = mission.GetComponentInChildren<Common_Properties>().quality; //Kvaliteten på objektet som skal leveres
        mission.GetComponent<Mission>().type_of_object_for_mission = (int)Generation_Object.get_random_type(); //typen av objekt som ska leveres
        Timer_Object.reset_timer = 10; //Reset
    }
    public bool check_mission_success(GameObject Object_to_check) //Her sjekker vi om ett levert objekt vil fullføre missionet
    {
        if (Missions_in_UI.Count > 0) // Kun vis vi har aktive missions
        {
            for (int i = 0; i < Missions_in_UI.Count; i++)
            {
                if (Object_to_check.GetComponent<Common_Properties>()) //Sjekk sword missions
                {
                    //Merga IF-setning som sjekker at begge delen av missionet er oppfylt
                    if (Missions_in_UI[i].GetComponent<Mission>().quality_of_object_for_mission == Object_to_check.GetComponent<Common_Properties>().quality && Missions_in_UI[i].GetComponent<Mission>().type_of_object_for_mission == (int)Object_to_check.GetComponent<Common_Properties>().mission_tag)
                    {
                        Destroy_Object(i); //Destroyer objekt og rydder opp

                        return true;
                    }
                }
            }
        }
        return false;
    }

    private void Destroy_Object(int i)
    {
        Destroy(Missions_in_UI[i]); //Rydder opp
        Missions_in_UI.Remove(Missions_in_UI[i]);
        completed_mission = true; //Øker difficutly
        amount_of_completed_missions++;
    }

    public void Storage() //Her tar vi den nødvendige basis dataen ut av missions, og lagrer det i staticData. Dette er kvalitet
                          //tid, og type objekt, samt X og Y posisjon. 
    {
        if (Missions_in_UI.Count > 0) // Kun vis vi har aktive missions
        {
            for (int i = 0; i < Missions_in_UI.Count; i++)
            {
                StaticData.quality_of_object_for_mission_static_data.Add(Missions_in_UI[i].GetComponent<Mission>().quality_of_object_for_mission);
                StaticData.Time_remaining_static_data.Add(Missions_in_UI[i].GetComponent<Mission>().Time_remaining);
                StaticData.type_of_object_for_mission_static_data.Add(Missions_in_UI[i].GetComponent<Mission>().type_of_object_for_mission);
                StaticData.x_position_mission.Add(Missions_in_UI[i].GetComponent<Mission>().transform.position.x);
                StaticData.y_position_mission.Add(Missions_in_UI[i].GetComponent<Mission>().transform.position.y);
            }
        }
    }

    public void Loading() //Her lager vi nye missions basert på tidligere lagret data, slik at vi får tilbake de missionsa vi hadde før vi byttet scene. 
    {
        if (StaticData.quality_of_object_for_mission_static_data.Count > 0)
        {
            for (int i = 0; i < StaticData.quality_of_object_for_mission_static_data.Count; i++)
            {
                GameObject mission = Generation_Object.recreate_mission(StaticData.Time_remaining_static_data[i], StaticData.quality_of_object_for_mission_static_data[i], StaticData.x_position_mission[i], StaticData.y_position_mission[i]); //Missionet blir laget i create_objekts koden
                Missions_in_UI.Add(mission); //Legges inn i objekter som skal tegnes i UIet
                mission.GetComponent<Mission>().quality_of_object_for_mission = StaticData.quality_of_object_for_mission_static_data[i]; //Kvaliteten på objektet som skal leveres
                mission.GetComponent<Mission>().type_of_object_for_mission = StaticData.type_of_object_for_mission_static_data[i]; //typen av objekt som ska leveres
            }
        }
        //Her rengjør vi staticData filen for evnt feil. 
        StaticData.quality_of_object_for_mission_static_data.Clear();
        StaticData.Time_remaining_static_data.Clear();
        StaticData.type_of_object_for_mission_static_data.Clear();
        StaticData.x_position_mission.Clear();
        StaticData.y_position_mission.Clear();
    }
}
