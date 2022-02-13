using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table : MonoBehaviour, IInteractor_Connector
{

    Object_Creation Generation_Object;
    GameObject blueprint_original_on_table;
    public GameObject fake_copy;
    public bool copied = true;
    // Start is called before the first frame update
    void Start()
    {
        blueprint_original_on_table = transform.GetChild(0).gameObject; //Finn child-objektet, vi setter det i editoren
        Generation_Object = Find_Components.find_Object_Creation(); //Generation objektet
    }

    // Update is called once per frame
    void Update()
    {
        if(copied == true) //Denne brukes for å lage en ny kopi med engang vi har fjernet den første kopien, da har vi alltid en kopi liggende som vi kan plukke opp
        {                  //Kopien ligger under orginalen, så spiller vil ikke se den. 
            if(blueprint_original_on_table.GetComponent<Blueprint_Sword>())
            {
                copied = false;
                fake_copy = Generation_Object.create_blueprint_sword(transform.position); //Posisjonen til kopien er lik posisjonen til bordet. 
            }
        }
    }

    public void Pickup(GameObject main_character) //Interface metode for å hente opp blueprint copies. 
    {

        main_character.GetComponent<DwarfScript>().Item_in_inventory = fake_copy;
        fake_copy = null;
        main_character.GetComponent<DwarfScript>().Item_in_inventory.transform.parent = null;
        copied = true;
        Return_Answer(main_character, true);
    }
    public void Drop_Off(GameObject main_character)
    {
        //Do nothing
    }

    public void Return_Answer(GameObject main_character, bool result) //Returnerer svar til dwarf character
    {
        main_character.SendMessage("Inventory_Full_Message", result);
    }
}
