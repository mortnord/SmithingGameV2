using System;
using System.Collections.Generic;
using UnityEngine;

public class Furnace : MonoBehaviour, IInteractor_Connector, IIData_transfer
{
    Ingot_Form ingot_form_object;
    Object_Creation Generation_Object;

    public Sprite[] spriteArray;
    public SpriteRenderer spriteRenderer;
    public GameObject Item_in_inventory = null;

    public bool smelting_in_progress = false;
    public float smelting_time = 11;

    public int quality_in_input = 0;
    public int quality_in_furnace = 0;
    public int smelting_quality = 0;
    public bool result;

    public List<GameObject> Ores_in_furnace = new List<GameObject>();
    // Start is called before the first frame update
    void Awake()
    {
        //f�rste vi gj�r er � finne objekten til andre scripts vi trenger � gj�re noe med, dette skjer her. 
        ingot_form_object = Find_Components.find_ingot_form(); //Her havner ingots.
        Generation_Object = Find_Components.find_Object_Creation();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>(); //Dette er spirit-renderen, vi kan bruke denne til � bytte sprites.
    }

    // Update is called once per frame
    void Update()
    {
        if (Ores_in_furnace.Count > 0) //Denne delen setter spriten til element 1 vis vi har 1 eller mer ores i furnacen
        {
            spriteRenderer.sprite = spriteArray[1];
            smelting_time -= 1 * Time.deltaTime;
            smelting_in_progress = true;

        }
        if (Ores_in_furnace.Count == 0) // Denne resetter tilbake til 0, vis vi er tomme for ore.
        {
            spriteRenderer.sprite = spriteArray[0];
            smelting_time = 11;
            smelting_in_progress = false;
        }
        if (smelting_time < 1) //N�r det har g�tt 10 sekund etter at vi la inn f�rste malmen, s� smeltes den til en barre. 
        {
            smelting_quality = FindQuality(Ores_in_furnace[0]);
            ingot_form_object.Ingots_in_form.Add(Generation_Object.create_ingot(smelting_quality));
            Destroy(Ores_in_furnace[0]);
            Ores_in_furnace.RemoveAt(0); //T�mme inventory og cleare, ingots har blitt laget i ingot_form objektet, s� det g�r fint

            smelting_time = 11;

        }


    }
    public int FindQuality(GameObject item_in_inventory)
    {
        
        if (item_in_inventory.GetComponent<Ore>() != null) //Hvis ore, sjekk kvalitet og bruk det som input. 
        {
            quality_in_input = (int)item_in_inventory.GetComponent<Ore>().ore_quality;
        }
        else if (item_in_inventory.GetComponent<Ingot>() != null) //Sjekker hvis det er ingot
        {
            quality_in_input = (int)item_in_inventory.GetComponent<Ingot>().ore_quality;
        }
        else if (item_in_inventory.GetComponent<Sword>() != null) //sjekker hvis det er sword. Finn en bedre l�sning her. 
        {
            quality_in_input = (int)item_in_inventory.GetComponent<Sword>().ore_quality;
        }
        return quality_in_input;
    }
    public void Pickup(GameObject main_character)
    {
        //Do Nothing
    }

    public void Drop_Off(GameObject main_character) //Interface metode for � legge fra seg ting
    {
        result = handleQuality(FindQuality(main_character.GetComponent<DwarfScript>().Item_in_inventory));
        if (result == true)
        {
            main_character.GetComponent<DwarfScript>().Item_in_inventory.transform.position = gameObject.transform.position;
            main_character.GetComponent<DwarfScript>().Item_in_inventory.SetActive(false);
            Ores_in_furnace.Add(main_character.GetComponent<DwarfScript>().Item_in_inventory);
            Return_Answer(main_character, false);
        }

    }

    private bool handleQuality(int ore_quality) //Her setter vi kvaliteten som skal v�re i furnacen, dette hindrer 2 typer malm � v�re inni samtidig.
    {
        if (Ores_in_furnace.Count == 0)
        {
            quality_in_furnace = ore_quality;
            return true;
        }
        else if (ore_quality == quality_in_furnace)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void Return_Answer(GameObject main_character, bool result) //Interface metode for � returnere svar
    {
        main_character.SendMessage("Inventory_Full_Message", result);
    }

    public void Storage() //Her lagrer vi malmen som er i furnacen. 
    {
        for (int i = 0; i < Ores_in_furnace.Count; i++) //Generer ore og legg det i stockpilen for usortert ore
        {
            if (Ores_in_furnace[i].GetComponent<Ore>() != null) //Hvis Ore, hent kvaliteten. og lagre den. 
            {
                StaticData.furnace_quality_static_object.Add(Ores_in_furnace[i].GetComponent<Ore>().ore_quality);
            }
            else if (Ores_in_furnace[i].GetComponent<Ingot>() != null) //Samme p� ingots og barre
            {
                StaticData.furnace_quality_static_object.Add(Ores_in_furnace[i].GetComponent<Ingot>().ore_quality);
            }
            else if (Ores_in_furnace[i].GetComponent<Sword>() != null)
            {
                StaticData.furnace_quality_static_object.Add(Ores_in_furnace[i].GetComponent<Sword>().ore_quality);
            }
        }
        StaticData.smelting_time_static = smelting_time; //Lagre annen data og
        StaticData.smelting_input_static = quality_in_furnace;
    }

    public void Loading() //Last inn data. 
    {
        if (StaticData.furnace_quality_static_object.Count > 0)
        {
            for (int i = 0; i < StaticData.furnace_quality_static_object.Count; i++) //Generer ore og legg det i stockpilen for usortert ore
            {
                print(StaticData.furnace_quality_static_object[i]); //Vi lager alt som malm, selv om det kanskje var ett sverd f.eks, f�r,
                                                                    //dette har lite betydning siden du ikke kan hente det opp igjen
                Ores_in_furnace.Add(Generation_Object.create_ore((int)StaticData.furnace_quality_static_object[i], gameObject));
                Ores_in_furnace[i].GetComponent<Ore>().ore_quality = (Enumtypes.Ore_Quality)Ores_in_furnace[i].GetComponent<Ore>().quality;
                Ores_in_furnace[i].SetActive(false);
            }
            StaticData.furnace_quality_static_object.Clear();
        }
        //Henter in data
        smelting_time = StaticData.smelting_time_static;
        smelting_quality = StaticData.smelting_input_static;
        quality_in_furnace = StaticData.smelting_input_static;
        quality_in_input = StaticData.smelting_input_static;
    }
}
