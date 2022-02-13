using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using static Enumtypes;

public class Object_Creation : MonoBehaviour
{
    
    public GameObject Ore_prefab; //Åpner for å sette prefaben i inspektoren, noe jeg vil endre i framtiden til script istedenfor inspector jobb. 
    public GameObject ingot_prefab;
    public GameObject sword_prefab;
    public GameObject Orders_card;
    public GameObject Mission_System;
    public GameObject blueprint_sword;
    public Sprite test_sprite;

    System.Random rand = new System.Random();
   
    public GameObject create_ore(int quality) //Oren som blir generert i usorta ore plassen. 
    {
        GameObject spawned_ore = Instantiate(Ore_prefab, new Vector3(UnityEngine.Random.Range(-9.2f, -8.2f), UnityEngine.Random.Range(-0.3f, -2f), 0), Quaternion.identity);
        spawned_ore.GetComponent<Ore>().quality = quality; //Vi bruker en instantitate av en prefab som er satt i inspektoren.
        return spawned_ore;
    }
    public GameObject create_ore(int quality, GameObject parent) //Oren som blir generert basert på parent etter å ha blitt lagra. 
    {
        GameObject spawned_ore = Instantiate(Ore_prefab, new Vector3(parent.transform.position.x, parent.transform.position.y, 0), Quaternion.identity);
        spawned_ore.GetComponent<Ore>().quality = quality; //Vi bruker en instantitate av en prefab som er satt i inspektoren.
        return spawned_ore;
    }
    public GameObject create_ore(float x, float y, int quality) //Oren som blir generert basert på X og Y posisjon, i dette tilfelle i random generation
    {
        x = x + 0.5f;
        y = y + 0.5f;
        GameObject spawned_ore = Instantiate(Ore_prefab, new Vector3(x, y, 0), Quaternion.identity);
        spawned_ore.GetComponent<Ore>().quality = quality; //Vi bruker en instantitate av en prefab som er satt i inspektoren.
        spawned_ore.tag = "Mining_Ore";
        return spawned_ore;
        
    }
    public GameObject create_ingot(int quality) //Ingots laga etter smelting
    {
        GameObject spawned_ingot = Instantiate(ingot_prefab, new Vector3(-4.8f, 2f), Quaternion.identity);
        spawned_ingot.GetComponent<Ingot>().quality = quality;
        return spawned_ingot;
    }
    public GameObject create_ingot(int quality, GameObject gameObject) //Ingots laget etter lasting av filer, lages i stockpiles. 
    {
        GameObject spawned_ingot = Instantiate(ingot_prefab, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y), Quaternion.identity);
        spawned_ingot.GetComponent<Ingot>().quality = quality;
        return spawned_ingot;
    }

    public GameObject create_sword(int quality, Vector3 position) //Sverd laget basert på tidligere posisjon av objektet, i dette tilfellet på ambolten
    {
        GameObject spawned_sword = Instantiate(sword_prefab, position, Quaternion.identity);
        spawned_sword.GetComponent<Sword>().quality = quality;
        return spawned_sword;
    }
    public GameObject create_blueprint_sword(Vector3 position) //Denne creater ett sverd Blueprint kopi, og setter sorting orderen slik at den havner under andre objekter
    {
        GameObject spawned_sword_blueprint = Instantiate(blueprint_sword, position, Quaternion.identity);
        spawned_sword_blueprint.GetComponent<Blueprint_Sword>().sprite_nr = 1;
        spawned_sword_blueprint.GetComponent<SpriteRenderer>().sortingLayerName = "Items";
        spawned_sword_blueprint.GetComponent<SpriteRenderer>().sortingOrder = -1;
        return spawned_sword_blueprint;
    }
    
    public GameObject create_card_with_mission(float time, Vector3 position) //Oh boy, en bestilling om mission kommer inn fra Mission_systemet
    {
        GameObject created_card_with_mission = Instantiate(Orders_card, position, Quaternion.identity); //Lager ordre card her 
        Mission mission = created_card_with_mission.AddComponent<Mission>(); //Legger til missionet vi lager i Mission_System, med blank verdier
        created_card_with_mission.transform.position = Mission_System.transform.position; //Posisjon og parents til mission-cardet
        created_card_with_mission.transform.parent = Mission_System.transform;
        mission.setTime(time); //Rewrite
        GameObject temp_sword = create_sword(get_random_quality(), new Vector3(0,0,-1)); //Dette er figuren som er på selve mission cardet, 
        temp_sword.transform.parent = created_card_with_mission.transform; //Vi setter posisjon og parent, slik at figuren på mission cardet følger etter
        temp_sword.transform.position = created_card_with_mission.transform.position;
        temp_sword.GetComponent<SpriteRenderer>().sortingOrder = 2; //Posisjoner i drawing
        temp_sword.GetComponent<SpriteRenderer>().sortingLayerName = "UI";
        
        
        return created_card_with_mission;
    }
    public GameObject recreate_mission(float time_remaining, int quality_object, float x_position, float y_position) //Her bestiller vi ett mission med satte 
                                                                                                                    //parametre, slik at vi får tilbake tidligere missions
    {
        GameObject recreated_mission = Instantiate(Orders_card, new Vector3(x_position, y_position, 0), Quaternion.identity);
        Mission mission = recreated_mission.AddComponent<Mission>();
        recreated_mission.transform.parent = Mission_System.transform;
        mission.setTime(time_remaining);
        GameObject temp_sword = create_sword(quality_object, new Vector3(0, 0, -1));
        temp_sword.transform.parent = recreated_mission.transform;
        temp_sword.transform.position = recreated_mission.transform.position;
        temp_sword.GetComponent<SpriteRenderer>().sortingOrder = 2; //Posisjoner i drawing
        temp_sword.GetComponent<SpriteRenderer>().sortingLayerName = "UI";

        return recreated_mission;

    }


    public int get_random_quality()
    {
        Type type = typeof(Ore_Quality);
        Array values = type.GetEnumValues();
        int index = rand.Next(values.Length);
        return index;
    }
    public int get_random_type()
    {
        Type type = typeof(Mission_Objects);
        Array values = type.GetEnumValues();
        int index = rand.Next(values.Length);
        return index;
    }
}
