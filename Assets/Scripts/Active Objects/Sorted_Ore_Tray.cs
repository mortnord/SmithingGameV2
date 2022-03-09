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

    private void Change_Sprite_Set(int quality) //Velger hva sprite-set som skal brukes. 
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
        else if (Ores_in_tray.Count > 3 && Ores_in_tray.Count <= 6) //Vis mellom 3 og 6
        {
            spriteRenderer.sprite = using_sprite[2];
        }
        else if (Ores_in_tray.Count > 6 && Ores_in_tray.Count <= 9) //Mellom 7 og 9
        {
            spriteRenderer.sprite = using_sprite[3]; 
        }
        else if (Ores_in_tray.Count == 10) //Hvis 10
        {
            spriteRenderer.sprite = using_sprite[4];
        }
        else if (Ores_in_tray.Count == 0) //Tilbake til tomt n�r det er tomt. 
        {
            spriteRenderer.sprite = using_sprite[0];
        }
    }

    public void Pickup(GameObject main_character)
    {
        if (Ores_in_tray.Count > 0)
        {
            main_character.GetComponent<DwarfScript>().Item_in_inventory = Ores_in_tray[0];

            Ores_in_tray[0].SetActive(true);
            Ores_in_tray.RemoveAt(0);
            Return_Answer(main_character, true);
            handleSprite();
        }
    }

    public void Drop_Off(GameObject main_character) //Her legger vi fra oss objekter, f�rst sjekker vi om kvaliteten matcher det som skal v�re i stockpilen. 
                                                    //Vis kvaliteten matcher, s� legger vi itemen i stockpilen
    {
        result = handleQuality(main_character.GetComponent<DwarfScript>().Item_in_inventory.GetComponent<Common_Properties>().ore_quality);
        if (result == true)
        {

            main_character.GetComponent<DwarfScript>().Item_in_inventory.transform.position = gameObject.transform.position;
            Ores_in_tray.Add(main_character.GetComponent<DwarfScript>().Item_in_inventory);
            main_character.GetComponent<DwarfScript>().Item_in_inventory.SetActive(false);
            Return_Answer(main_character, false);
        }
        handleSprite();
    }



    private bool handleQuality(Enumtypes.Ore_Quality ore_quality)//Her sjekker vi om kvaliteten matcher
                                                                 //Hvis det er tomt, s� matcher jo alle, og kvaliteten p� stockpilen blir kvaliteten p� malmen. 
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

    public void Storage() //Interface metode for � lagre qualitien av objekter i storage i staticData. 
    {
        List<Enumtypes.Ore_Quality> Ore_Quality = new List<Enumtypes.Ore_Quality>();
        for (int i = 0; i < Ores_in_tray.Count; i++) //Generer ore og legg det i stockpilen for usortert ore
        {
            Ore_Quality.Add(Ores_in_tray[i].GetComponent<Common_Properties>().ore_quality);
        }
        List<int> ore_percent_quality = new List<int>();
        for (int i = 0; i < Ores_in_tray.Count; i++) //Generer ore og legg det i stockpilen for usortert ore
        {
            ore_percent_quality.Add(Ores_in_tray[i].GetComponent<Ore>().percent_ore_to_ingot);
        }
        StaticData.Ore_Quality.Add(Ore_Quality);
        StaticData.percent_ore_quality_ore_storage.Add(ore_percent_quality);

    }

    public void Loading() //Her henter vi ut igjen dataen fra staticdata filene, vi leser inn en og en list, og legger den til. S� fjernes det fra staticData
    {
        if (StaticData.Ore_Quality.Count > 0)
        {
            List<Enumtypes.Ore_Quality> Ore_Quality = StaticData.Ore_Quality[0];
            StaticData.Ore_Quality.RemoveAt(0);
            List<int> ore_percent_quality = StaticData.percent_ore_quality_ore_storage[0];
            StaticData.percent_ore_quality_ore_storage.RemoveAt(0);
            for (int i = 0; i < Ore_Quality.Count; i++) //Generer ore og legg det i stockpilen for usortert ore
            {

                handleQuality(Ore_Quality[i]);
                Ores_in_tray.Add(Generation_Object.create_ore((int)Ore_Quality[i], gameObject, ore_percent_quality[i]));
            }
            handleSprite();
        }
    }
}
