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
    public GameObject create_ore()//Her er koden vi bruker for å generate ore og ingots, posisjonsdataen for vectoren må endres engang i framtiden
    {
        GameObject spawned_ore = Instantiate(Ore_prefab, new Vector3(UnityEngine.Random.Range(-9.2f, - 8.2f), UnityEngine.Random.Range(-0.3f, -2f), 0), Quaternion.identity);
        spawned_ore.GetComponent<Ore>().quality = get_random_quality(); //Vi bruker en instantitate av en prefab som er satt i inspektoren.
        return spawned_ore;
    }
    public GameObject create_ingot(int quality)
    {
        GameObject spawned_ingot = Instantiate(ingot_prefab, new Vector3(-6, 0.6f), Quaternion.identity);
        spawned_ingot.GetComponent<Ingot>().quality = quality;
        return spawned_ingot;
    }

    public GameObject create_sword(int quality, Vector3 position)
    {
        GameObject spawned_sword = Instantiate(sword_prefab, position, Quaternion.identity);
        spawned_sword.GetComponent<Sword>().quality = quality;
        return spawned_sword;
    }
    public GameObject create_blueprint_sword(Vector3 position)
    {
        GameObject spawned_sword_blueprint = Instantiate(blueprint_sword, position, Quaternion.identity);
        spawned_sword_blueprint.GetComponent<Blueprint_Sword>().sprite_nr = 1;
        spawned_sword_blueprint.GetComponent<SpriteRenderer>().sortingLayerName = "Items";
        spawned_sword_blueprint.GetComponent<SpriteRenderer>().sortingOrder = -1;
        return spawned_sword_blueprint;
    }
    
    public GameObject create_card_with_mission(float time, Vector3 position)
    {
        GameObject created_card_with_mission = Instantiate(Orders_card, position, Quaternion.identity);
        Mission mission = created_card_with_mission.AddComponent<Mission>();
        created_card_with_mission.transform.position = Mission_System.transform.position;
        created_card_with_mission.transform.parent = Mission_System.transform;
        mission.setTime(time);
        GameObject temp_sword = create_sword(get_random_quality(), new Vector3(0,0,-1));
        temp_sword.transform.parent = created_card_with_mission.transform;
        temp_sword.transform.position = created_card_with_mission.transform.position;
        temp_sword.GetComponent<SpriteRenderer>().sortingOrder = 2;
        temp_sword.GetComponent<SpriteRenderer>().sortingLayerName = "UI";
        
        
        return created_card_with_mission;
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
    /*
    public int get_random_from_enum()
    {
        Type type = typeof();
        Array values = type.GetEnumValues();
        int index = rand.Next(values.Length);
        return index;
    }*/
}
