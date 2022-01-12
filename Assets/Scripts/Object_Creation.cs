using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object_Creation : MonoBehaviour
{
    
    public GameObject Ore_prefab;
    public void create_ore()
    {
        Instantiate(Ore_prefab, new Vector3(Random.Range(-9.2f, - 8.2f), Random.Range(-0.3f, -2f), 0), Quaternion.identity);
    }
}
