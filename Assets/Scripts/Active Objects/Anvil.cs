using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anvil : MonoBehaviour
{

    Object_Creation Generation_Object;
    public GameObject Converted_Object;
    public GameObject object_to_be_destroyed;
    public GameObject blueprint_copy;
    public int ingot_quality;
    public bool convert = false;
    // Start is called before the first frame update
    void Start()
    {
        Generation_Object = Find_Components.find_Object_Creation();
    }

    // Update is called once per frame
    void Update()
    {
        if(convert && blueprint_copy != null)
        {
            if(blueprint_copy.GetComponent<Blueprint_Sword>())
            {
                ingot_quality = Converted_Object.GetComponent<Ingot>().quality;
                object_to_be_destroyed = Converted_Object;
                Converted_Object = Generation_Object.create_sword(ingot_quality, transform.position);
                Destroy(object_to_be_destroyed);
                convert = false;
            }
           
        }
        else
        {
            convert = false;
        }
        
    }
}
