using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sorted_Ingots_Tray : MonoBehaviour, IInteractor_Connector
{
    public int quality;
    public List<GameObject> Ingots_in_tray = new List<GameObject>();

    public Sprite[] spriteArray;
    public SpriteRenderer spriteRenderer;

    public void Drop_Off(GameObject main_character)
    {
        //result = handleQuality(main_character.GetComponent<DwarfScript>().Item_in_inventory.GetComponent<Ore>().ore_quality);
        //if (result == true)
        {
            main_character.GetComponent<DwarfScript>().Item_in_inventory.transform.position = gameObject.transform.position;
            Ingots_in_tray.Add(main_character.GetComponent<DwarfScript>().Item_in_inventory);
            Return_Answer(main_character, false);
        }
    }

    public void Pickup(GameObject main_character)
    {
        main_character.GetComponent<DwarfScript>().Item_in_inventory = Ingots_in_tray[0];
        Ingots_in_tray.RemoveAt(0);
        Return_Answer(main_character, true);
    }

    public void Return_Answer(GameObject main_character, bool result)
    {
        main_character.SendMessage("Inventory_Full_Message", result);
    }

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
