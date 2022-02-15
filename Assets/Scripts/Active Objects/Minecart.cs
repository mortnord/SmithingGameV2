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
        GetOreFromBelow();
        position = transform.position;
    }

    private void GetOreFromBelow()
    {
        for (int i = 0; i < StaticData.Transition_Ores.Count; i++)
        {
            list_of_ore.Add(Generation_Object.create_ore((int)StaticData.Transition_Ores[i], gameObject));
            list_of_ore[i].SetActive(false);
        }
        StaticData.Transition_Ores.Clear();
    }

    // Update is called once per frame
    void Update()
    {
        if (list_of_ore.Count > 0 && gameObject.transform.position.y > 1.2)
        {
            spriteRenderer.sprite = spriteArray[0];
            if (time_until_go > 0)
            {
                time_until_go -= 1 * Time.deltaTime;
            }
            if (time_until_go < 0)
            {
                position.y -= 4 * Time.deltaTime;
                gameObject.transform.position = position;
            }
        }
        else if (list_of_ore.Count > 0)
        {
            spriteRenderer.sprite = spriteArray[1];
            time_until_go = 1;
        }
        else if (list_of_ore.Count == 0 && gameObject.transform.position.y < 7)
        {
            spriteRenderer.sprite = spriteArray[2];
            if (time_until_go > 0)
            {
                time_until_go -= 1 * Time.deltaTime;
            }
            if (time_until_go < 0)
            {
                position.y += 2 * Time.deltaTime;
                gameObject.transform.position = position;
            }
        }
       
    }

    public void Pickup(GameObject main_character)
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
