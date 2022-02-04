using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interacter_script : MonoBehaviour, IInteractor_Connector
{

    public void Drop_Off(GameObject main_character, GameObject item_to_deposit)
    {
        throw new System.NotImplementedException();
    }

    public void Pickup(GameObject main_character)
    {
        
        //main_character.GetComponent<DwarfScript>().Item_in_inventory;
    }
}
/*
nsorted_Tray_Object = Find_Components.find_Unsorted_Tray();  // Her finner vi gameObjektet sin script-component, som vi prøver å loote
Item_in_inventory = Unsorted_Tray_Object.Ores_in_tray.ElementAt(0); //Her setter vi inventory til character til lik element 0 (alså første i arrayen)
Unsorted_Tray_Object.Ores_in_tray.RemoveAt(0);*/