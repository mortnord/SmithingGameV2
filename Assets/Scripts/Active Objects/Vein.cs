using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static Enumtypes;

public class Vein : MonoBehaviour,IInteractor_Connector
{
    // Start is called before the first frame update

    Object_Creation Generation_Object;
    public Ore_Quality ore_quality;

    public Sprite[] spriteArray;
    public SpriteRenderer spriteRenderer;


    void Start()
    {
        Generation_Object = Find_Components.find_Object_Creation();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>(); //Dette er spirit-renderen, vi kan bruke denne til å bytte sprites.
        spriteRenderer.sprite = spriteArray[(int)ore_quality];
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Pickup(GameObject main_character)
    {
        main_character.GetComponent<DwarfScript>().Item_in_inventory = Generation_Object.create_ore((int)ore_quality);
        Return_Answer(main_character, true);
    }

    public void Drop_Off(GameObject main_character)
    {
        //Do nothing
    }

    public void Return_Answer(GameObject main_character, bool result)
    {
        main_character.SendMessage("Inventory_Full_Message", result);
    }
}
