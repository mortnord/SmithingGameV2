using System;
using System.Collections.Generic;
using UnityEngine;

public class Minecart : MonoBehaviour, IInteractor_Connector
{


    public List<GameObject> list_of_ore = new List<GameObject>();
    public Object_Creation Generation_Object;
    public float time_until_go = 5;
    Vector2 position;
    SpriteRenderer spriteRenderer;
    public Sprite[] spriteArray;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        Generation_Object = Find_Components.find_Object_Creation();
        GetOreFromBelow(); //Hent ore fra staticData. 
        position = transform.position;
    }

    private void GetOreFromBelow() //Lager malm i minecarten basert på kvaliteten i StaticData. 
    {
        for (int i = 0; i < StaticData.Transition_Ores.Count; i++)
        {
            list_of_ore.Add(Generation_Object.create_ore((int)StaticData.Transition_Ores[i], gameObject, StaticData.percent_ore_quality_transition[i]));
            list_of_ore[i].SetActive(false);
        }
        StaticData.Transition_Ores.Clear(); //fjerner dataen fra staticData.
    }

    // Update is called once per frame
    void Update()
    {
        if (list_of_ore.Count > 0 && gameObject.transform.position.y > 1.2) //Vis vi ikke har kjørt helt fram, og malmen er i vogna, gjør neste
        {
            
            spriteRenderer.sprite = spriteArray[0];
            if (time_until_go > 0) //Tid for å kjøre?, ja/nei
            {
                time_until_go -= 1 * Time.deltaTime; //nei
            }
            if (time_until_go < 0) //Ja
            {
                position.y -= 4 * Time.deltaTime;
                gameObject.transform.position = position; //Da turer vi. 
            }
        }
        else if (list_of_ore.Count > 0) //Vis framme, og malm i vogna, bytt sprite
        {
            spriteRenderer.sprite = spriteArray[1];
            time_until_go = 1;
        }
        else if (list_of_ore.Count == 0 && gameObject.transform.position.y < 7) //Tid for å dra tilbake?
        {
            spriteRenderer.sprite = spriteArray[2];
            if (time_until_go > 0) //Nei
            {
                time_until_go -= 1 * Time.deltaTime;
            }
            if (time_until_go < 0) //Ja
            {
                position.y += 2 * Time.deltaTime;
                gameObject.transform.position = position; //Vi turer tilbake igjen
            }
        }
       
    }

    public void Pickup(GameObject main_character) //Vi plukker opp ting
    {
        main_character.GetComponent<DwarfScript>().Item_in_inventory = list_of_ore[0];
        
        list_of_ore.RemoveAt(0);
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
