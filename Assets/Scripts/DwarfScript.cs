using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DwarfScript : MonoBehaviour
{
    Rigidbody2D rb;
    Unsorted_Ore_container Unsorted_Tray_Object;
    Ingot_Form ingot_form_object;
    Sorted_Ore_Tray Sorted_Ore_Tray_Object;
    Sorted_Ingots_Tray Sorted_Ingots_Tray_Object;
    Furnace Furnace_Object;
    Anvil Anvil_Object;
    Export_Chute export_chute_Object;
    Trashcan trashcan_object;
    Table table_object;

    public bool Inventory_Full = false;
    public GameObject Item_in_inventory = null;
    public GameObject Nearest_Object = null;
   

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Denne trengs for � kunne gj�re physicsbasert movement
    }
    // Update is called once per frame
    void Update() 
    {
        float horizontal = Input.GetAxis("Horizontal"); // H�yre og venstre verdiene
        float vertical = Input.GetAxis("Vertical"); // Opp og ned Verdien

        if(Inventory_Full && Input.GetKeyDown(KeyCode.Space) == false) //Vis inventory er tomt (als� false p� testen), og vi ikke trykker space, s� ska vi flytte med oss inventoriet
        {
            Item_in_inventory.transform.position = transform.position + new Vector3(0, 0.5f, 0);
        
        }

        if (Input.GetKeyDown(KeyCode.Space)) //Gj�r kun ting vis space er trykket ned
        {
            Nearest_Object = find_nearest_interactable_object_within_range(3); // Finner n�rmeste objekt (ofte stockpiles eller furnace) med taggen "Pickable Object" innen 5 units distanse. 
            if (Inventory_Full) //Kun vis inventory er full, da pr�ver den � legge noe p� plass
            {
                
                if (Nearest_Object.name == "Furnace")
                {
                    if(Item_in_inventory.GetComponent<Ore>() != null)
                    {
                        Furnace_Object = Find_Components.find_furnace();
                        Furnace_Object.Ores_in_furnace.Add(Item_in_inventory);

                        Item_in_inventory.transform.position = Furnace_Object.transform.position;
                        Cleanup();
                    }
                }
                if (Nearest_Object.name == "Ingot_Tray_Low")
                {
                    Sorted_Ingots_Tray_Object = Find_Components.find_ingot_tray_low();
                    if ((int)Item_in_inventory.GetComponent<Ingot>().ore_quality == 0)
                    {
                        Insert_Into_Ingot_Tray();
                    }
                    else
                    {
                        print("Feil plass");
                    }
                }
                else if (Nearest_Object.name == "Ingot_Tray_Medium")
                {
                    Sorted_Ingots_Tray_Object = Find_Components.find_ingot_tray_medium();
                    if ((int)Item_in_inventory.GetComponent<Ingot>().ore_quality == 1)
                    {
                        Insert_Into_Ingot_Tray();
                    }
                    else
                    {
                        print("Feil plass");
                    }
                }
                else if (Nearest_Object.name == "Ingot_Tray_High")
                {
                    Sorted_Ingots_Tray_Object = Find_Components.find_ingot_tray_high();
                    if ((int)Item_in_inventory.GetComponent<Ingot>().ore_quality == 2)
                    {
                        Insert_Into_Ingot_Tray();
                    }
                    else
                    {
                        print("Feil plass");
                    }
                }
                if (Nearest_Object.name == "Anvil")
                {
                    Anvil_Object = Find_Components.find_anvil();
                    if (Item_in_inventory.GetComponent<Ingot>() != null) //Her legger vi inn ingots i anvilen
                    {
                        Anvil_Object.object_to_be_destroyed = Item_in_inventory;
                        Item_in_inventory.transform.position = Anvil_Object.transform.position;
                        Cleanup();
                    }
                    if (Item_in_inventory.GetComponent<Blueprint_Sword>() != null) //Her kommer blueprints inn i anvilen
                    {
                        Anvil_Object.blueprint_copy = Item_in_inventory;
                        Item_in_inventory.transform.position = Anvil_Object.transform.position;
                        Cleanup();
                    }
                }
                if (Nearest_Object.name == "Export_Chute")
                {
                    export_chute_Object = Find_Components.find_export_chute();
                    if(Item_in_inventory.GetComponent<Sword>())
                    {
                        export_chute_Object.Stuff_to_transport.Add(Item_in_inventory);
                        Item_in_inventory.transform.position = export_chute_Object.transform.position;
                        Cleanup();
                    }
                        
                }
                if (Nearest_Object.name == "Trashcan")
                {
                    trashcan_object = Find_Components.find_trashcan();
                    trashcan_object.Destroyable_Object = Item_in_inventory;
                    Cleanup();
                }
                

            }
            else if (Inventory_Full == false && Nearest_Object != null) // Her plukker vi opp ting, vis vi har ting n�rme nok fra � plukke opp fra
            {
                
                //Alle disse nedover er gjentagende kode, bare med 1 forskjellig metodekall, �nsker tips for � sl� dette sammen.
                if (Nearest_Object.name == "Ingot_form")  // Alle disse nedover er like
                {
                    ingot_form_object = Find_Components.find_ingot_form();
                    if (ingot_form_object.Ingots_in_form.Count > 0)
                    {
                        Item_in_inventory = ingot_form_object.Ingots_in_form.ElementAt(0);
                        ingot_form_object.Ingots_in_form.RemoveAt(0);
                        Inventory_Full = true;
                    }
                }
                
                if (Nearest_Object.name == "Ingot_Tray_Low")
                {
                    Sorted_Ingots_Tray_Object = Find_Components.find_ingot_tray_low();
                    Remove_From_Ingot_Tray();
                }
                else if (Nearest_Object.name == "Ingot_Tray_Medium")
                {
                    Sorted_Ingots_Tray_Object = Find_Components.find_ingot_tray_medium();
                    Remove_From_Ingot_Tray();
                }
                else if (Nearest_Object.name == "Ingot_Tray_High")
                {
                    Sorted_Ingots_Tray_Object = Find_Components.find_ingot_tray_high();
                    Remove_From_Ingot_Tray();
                }
                if (Nearest_Object.name == "Anvil")
                {
                    Anvil_Object = Find_Components.find_anvil();
                    if(Anvil_Object.Converted_Object != null)
                    {
                        Item_in_inventory = Anvil_Object.Converted_Object;
                        Anvil_Object.Converted_Object = null;
                        Inventory_Full = true;
                    }
                    else if(Anvil_Object.blueprint_copy != null)
                    {
                        Item_in_inventory = Anvil_Object.blueprint_copy;
                        Anvil_Object.blueprint_copy = null;
                        Inventory_Full = true;
                    }
                    
                }
                if (Nearest_Object.name == "Table_Blueprints")
                {
                    print("Finner table");
                    table_object = Find_Components.find_table();
                    table_object.copied = true;
                    Item_in_inventory = table_object.fake_copy;
                    table_object.fake_copy = null;
                    Item_in_inventory.transform.parent = null;
                    Inventory_Full = true;
                }

            }

        }
        if (Input.GetKeyDown(KeyCode.E)) //Her aktiverer vi objekter, evnt s� kan vi ha en spak vi interacter med for � gj�re det samme, vis alt ska v�re p� space-knappen
        {
            Nearest_Object = find_nearest_interactable_object_within_range(5); // 
            if (Nearest_Object.name == "Furnace")
            {
                Furnace_Object = Find_Components.find_furnace(); //Vi finner skript-koden til furnacen
                Furnace_Object.smelting_ready = true; // Her setter vi furnacen sin smelting test til true, da vil furnacen endre ore til ingots med tilsvarende kvalitet

            }
            if(Nearest_Object.name == "Anvil")
            {
                Anvil_Object = Find_Components.find_anvil();
                Anvil_Object.convert = true;
            }
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Nearest_Object = find_nearest_interactable_object_within_range(5);
            if (Inventory_Full == false)
            {
                Nearest_Object.transform.parent.SendMessage("Pickup", gameObject);
            }
            else if(Inventory_Full == true)
            {
                Nearest_Object.transform.parent.SendMessage("Drop_Off", gameObject);
            }
        }      
        float moveByX = horizontal * 4; //Movement speed 
        float moveByY = vertical * 4; // Movement speed 
        rb.velocity = new Vector2(moveByX, moveByY); //Legge til krefer p� fysikken, slik at figuren beveger seg
    }

    private void Insert_Into_Ingot_Tray() //Felles metode for ingot trays
    {
        Sorted_Ingots_Tray_Object.Ingots_in_tray.Add(Item_in_inventory);

        Item_in_inventory.transform.position = Sorted_Ingots_Tray_Object.transform.position;
        Cleanup();
    }
    private void Remove_From_Ingot_Tray() //Felles metode for ingot trays
    {
        Item_in_inventory = Sorted_Ingots_Tray_Object.Ingots_in_tray.ElementAt(0);
        Sorted_Ingots_Tray_Object.Ingots_in_tray.RemoveAt(0);
        Inventory_Full = true;
    }

    private void Cleanup() // Som nevnt, denne rydder opp i inventory for � forhindre bugs
    {
        Item_in_inventory = null;
        Inventory_Full = false;
    }

    GameObject find_nearest_interactable_object_within_range(int Range) //Her finner vi n�rmeste objekt innenfor objekter med riktig tag, returnerer ett GameObject
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Interact_Object");

        GameObject closest = null;

        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;

            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance && curDistance < Range)
            {
                closest = go;
                distance = curDistance;
            }
        }
        return closest;
    }
    private void Inventory_Full_Message(bool result)
    {
        if (result == true)
        {
            Inventory_Full = true;
        }
        else
        {
            Cleanup();
        }
        print(result);
    }
   
}
