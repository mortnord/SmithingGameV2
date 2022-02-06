using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingot_Form : MonoBehaviour, IInteractor_Connector
{
    public List<GameObject> Ingots_in_form = new List<GameObject>();

    public void Drop_Off(GameObject main_character)
    {
        //Do nothing
    }

    public void Pickup(GameObject main_character)
    {
        main_character.GetComponent<DwarfScript>().Item_in_inventory = Ingots_in_form[0];
        Ingots_in_form.RemoveAt(0);
        Return_Answer(main_character, true);
    }

    public void Return_Answer(GameObject main_character, bool result)
    {
        main_character.SendMessage("Inventory_Full_Message", result);
    }

    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
