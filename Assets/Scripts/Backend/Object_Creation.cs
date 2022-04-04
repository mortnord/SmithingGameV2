using System;
using UnityEngine;
using static Enumtypes;
public class Object_Creation : MonoBehaviour
{
    public GameObject Ore_prefab; //Åpner for å sette prefaben i inspektoren, noe jeg vil endre i framtiden til script istedenfor inspector jobb. 
    public GameObject ingot_prefab;
    public GameObject sword_prefab;
    public GameObject Orders_card;
    public GameObject Mission_System;
    public GameObject blueprint_sword;
    public GameObject energy_object;
    public GameObject beer_object;
    public GameObject track_object;
    public Sprite test_sprite;
    System.Random rand = new System.Random();
    public GameObject Create_Ore(int quality, GameObject parent, int percent_ore_quality) //Oren som blir generert basert på parent etter å ha blitt lagra. 
    {
        GameObject spawned_ore = Instantiate(Ore_prefab, new Vector3(parent.transform.position.x, parent.transform.position.y, 0), Quaternion.identity);
        spawned_ore.GetComponent<Ore>().Set_Percent_Ore_To_Ingot(percent_ore_quality);
        spawned_ore.GetComponent<Ore>().Set_Quality(quality); //Vi bruker en instantitate av en prefab som er satt i inspektoren.
        spawned_ore.GetComponent<Common_Properties>().Set_Quality(quality);
        spawned_ore.GetComponent<Common_Properties>().Set_Object_Tag(Enumtypes.Object_Types.Ore);
        return spawned_ore;
    }
    public GameObject Create_Ingot(int quality) //Ingots laga etter smelting
    {
        GameObject spawned_ingot = Instantiate(ingot_prefab, new Vector3(-4.8f, 2f), Quaternion.identity);
        spawned_ingot.SetActive(true);
        spawned_ingot.GetComponent<Ingot>().Set_Quality(quality);
        spawned_ingot.GetComponent<Common_Properties>().Set_Quality(quality);
        spawned_ingot.GetComponent<Common_Properties>().Set_Object_Tag(Enumtypes.Object_Types.Ingot);
        return spawned_ingot;
    }
    public GameObject Create_Ingot(int quality, GameObject gameObject) //Ingots laget etter lasting av filer, lages i stockpiles. 
    {
        GameObject spawned_ingot = Instantiate(ingot_prefab, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y), Quaternion.identity);
        spawned_ingot.GetComponent<Ingot>().Set_Quality(quality);
        spawned_ingot.GetComponent<Common_Properties>().Set_Quality(quality);
        spawned_ingot.GetComponent<Common_Properties>().Set_Object_Tag(Enumtypes.Object_Types.Ingot);
        return spawned_ingot;
    }
    public GameObject Create_Sword(int quality, Vector3 position) //Sverd laget basert på tidligere posisjon av objektet, i dette tilfellet på ambolten
    {
        GameObject spawned_sword = Instantiate(sword_prefab, position, Quaternion.identity);
        spawned_sword.GetComponent<Sword>().Set_Quality(quality);
        spawned_sword.GetComponent<Common_Properties>().Set_Quality(quality);
        spawned_sword.GetComponent<Common_Properties>().Set_Object_Tag(Enumtypes.Object_Types.Sword);
        spawned_sword.GetComponent<Common_Properties>().Set_Mission_Tag(Enumtypes.Mission_Objects.Sword);
        return spawned_sword;
    }
    //Rework eller gjør noe med..
    public GameObject Create_Blueprint_Sword(Vector3 position) //Denne creater ett sverd Blueprint kopi, og setter sorting orderen slik at den havner under andre objekter
    {
        GameObject spawned_sword_blueprint = Instantiate(blueprint_sword, position, Quaternion.identity);
        spawned_sword_blueprint.GetComponent<Blueprint_Sword>().Set_Sprite_Nr(1);
        spawned_sword_blueprint.GetComponent<SpriteRenderer>().sortingLayerName = "Items";
        spawned_sword_blueprint.GetComponent<SpriteRenderer>().sortingOrder = -1;
        return spawned_sword_blueprint;
    }
    public GameObject Create_Card_With_Mission(float time, Vector3 position) //Oh boy, en bestilling om mission kommer inn fra Mission_systemet
    {
        GameObject created_card_with_mission = Instantiate(Orders_card, position, Quaternion.identity); //Lager ordre card her 
        Mission mission = created_card_with_mission.AddComponent<Mission>(); //Legger til missionet vi lager i Mission_System, med blank verdier
        created_card_with_mission.transform.position = Mission_System.transform.position; //Posisjon og parents til mission-cardet
        created_card_with_mission.transform.parent = Mission_System.transform;
        mission.Set_Time(time); //Rewrite
        GameObject temp_sword = Create_Sword(Get_Random_Quality(), new Vector3(0, 0, -1)); //Dette er figuren som er på selve mission cardet, 
        temp_sword.transform.parent = created_card_with_mission.transform; //Vi setter posisjon og parent, slik at figuren på mission cardet følger etter
        temp_sword.transform.position = created_card_with_mission.transform.position;
        temp_sword.GetComponent<SpriteRenderer>().sortingOrder = 2; //Posisjoner i drawing
        temp_sword.GetComponent<SpriteRenderer>().sortingLayerName = "UI";
        return created_card_with_mission;
    }
    public GameObject Recreate_Mission(float time_remaining, int quality_object, float x_position, float y_position) //Her bestiller vi ett mission med satte 
                                                                                                                     //parametre, slik at vi får tilbake tidligere missions
    {
        GameObject recreated_mission = Instantiate(Orders_card, new Vector3(x_position, y_position, 0), Quaternion.identity);
        Mission mission = recreated_mission.AddComponent<Mission>();
        recreated_mission.transform.parent = Mission_System.transform;
        mission.Set_Time(time_remaining);
        GameObject temp_sword = Create_Sword(quality_object, new Vector3(0, 0, -1));
        temp_sword.transform.parent = recreated_mission.transform;
        temp_sword.transform.position = recreated_mission.transform.position;
        temp_sword.GetComponent<SpriteRenderer>().sortingOrder = 2; //Posisjoner i drawing
        temp_sword.GetComponent<SpriteRenderer>().sortingLayerName = "UI";
        return recreated_mission;
    }
    public GameObject Create_Energy_Indicator(GameObject parent)
    {
        GameObject created_Energy_object = Instantiate(energy_object, new Vector3(parent.transform.position.x, parent.transform.position.y, 0), Quaternion.identity);
        return created_Energy_object;
    }
    public GameObject Create_Beer_Object(GameObject parent)
    {
        GameObject create_beer_object = Instantiate(beer_object, new Vector3(parent.transform.position.x, parent.transform.position.y, 0), Quaternion.identity);
        return create_beer_object;
    }
    public GameObject Create_Track_Object(GameObject parent)
    {
        GameObject create_track_object = Instantiate(track_object, new Vector3(parent.transform.position.x, parent.transform.position.y, 0), Quaternion.identity);
        return create_track_object;
    }
    public int Get_Random_Quality()
    {
        Type type = typeof(Ore_Quality);
        Array values = type.GetEnumValues();
        int index = rand.Next(values.Length);
        return index;
    }
    public int Get_Random_Type()
    {
        Type type = typeof(Mission_Objects);
        Array values = type.GetEnumValues();
        int index = rand.Next(values.Length);
        return index;
    }
}
