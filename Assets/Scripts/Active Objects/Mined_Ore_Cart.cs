using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mined_Ore_Cart : MonoBehaviour, IInteractor_Connector, IInteract_Work
{

    public List<GameObject> Ores_in_tray = new List<GameObject>();

    public void Drop_Off(GameObject main_character)
    {
        main_character.GetComponent<DwarfScript>().Item_in_inventory.transform.position = gameObject.transform.position;
        main_character.GetComponent<DwarfScript>().Item_in_inventory.tag = "Object";
        Ores_in_tray.Add(main_character.GetComponent<DwarfScript>().Item_in_inventory);
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

    public void Work(GameObject main_character)
    {
        StaticData.Transition_Ores.Clear();
        for (int i = Ores_in_tray.Count; i > 0; i--) //Generer ore og legg det i stockpilen for usortert ore
        {
            StaticData.Transition_Ores.Add(Ores_in_tray[i-1].GetComponent<Ore>().ore_quality);
            Destroy(Ores_in_tray[i-1]);
        }
        Ores_in_tray.Clear();
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
