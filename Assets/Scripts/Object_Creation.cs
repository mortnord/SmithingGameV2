using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object_Creation : MonoBehaviour
{
    
    public GameObject Ore_prefab;
    public GameObject ingot_prefab;
    public GameObject create_ore(int quality)
    {
        GameObject spawned_ore = Instantiate(Ore_prefab, new Vector3(Random.Range(-9.2f, - 8.2f), Random.Range(-0.3f, -2f), 0), Quaternion.identity);
        spawned_ore.GetComponent<Ore>().quality = quality;
        return spawned_ore;
    }
    public GameObject create_ingot(int quality)
    {
        GameObject spawned_ingot = Instantiate(ingot_prefab, new Vector3(-6, 0.6f), Quaternion.identity);
        spawned_ingot.GetComponent<Ingot>().quality = quality;
        return spawned_ingot;
    }
}
