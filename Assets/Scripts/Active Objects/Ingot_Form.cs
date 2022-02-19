using System.Collections.Generic;
using UnityEngine;

public class Ingot_Form : MonoBehaviour, IInteractor_Connector
{
    public List<GameObject> Ingots_in_form = new List<GameObject>();
    public Furnace furnace_object;
    public void Drop_Off(GameObject main_character)
    {
        //Do nothing
    }

    public void Pickup(GameObject main_character) //Interface metode for å plukke opp objekter. 
    {
        if (Ingots_in_form.Count > 0)
        {
            main_character.GetComponent<DwarfScript>().Item_in_inventory = Ingots_in_form[0];
            Ingots_in_form.RemoveAt(0);
            Return_Answer(main_character, true);
        }
    }

    public void Return_Answer(GameObject main_character, bool result) //Interface metode for å returnere ett svar
    {
        main_character.SendMessage("Inventory_Full_Message", result);
    }



    // Start is called before the first frame update
    void Start()
    {
        furnace_object = Find_Components.find_furnace();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
