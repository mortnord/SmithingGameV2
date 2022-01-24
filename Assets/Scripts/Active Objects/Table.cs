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
}
