using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using static Enumtypes;

public class Object_Creation : MonoBehaviour
{
    
    public GameObject Ore_prefab; //Åpner for å sette prefaben i inspektoren, noe jeg vil endre i framtiden til script istedenfor inspector jobb. 
    public GameObject ingot_prefab;
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
    public int get_random_quality()
    {
        System.Random rand = new System.Random();
        Type type = typeof(Ore_Quality);
        Array values = type.GetEnumValues();
        int index = rand.Next(values.Length);
        return index;
        

    }
}
