using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sorted_Ore_Tray : MonoBehaviour, IInteractor_Connector, IIData_transfer
{
    public int quality = 0;

    public List<GameObject> Ores_in_tray = new List<GameObject>();

    public Sprite[] spriteArray_iron;
    public Sprite[] spriteArray_copper;
    public Sprite[] spriteArray_mithril;
    bool result;

    public Sprite[] using_sprite;
    public SpriteRenderer spriteRenderer;
    public Object_Creation Generation_Object;

    // Start is called before the first frame update
    void Awake()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        Generation_Object = Find_Components.find_Object_Creation(); //Generation objektet
        Change_Sprite_Set(0);
    }

    private void Change_Sprite_Set(int quality)
    {
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
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void handleSprite()
    {
        if (Ores_in_tray.Count > 0 && Ores_in_tray.Count <= 3)//Sprite endring fra tomt til fult. 
        {
            spriteRenderer.sprite = using_sprite[1];
        }
        else if (Ores_in_tray.Count > 3 && Ores_in_tray.Count <= 6)
        {
            spriteRenderer.sprite = using_sprite[2];
        }
        else if (Ores_in_tray.Count > 6 && Ores_in_tray.Count <= 9)
        {
            spriteRenderer.sprite = using_sprite[3];
        }
        else if (Ores_in_tray.Count == 10)
        {
            spriteRenderer.sprite = using_sprite[4];
        }
        else if (Ores_in_tray.Count == 0) //Tilbake til tomt når det er tomt. 
        {
            spriteRenderer.sprite = using_sprite[0];
        }
    }

    public void Pickup(GameObject main_character)
    {
        main_character.GetComponent<DwarfScript>().Item_in_inventory = Ores_in_tray[0];
        Ores_in_tray.RemoveAt(0);
        Return_Answer(main_character, true);
        handleSprite();
    }

    public void Drop_Off(GameObject main_character)
    {
        result = handleQuality(main_character.GetComponent<DwarfScript>().Item_in_inventory.GetComponent<Ore>().ore_quality);
        if (result == true)
        {
            main_character.GetComponent<DwarfScript>().Item_in_inventory.transform.position = gameObject.transform.position;
            Ores_in_tray.Add(main_character.GetComponent<DwarfScript>().Item_in_inventory);
            Return_Answer(main_character, false);
        }
        handleSprite();
    }



    private bool handleQuality(Enumtypes.Ore_Quality ore_quality)
    {
        if (Ores_in_tray.Count == 0)
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

    public void Return_Answer(GameObject main_character, bool result)
    {
        main_character.SendMessage("Inventory_Full_Message", result);
    }

    public void Storage() //Interface metode for å lagre qualitien av objekter i storage i staticData. 
    {
        List<Enumtypes.Ore_Quality> Ore_Quality = new List<Enumtypes.Ore_Quality>();
        for (int i = 0; i < Ores_in_tray.Count; i++) //Generer ore og legg det i stockpilen for usortert ore
        {
            Ore_Quality.Add(Ores_in_tray[i].GetComponent<Ore>().ore_quality);
        }
        StaticData.Ore_Quality.Add(Ore_Quality);
    }

    public void Loading() //Her henter vi ut igjen dataen fra staticdata filene, vi leser inn en og en list, og legger den til. Så fjernes det fra staticData
    {
        if (StaticData.Ore_Quality.Count > 0)
        {
            List<Enumtypes.Ore_Quality> Ore_Quality = StaticData.Ore_Quality[0];
            StaticData.Ore_Quality.RemoveAt(0);
            for (int i = 0; i < Ore_Quality.Count; i++) //Generer ore og legg det i stockpilen for usortert ore
            {
                
                handleQuality(Ore_Quality[i]);
                Ores_in_tray.Add(Generation_Object.create_ore((int)Ore_Quality[i], gameObject));
            }
            handleSprite();
        }
    }
}
