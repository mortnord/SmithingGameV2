using System.Collections.Generic;
using UnityEngine;
public class Export_Chute : MonoBehaviour, IInteractor_Connector, IData_Transfer
{
    Mission_System mission_system_object;
    // Start is called before the first frame update
    public int transport_speed = 1;
    public List<GameObject> Stuff_to_transport = new List<GameObject>();
    public Object_Creation Generation_Object;
    void Awake()
    {
        mission_system_object = Find_Components.Find_Mission_System(); //Mission system objektet
        Generation_Object = Find_Components.Find_Object_Creation();
    }
    // Update is called once per frame
    void Update()
    {
        if (Stuff_to_transport.Count > 0) //Ikke vits ? sjekke vis export chuten er tom
        {
            for (int i = 0; i < Stuff_to_transport.Count; i++)
            {
                Stuff_to_transport[i].transform.position = new Vector3(Stuff_to_transport[i].transform.position.x + 0.05f * transport_speed * Time.deltaTime, Stuff_to_transport[i].transform.position.y);
                //Denne flytter alle objekter til h?yre med en fast hastighet i sekundet. sett transport speed til noe annet for ? ?ke farten
                if (Stuff_to_transport[i].transform.position.x > 7.2f) //Vis objektet er utenfor hardcoda posisjon, sjekk om det oppfyller krav fra missions vi har
                {
                    if (mission_system_object.Check_Mission_Success(Stuff_to_transport[i])) //Returnerer True vis objektet oppfyller ett krav til ett mission
                    {
                        StaticData.score += Stuff_to_transport[i].GetComponent<Sword>().Get_Value(); //Gi score
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
    public void Drop_Off(GameObject main_character) //Interface metode for ? legge fra seg ting
    {
        main_character.GetComponent<MainCharacterStateManager>().Get_Item_In_Inventory().transform.position = gameObject.transform.position;
        Stuff_to_transport.Add(main_character.GetComponent<MainCharacterStateManager>().Get_Item_In_Inventory());
        Return_Answer(main_character, false);
    }
    public void Return_Answer(GameObject main_character, bool result) //Interface metode for ? returnere ett svar. 
    {
        main_character.SendMessage("Inventory_Full_Message", result);
    }
    public void Storage() //Lagring og lasting av objekter p? export-chuten. 
    {
        for (int i = 0; i < Stuff_to_transport.Count; i++) //Sjekker alle objekter i lista
        {
            StaticData.export_chute_object_static.Add(Stuff_to_transport[i].GetComponent<Common_Properties>().Get_Object_Tag()); //Lagrer hva type objekt det er
            StaticData.export_chute_quality_static.Add(Stuff_to_transport[i].GetComponent<Common_Properties>().Get_Ore_Quality()); //Lagrer hva kvaliteten er
            StaticData.x_position_export_chute.Add(Stuff_to_transport[i].transform.position.x); //x-posisjon
            StaticData.y_position_export_chute.Add(Stuff_to_transport[i].transform.position.y); //y-posisjon
        }
    }
    public void Loading() // her laster vi inn data, for ? gjenskape situasjonen
    {
        for (int i = 0; i < StaticData.export_chute_quality_static.Count; i++) //Vi gj?r det for alle objekter i lista, antar at det er samme mengde data i alle 4 listene
                                                                               //S? lager vi ett antall sverd objekt basert p? dataen vi har, s? clearer vi alt.
        {
            Stuff_to_transport.Add(Generation_Object.Create_Sword((int)StaticData.export_chute_quality_static[i], new Vector3(StaticData.x_position_export_chute[i], StaticData.y_position_export_chute[i], 0)));
        }
        StaticData.export_chute_quality_static.Clear();
        StaticData.export_chute_object_static.Clear();
        StaticData.x_position_export_chute.Clear();
        StaticData.y_position_export_chute.Clear();
    }
}
