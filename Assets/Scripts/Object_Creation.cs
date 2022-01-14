using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object_Creation : MonoBehaviour
{
    
    public GameObject Ore_prefab;
    public GameObject create_ore(int quality)
    {
        GameObject spawned_ore = Instantiate(Ore_prefab, new Vector3(Random.Range(-9.2f, - 8.2f), Random.Range(-0.3f, -2f), 0), Quaternion.identity);
        spawned_ore.GetComponent<Ore>().quality = quality;
        return spawned_ore;
    }
}
