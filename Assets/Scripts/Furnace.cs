using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Furnace : MonoBehaviour
{

    TimerScript Timer_Object;
    Ingot_Form ingot_form_object;
    Object_Creation Generation_Object;
    public Sprite[] spriteArray;
    public SpriteRenderer spriteRenderer;
    public GameObject Item_in_inventory = null;
    public bool smelting_ready = false;
    public bool smelting_in_progress = false;

    public List<GameObject> Ores_in_furnace = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {

        Timer_Object = Find_Components.find_Timer_Object();
        ingot_form_object = Find_Components.find_ingot_form();
        Generation_Object = Find_Components.find_Object_Creation();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Ores_in_furnace.Count > 0)
        {
            spriteRenderer.sprite = spriteArray[1];
        }
        if (Ores_in_furnace.Count == 0)
        {
            spriteRenderer.sprite = spriteArray[0];
        }
        if (smelting_ready)
        {
            //Implementer en tidsbasert smelting her
            foreach (GameObject Ore in Ores_in_furnace)
            {
                ingot_form_object.Ingots_in_form.Add(Generation_Object.create_ingot(Ore.GetComponent<Ore>().quality));
                Destroy(Ore);
            }
            Ores_in_furnace.Clear();
            smelting_ready = false;
        }
    }
}
