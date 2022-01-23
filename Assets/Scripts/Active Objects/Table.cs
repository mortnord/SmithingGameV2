using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table : MonoBehaviour
{

    Object_Creation Generation_Object;
    GameObject blueprint_original_on_table;
    public GameObject fake_copy;
    public bool copied = true;
    // Start is called before the first frame update
    void Start()
    {
        blueprint_original_on_table = transform.GetChild(0).gameObject;
        Generation_Object = Find_Components.find_Object_Creation();
    }

    // Update is called once per frame
    void Update()
    {
        if(copied == true)
        {
            if(blueprint_original_on_table.GetComponent<Blueprint_Sword>())
            {
                copied = false;
                fake_copy = Generation_Object.create_blueprint_sword(transform.position);
            }
        }
    }
}
