using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trashcan : MonoBehaviour, IInteractor_Connector
{
    public GameObject Destroyable_Object = null;

    public void Drop_Off(GameObject main_character) //Interface metode for � legge fra seg ting. 
    {
        main_character.GetComponent<DwarfScript>().Item_in_inventory.transform.position = gameObject.transform.position;
        Destroyable_Object = main_character.GetComponent<DwarfScript>().Item_in_inventory;
        Return_Answer(main_character, false);
    }

    public void Pickup(GameObject main_character)
    {
        //Do nothing
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
        if(Destroyable_Object != null) //vis objekt i inventory, slett.
        {
            Destroy(Destroyable_Object);
            Destroyable_Object = null;
        }
    }
}
