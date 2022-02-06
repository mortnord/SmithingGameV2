using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anvil : MonoBehaviour, IInteractor_Connector, IInteract_Work
{

    Object_Creation Generation_Object;
    public GameObject Converted_Object;
    public GameObject blueprint_copy;
    public GameObject object_to_be_destroyed;
    public bool convert = false;
    // Start is called before the first frame update
    void Start()
    {
        Generation_Object = Find_Components.find_Object_Creation(); // Her finner vi generation objektet for � generate stuff fra ingots
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
            convert = false; //ingen blueprint copy, s� da mislykkes converteringen
        }
        
    }

    private void Reset()
    {
        Destroy(object_to_be_destroyed);
        convert = false;
    }

    public void Pickup(GameObject main_character)
    {
        if(Converted_Object != null)
        {
            main_character.GetComponent<DwarfScript>().Item_in_inventory = Converted_Object;
            Converted_Object = null;
        }
        else
        {
            main_character.GetComponent<DwarfScript>().Item_in_inventory = blueprint_copy;
            blueprint_copy = null;
        }
        Return_Answer(main_character, true);
    }

    public void Drop_Off(GameObject main_character)
    {
        if (main_character.GetComponent<DwarfScript>().Item_in_inventory.GetComponent<Ingot>() != null) //Her legger vi inn ingots i anvilen
        {
            object_to_be_destroyed = main_character.GetComponent<DwarfScript>().Item_in_inventory;
            main_character.GetComponent<DwarfScript>().Item_in_inventory.transform.position = gameObject.transform.position;
            Return_Answer(main_character, false);
        }
        else if (main_character.GetComponent<DwarfScript>().Item_in_inventory.GetComponent<Blueprint_Sword>() != null) //Her kommer blueprints inn i anvilen
        {
            blueprint_copy = main_character.GetComponent<DwarfScript>().Item_in_inventory;
            main_character.GetComponent<DwarfScript>().Item_in_inventory.transform.position = gameObject.transform.position;
            Return_Answer(main_character, false);
        }
    }

    public void Return_Answer(GameObject main_character, bool result)
    {
        main_character.SendMessage("Inventory_Full_Message", result);
    }

    public void Work(GameObject main_character)
    {
        convert = true;
    }
}
