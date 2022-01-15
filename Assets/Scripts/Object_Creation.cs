using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object_Creation : MonoBehaviour
{
    
    public GameObject Ore_prefab; //Åpner for å sette prefaben i inspektoren, noe jeg vil endre i framtiden til script istedenfor inspector jobb. 
    public GameObject ingot_prefab;
    public GameObject create_ore(int quality) //Her er koden vi bruker for å generate ore og ingots, posisjonsdataen for vectoren må endres engang i framtiden
    {
        GameObject spawned_ore = Instantiate(Ore_prefab, new Vector3(Random.Range(-9.2f, - 8.2f), Random.Range(-0.3f, -2f), 0), Quaternion.identity);
        spawned_ore.GetComponent<Ore>().quality = quality; //Vi bruker en instantitate av en prefab som er satt i inspektoren.
        return spawned_ore;
    }
    public GameObject create_ingot(int quality)
    {
        GameObject spawned_ingot = Instantiate(ingot_prefab, new Vector3(-6, 0.6f), Quaternion.identity);
        spawned_ingot.GetComponent<Ingot>().quality = quality;
        return spawned_ingot;
    }
}
