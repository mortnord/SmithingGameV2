using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sorted_Ingots_Tray : MonoBehaviour, IInteractor_Connector, IIData_transfer
{
    public int quality;
    public List<GameObject> Ingots_in_tray = new List<GameObject>();

    public Sprite[] spriteArray;
    public SpriteRenderer spriteRenderer;

    public Object_Creation Generation_Object;
    bool result;

    public void Drop_Off(GameObject main_character)
    {
        result = handleQuality(main_character.GetComponent<DwarfScript>().Item_in_inventory.GetComponent<Ingot>().ore_quality);
        if (result == true)
        {
            main_character.GetComponent<DwarfScript>().Item_in_inventory.transform.position = gameObject.transform.position;
            Ingots_in_tray.Add(main_character.GetComponent<DwarfScript>().Item_in_inventory);
            Return_Answer(main_character, false);
        }
    }

    public void Pickup(GameObject main_character)
    {
        if(Ingots_in_tray.Count > 0)
        {
            main_character.GetComponent<DwarfScript>().Item_in_inventory = Ingots_in_tray[0];
            Ingots_in_tray.RemoveAt(0);
            Return_Answer(main_character, true);
        }
        
    }

    public void Return_Answer(GameObject main_character, bool result)
    {
        main_character.SendMessage("Inventory_Full_Message", result);
    }

    // Start is called before the first frame update
    void Awake()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        Generation_Object = Find_Components.find_Object_Creation(); //Generation objektet
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private bool handleQuality(Enumtypes.Ore_Quality ore_quality)
    {
        if (Ingots_in_tray.Count == 0)
        {
            quality = (int)ore_quality;
            Change_Sprite_Set(quality);
            return true;
        }
        else if ((int)ore_quality == quality)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    
    private void Change_Sprite_Set(int quality)
    {
        /*
        if (quality == 0)
        {
            using_sprite = spriteArray_copper;

        }
        else if (quality == 1)
        {
            using_sprite = spriteArray_iron;
        }

        else if (quality == 2)
        {
            using_sprite = spriteArray_mithril;
        }*/
    }
    private void handleSprite()
    {
        if (Ingots_in_tray.Count > 0 && Ingots_in_tray.Count <= 3)//Sprite endring fra tomt til fult. 
        {
            //spriteRenderer.sprite = using_sprite[1];
        }
        else if (Ingots_in_tray.Count > 3 && Ingots_in_tray.Count <= 6)
        {
            //spriteRenderer.sprite = using_sprite[2];
        }
        else if (Ingots_in_tray.Count > 6 && Ingots_in_tray.Count <= 9)
        {
            //spriteRenderer.sprite = using_sprite[3];
        }
        else if (Ingots_in_tray.Count == 10)
        {
            //spriteRenderer.sprite = Ingots_in_tray[4];
        }
        else if (Ingots_in_tray.Count == 0) //Tilbake til tomt når det er tomt. 
        {
            //spriteRenderer.sprite = Ingots_in_tray[0];
        }
    }

    public void Storage() //Interface metode for å lagre qualitien av objekter i storage i staticData. 
    {
        List<Enumtypes.Ore_Quality> Ore_Quality = new List<Enumtypes.Ore_Quality>();
        for (int i = 0; i < Ingots_in_tray.Count; i++) //Generer ore og legg det i stockpilen for usortert ore
        {
            Ore_Quality.Add(Ingots_in_tray[i].GetComponent<Ingot>().ore_quality);
        }
        StaticData.Ingot_Quality.Add(Ore_Quality);
    }

    public void Loading() //Her henter vi ut igjen dataen fra staticdata filene, vi leser inn en og en list, og legger den til. Så fjernes det fra staticData
    {
        if (StaticData.Ingot_Quality.Count > 0)
        {
            List<Enumtypes.Ore_Quality> Ore_Quality = StaticData.Ingot_Quality[0];
            StaticData.Ingot_Quality.RemoveAt(0);
            for (int i = 0; i < Ore_Quality.Count; i++) //Generer ore og legg det i stockpilen for usortert ore
            {

                handleQuality(Ore_Quality[i]);
                Ingots_in_tray.Add(Generation_Object.create_ingot((int)Ore_Quality[i], gameObject));
            }
            handleSprite();
        }
    }
}
