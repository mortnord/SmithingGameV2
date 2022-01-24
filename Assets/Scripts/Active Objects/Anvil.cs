using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anvil : MonoBehaviour
{

    Object_Creation Generation_Object;
    public GameObject Converted_Object;
    public GameObject object_to_be_destroyed;
    public GameObject blueprint_copy;
    public bool convert = false;
    // Start is called before the first frame update
    void Start()
    {
        Generation_Object = Find_Components.find_Object_Creation(); // Her finner vi generation objektet for å generate stuff fra ingots
    }

    // Update is called once per frame
    void Update()
    {
        if(convert && blueprint_copy != null) //Sjekk om vi i det hele tatt har en blueprint
        {
            if(blueprint_copy.GetComponent<Blueprint_Sword>()) //Hvis sverd blueprint.
            {
                Converted_Object = Generation_Object.create_sword(object_to_be_destroyed.GetComponent<Ingot>().quality, transform.position); // Her caller vi create sword
                                                                                                                                             // med ingoten sin quality, og anvilen sin posisjon
                Reset(); //Reset anvilen tilbake til normal og destroy ingot objektet
            }

        }
        else
        {
            convert = false; //ingen blueprint copy, så da mislykkes converteringen
        }
        
    }

    private void Reset()
    {
        Destroy(object_to_be_destroyed); //Ingoten som blir ødelagt
        convert = false;
    }
}
