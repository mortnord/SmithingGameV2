using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using static Enumtypes;

public class Object_Creation : MonoBehaviour
{
    
    public GameObject Ore_prefab; //�pner for � sette prefaben i inspektoren, noe jeg vil endre i framtiden til script istedenfor inspector jobb. 
    public GameObject ingot_prefab;
    public GameObject sword_prefab;
    public GameObject Orders_card;

    System.Random rand = new System.Random();
    public GameObject create_ore()//Her er koden vi bruker for � generate ore og ingots, posisjonsdataen for vectoren m� endres engang i framtiden
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
    
    public GameObject create_card_with_mission(float time, Vector3 position)
    {
        GameObject created_card_with_mission = Instantiate(Orders_card, position, Quaternion.identity);
        Mission mission = created_card_with_mission.AddComponent<Mission>();
        mission.setTime(time);
        
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
        Type type = typeof(Object_Types);
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
