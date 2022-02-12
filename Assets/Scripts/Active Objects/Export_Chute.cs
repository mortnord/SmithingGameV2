using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Export_Chute : MonoBehaviour, IInteractor_Connector
{
    Mission_System mission_system_object;
    // Start is called before the first frame update
    public int transport_speed = 1;
    public List<GameObject> Stuff_to_transport = new List<GameObject>();
    void Start()
    {
        mission_system_object = Find_Components.find_mission_system(); //Mission system objektet
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
                    if(mission_system_object.check_mission_success(Stuff_to_transport[i])) //Returnerer True vis objektet oppfyller ett krav til ett mission
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

    public void Drop_Off(GameObject main_character)
    {
        main_character.GetComponent<DwarfScript>().Item_in_inventory.transform.position = gameObject.transform.position;
        Stuff_to_transport.Add(main_character.GetComponent<DwarfScript>().Item_in_inventory);
        Return_Answer(main_character, false);
    }

    public void Return_Answer(GameObject main_character, bool result)
    {
        main_character.SendMessage("Inventory_Full_Message", result);
    }
}
