using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anvil : MonoBehaviour, IInteractor_Connector, IInteract_Work
{

    Object_Creation Generation_Object;
    public GameObject Converted_Object; //Resultat
    public GameObject blueprint_copy; 
    public GameObject object_to_be_destroyed; //Inn-objekt. 
    public bool convert = false; //Om ingoten skal converteres. 
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
    private void Reset() // Lett måte å resette anvilen på. Destroyer forrige objekt, og setter convert tilbake til false
    {
        Destroy(object_to_be_destroyed);
        convert = false;
    }

    public void Pickup(GameObject main_character) //Interface metode for å hente ut converted objekt først, så prøver den å rydde opp i blueprint copien
    {
        if (Converted_Object != null)
        {
            main_character.GetComponent<DwarfScript>().Item_in_inventory = Converted_Object;
            Converted_Object = null;
            Return_Answer(main_character, true); //Returnerer svar at vi fikk plukket oppe noe
        }
        else if (blueprint_copy != null)
        {
            main_character.GetComponent<DwarfScript>().Item_in_inventory = blueprint_copy;
            blueprint_copy = null;
            Return_Answer(main_character, true); //Returnerer svar at vi fikk plukket oppe noe
        }
        else if (object_to_be_destroyed != null)
        {
            main_character.GetComponent<DwarfScript>().Item_in_inventory = object_to_be_destroyed;
            object_to_be_destroyed = null;
            Return_Answer(main_character, true); //Returnerer svar at vi fikk plukket oppe noe
        }
    }

    public void Drop_Off(GameObject main_character) //Interface metode for å legge inn objekt. 
    {
        if (main_character.GetComponent<DwarfScript>().Item_in_inventory.GetComponent<Ingot>() != null) //Her legger vi inn ingots i anvilen
        {
            object_to_be_destroyed = main_character.GetComponent<DwarfScript>().Item_in_inventory;
            main_character.GetComponent<DwarfScript>().Item_in_inventory.transform.position = gameObject.transform.position;
            Return_Answer(main_character, false); //Returnerer svar at vi fikk lagt fra oss noe. 
        }
        else if (main_character.GetComponent<DwarfScript>().Item_in_inventory.GetComponent<Blueprint_Sword>() != null) //Her kommer blueprints inn i anvilen
        {
            blueprint_copy = main_character.GetComponent<DwarfScript>().Item_in_inventory;
            main_character.GetComponent<DwarfScript>().Item_in_inventory.transform.position = gameObject.transform.position;
            Return_Answer(main_character, false); //Returnerer svar at vi fikk lagt fra oss noe.
        }
    }

    public void Return_Answer(GameObject main_character, bool result) //Interface metode for å returnere svar. 
    {
        main_character.SendMessage("Inventory_Full_Message", result);
    }

    public void Work(GameObject main_character) //Interface metode for å gjøre arbeid, i dette tilfelle konvertere ett objekt. 
    {
        convert = true;
    }
}
