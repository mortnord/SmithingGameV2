using System.Collections.Generic;
using UnityEngine;

public class Mined_Ore_Cart : MonoBehaviour, IInteractor_Connector
{

    public List<GameObject> Ores_in_tray = new List<GameObject>();

    public void Drop_Off(GameObject main_character) //Her legger vi fra oss objektet vi har plukket opp, samtidig som vi gj�r at vi ikke kan plukke det opp igjen
                                                    // med � gj�re taggen til objekt
    {
        main_character.GetComponent<DwarfScript>().Item_in_inventory.transform.position = gameObject.transform.position;
        main_character.GetComponent<DwarfScript>().Item_in_inventory.tag = "Object";
        main_character.GetComponent<DwarfScript>().Item_in_inventory.SetActive(false);
        StaticData.Transition_Ores.Add(main_character.GetComponent<DwarfScript>().Item_in_inventory.GetComponent<Ore>().ore_quality);
        Ores_in_tray.Add(main_character.GetComponent<DwarfScript>().Item_in_inventory);
        Return_Answer(main_character, false); //returnere svar 
    }

    public void Pickup(GameObject main_character)
    {
        //Do nothing
    }

    public void Return_Answer(GameObject main_character, bool result) //Interface metode for � returnere svar
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
