using System.Collections.Generic;
using UnityEngine;
public class Mission_System : MonoBehaviour, IData_Transfer
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
        Timer_Object = Find_Components.Find_Timer_Object();
        Generation_Object = Find_Components.Find_Object_Creation();
        Loading(); 
    }
    // Update is called once per frame
    void Update()
    {
        if (Timer_Object.Get_Reset() == true && completed_mission == true) //Vis man har brukt mer enn 10 sekund, men mindre en maks tid mellom missions
                                                                     //, og har gjort ett mission får du ett nytt
        {
            Create_Mission(); //Lag ny mission
            Timer_Object.Set_Reset(false); //Reset
            completed_mission = false;
        }
        else if (Timer_Object.Get_Ekstra_Reset() == true) //Vis du bruker maks tid mellom missions, får du ett nytt, ala scaling difficulty
        {
            Create_Mission(); //Lag nytt mission 
            Timer_Object.Set_Ekstra_Mission_Spawn(30 - amount_of_completed_missions); //Øk difficulty
            if (Timer_Object.Get_Ekstra_Mission_Spawn() < 10) //Ikke for vansklig da
            {
                Timer_Object.Set_Ekstra_Mission_Spawn(10);
            }
            Timer_Object.Set_Reset(false); //Reset
            Timer_Object.Set_Ekstra_Reset(false);
            completed_mission = false;
        }
    }
    void Create_Mission() //Her lager mission systemet selve missionet
    {
        time_remaining = StaticData.time_Remaining; //Vi finner tid igjen, men ska vel rewrites?
        GameObject mission = Generation_Object.Create_Card_With_Mission(time_remaining, new Vector3(0, 0, 0)); //Missionet blir laget i create_objekts koden
        Missions_in_UI.Add(mission); //Legges inn i objekter som skal tegnes i UIet
        mission.GetComponent<Mission>().Set_Quality_Of_Object_For_Mission(mission.GetComponentInChildren<Common_Properties>().Get_Quality()); //Kvaliteten på objektet som skal leveres
        mission.GetComponent<Mission>().Set_Type_Of_Object_For_Mission((int)Generation_Object.Get_Random_Type()); //typen av objekt som ska leveres
        Timer_Object.Set_Reset_Timer(10); //Reset
    }
    public bool Check_Mission_Success(GameObject Object_to_check) //Her sjekker vi om ett levert objekt vil fullføre missionet
    {
        if (Missions_in_UI.Count > 0) // Kun vis vi har aktive missions
        {
            for (int i = 0; i < Missions_in_UI.Count; i++)
            {
                if (Object_to_check.GetComponent<Common_Properties>()) //Sjekk sword missions
                {
                    //Merga IF-setning som sjekker at begge delen av missionet er oppfylt
                    if (Missions_in_UI[i].GetComponent<Mission>().Get_Quality_Of_Object_For_Mission() == Object_to_check.GetComponent<Common_Properties>().Get_Quality() && Missions_in_UI[i].GetComponent<Mission>().Get_Type_Of_Object_For_Mission() == (int)Object_to_check.GetComponent<Common_Properties>().Get_Mission_Tag())
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
                StaticData.quality_of_object_for_mission_static_data.Add(Missions_in_UI[i].GetComponent<Mission>().Get_Quality_Of_Object_For_Mission());
                StaticData.Time_remaining_static_data.Add(Missions_in_UI[i].GetComponent<Mission>().Get_Time_Remaining());
                StaticData.type_of_object_for_mission_static_data.Add(Missions_in_UI[i].GetComponent<Mission>().Get_Type_Of_Object_For_Mission());
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
                GameObject mission = Generation_Object.Recreate_Mission(StaticData.Time_remaining_static_data[i], StaticData.quality_of_object_for_mission_static_data[i], StaticData.x_position_mission[i], StaticData.y_position_mission[i]); //Missionet blir laget i create_objekts koden
                Missions_in_UI.Add(mission); //Legges inn i objekter som skal tegnes i UIet
                mission.GetComponent<Mission>().Set_Quality_Of_Object_For_Mission(StaticData.quality_of_object_for_mission_static_data[i]); //Kvaliteten på objektet som skal leveres
                mission.GetComponent<Mission>().Set_Type_Of_Object_For_Mission(StaticData.type_of_object_for_mission_static_data[i]); //typen av objekt som ska leveres
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
