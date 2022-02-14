using System.Collections.Generic;
using UnityEngine;

public class Export_Chute : MonoBehaviour, IInteractor_Connector, IIData_transfer
{
    Mission_System mission_system_object;
    // Start is called before the first frame update
    public int transport_speed = 1;
    public List<GameObject> Stuff_to_transport = new List<GameObject>();
    public Object_Creation Generation_Object;
    void Awake()
    {
        mission_system_object = Find_Components.find_mission_system(); //Mission system objektet
        Generation_Object = Find_Components.find_Object_Creation();
    }

    // Update is called once per frame
    void Update()
    {
        if (Stuff_to_transport.Count > 0) //Ikke vits å sjekke vis export chuten er tom
        {
            for (int i = 0; i < Stuff_to_transport.Count; i++)
            {
                Stuff_to_transport[i].transform.position = new Vector3(Stuff_to_transport[i].transform.position.x + 0.05f * transport_speed * Time.deltaTime, Stuff_to_transport[i].transform.position.y);
                //Denne flytter alle objekter til høyre med en fast hastighet i sekundet. sett transport speed til noe annet for å øke farten
                if (Stuff_to_transport[i].transform.position.x > 7.2f) //Vis objektet er utenfor hardcoda posisjon, sjekk om det oppfyller krav fra missions vi har
                {
                    if (mission_system_object.check_mission_success(Stuff_to_transport[i])) //Returnerer True vis objektet oppfyller ett krav til ett mission
                    {
                        StaticData.score += Stuff_to_transport[i].GetComponent<Sword>().value; //Gi score
                        Destroy(Stuff_to_transport[i]); //Slett objekt fra spillet 
                        Stuff_to_transport.Remove(Stuff_to_transport[i]); //Slett objekt fra listen
                    }
                }
            }
        }
    }

    public void Pickup(GameObject main_character)
    {
        //Do nothing
    }

    public void Drop_Off(GameObject main_character) //Interface metode for å legge fra seg ting
    {
        main_character.GetComponent<DwarfScript>().Item_in_inventory.transform.position = gameObject.transform.position;
        Stuff_to_transport.Add(main_character.GetComponent<DwarfScript>().Item_in_inventory);
        Return_Answer(main_character, false);
    }

    public void Return_Answer(GameObject main_character, bool result) //Interface metode for å returnere ett svar. 
    {
        main_character.SendMessage("Inventory_Full_Message", result);
    }

    public void Storage()
    {
        for (int i = 0; i < Stuff_to_transport.Count; i++)
        {
            StaticData.export_chute_object_static.Add(Stuff_to_transport[i].GetComponent<Sword>().object_tag);
            StaticData.export_chute_quality_static.Add(Stuff_to_transport[i].GetComponent<Sword>().ore_quality);
            StaticData.x_position_export_chute.Add(Stuff_to_transport[i].transform.position.x);
            StaticData.y_position_export_chute.Add(Stuff_to_transport[i].transform.position.y);
        }
    }

    public void Loading()
    {

        for (int i = 0; i < StaticData.export_chute_quality_static.Count; i++)
        {
            Stuff_to_transport.Add(Generation_Object.create_sword((int)StaticData.export_chute_quality_static[i], new Vector3(StaticData.x_position_export_chute[i], StaticData.y_position_export_chute[i], 0)));
        }
        StaticData.export_chute_quality_static.Clear();
        StaticData.export_chute_object_static.Clear();
        StaticData.x_position_export_chute.Clear();
        StaticData.y_position_export_chute.Clear();
    }
}
