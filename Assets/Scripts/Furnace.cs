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
        //første vi gjør er å finne objekten til andre scripts vi trenger å gjøre noe med, dette skjer her. 
        Timer_Object = Find_Components.find_Timer_Object();
        ingot_form_object = Find_Components.find_ingot_form(); //Her havner ingots.
        Generation_Object = Find_Components.find_Object_Creation();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>(); //Dette er spirit-renderen, vi kan bruke denne til å bytte sprites.
    }

    // Update is called once per frame
    void Update()
    {
        if (Ores_in_furnace.Count > 0) //Denne delen setter spriten til element 1 vis vi har 1 eller mer ores i furnacen
        {
            spriteRenderer.sprite = spriteArray[1];
        }
        if (Ores_in_furnace.Count == 0) // Denne resetter tilbake til 0, vis vi er tomme for ore.
        {
            spriteRenderer.sprite = spriteArray[0];
        }
        if (smelting_ready) //Når vi får klarsignalet fra dwarfen, så starter smelting
        {
            //Implementer en tidsbasert smelting her
            foreach (GameObject Ore in Ores_in_furnace)  // For hver ore, så lager vi en tilsvarende ingot med tilsvarende kvalitet, så destroyer vi oren
            {
                ingot_form_object.Ingots_in_form.Add(Generation_Object.create_ingot(Ore.GetComponent<Ore>().quality));
                Destroy(Ore);
            }
            Ores_in_furnace.Clear(); //Tømme inventory og cleare, ingots har blitt laget i ingot_form objektet, så det går fint
            smelting_ready = false; //reset tilbake til tidligere stadie
        }
    }
}
