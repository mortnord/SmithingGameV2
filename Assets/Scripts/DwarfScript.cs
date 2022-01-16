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

    public bool Inventory_Full = false;
    public GameObject Item_in_inventory = null;
    public GameObject Nearest_Object = null;
    // Start is called before the first frame update
    void Start()
    {
        
        rb = GetComponent<Rigidbody2D>(); // Denne trengs for å kunne gjøre physicsbasert movement
    }

    // Update is called once per frame
    void Update() 
    {
        float horizontal = Input.GetAxis("Horizontal"); // Høyre og venstre verdiene
        float vertical = Input.GetAxis("Vertical"); // Opp og ned Verdien

        if(Inventory_Full && Input.GetKeyDown(KeyCode.Space) == false) //Vis inventory er tomt (alså false på testen), og vi ikke trykker space, så ska vi flytte med oss inventoriet
        {
            Item_in_inventory.transform.position = transform.position + new Vector3(0, 0.5f, 0);
        }

        if (Input.GetKeyDown(KeyCode.Space)) //Gjør kun ting vis space er trykket ned
        {
            Nearest_Object = find_nearest_interactable_object_within_range(3); // Finner nærmeste objekt (ofte stockpiles eller furnace) med taggen "Pickable Object" innen 5 units distanse. 
            if (Inventory_Full) //Kun vis inventory er full, da prøver den å legge noe på plass
            {
                if (Nearest_Object.name == "Sorted_Ore_Tray_Low") //Her er det lav kvalitet ore den prøver å legge noe i 
                {
                    Sorted_Ore_Tray_Object = Find_Components.find_Sorted_Tray_Low(); // Her finner vi gameObjektet sin script-component, som vi ska legge ting i
                    if (Item_in_inventory.GetComponent<Ore>().quality == 0) // Test for å sjekke om det jeg har i inventory er riktig kvalitet for stockpilen
                    {
                        Sorted_Ore_Tray_Object.Ores_in_tray.Add(Item_in_inventory); // Her legger vi objektet i Inventory i arrayen som holder objekter i stockpilen

                        Item_in_inventory.transform.position = Sorted_Ore_Tray_Object.transform.position; // Her flytter den selve item objektet oppi stockpilen,
                                                                                                          // muligens endre til distinkt plass, eller bare la spriten dekke over slik den gjør nå
                        Cleanup(); // Metode som rydder opp i Inventory til maincharacter, for å forhindre bugs.
                    }
                    else // Vis testen om riktig kvalitet på riktig stockpile failer, skjer dette
                    {
                        print("Feil plass"); // Her tenkte jeg vi kunne ha en beskjed til spiller om feil stockpile?
                    }
                    
                } //Alle metodene herunder er tilsvarende, kanskje noe å endre til bedre, helst i færre if-setninger, optimalt kun i en if-setning?
                if (Nearest_Object.name == "Sorted_Ore_Tray_Medium")
                {
                    Sorted_Ore_Tray_Object = Find_Components.find_Sorted_Tray_Medium();
                    if (Item_in_inventory.GetComponent<Ore>().quality == 1)
                    {
                        Sorted_Ore_Tray_Object.Ores_in_tray.Add(Item_in_inventory);

                        Item_in_inventory.transform.position = Sorted_Ore_Tray_Object.transform.position;
                        Cleanup();
                    }
                    else
                    {
                        print("Feil plass");
                    }

                }
                if (Nearest_Object.name == "Sorted_Ore_Tray_High")
                {
                    Sorted_Ore_Tray_Object = Find_Components.find_Sorted_Tray_High();
                    if (Item_in_inventory.GetComponent<Ore>().quality == 2)
                    {
                        Sorted_Ore_Tray_Object.Ores_in_tray.Add(Item_in_inventory);

                        Item_in_inventory.transform.position = Sorted_Ore_Tray_Object.transform.position;
                        Cleanup();
                    }
                    else
                    {
                        print("Feil plass");
                    }

                }
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
                    if (Item_in_inventory.GetComponent<Ingot>().quality == 0)
                    {
                        Sorted_Ingots_Tray_Object.Ingots_in_tray.Add(Item_in_inventory);

                        Item_in_inventory.transform.position = Sorted_Ingots_Tray_Object.transform.position;
                        Cleanup();
                    }
                    else
                    {
                        print("Feil plass");
                    }
                }
                if (Nearest_Object.name == "Ingot_Tray_Medium")
                {
                    Sorted_Ingots_Tray_Object = Find_Components.find_ingot_tray_medium();
                    if (Item_in_inventory.GetComponent<Ingot>().quality == 1)
                    {
                        Sorted_Ingots_Tray_Object.Ingots_in_tray.Add(Item_in_inventory);

                        Item_in_inventory.transform.position = Sorted_Ingots_Tray_Object.transform.position;
                        Cleanup();
                    }
                    else
                    {
                        print("Feil plass");
                    }
                }
                if (Nearest_Object.name == "Ingot_Tray_High")
                {
                    Sorted_Ingots_Tray_Object = Find_Components.find_ingot_tray_high();
                    if (Item_in_inventory.GetComponent<Ingot>().quality == 2)
                    {
                        Sorted_Ingots_Tray_Object.Ingots_in_tray.Add(Item_in_inventory);

                        Item_in_inventory.transform.position = Sorted_Ingots_Tray_Object.transform.position;
                        Cleanup();
                    }
                    else
                    {
                        print("Feil plass");
                    }
                }
                if (Nearest_Object.name == "Anvil")
                {
                    if (Item_in_inventory.GetComponent<Ingot>() != null)
                    {
                        Anvil_Object = Find_Components.find_anvil();
                        Anvil_Object.Converted_Object = Item_in_inventory;
                        Item_in_inventory.transform.position = Anvil_Object.transform.position;
                        Cleanup();
                    }
                }
                if (Nearest_Object.name == "Export_Chute")
                {
                    export_chute_Object = Find_Components.find_export_chute();
                    export_chute_Object.Stuff_to_transport.Add(Item_in_inventory);
                    Item_in_inventory.transform.position = export_chute_Object.transform.position;
                    Cleanup();
                }

            }
            else if (Inventory_Full == false && Nearest_Object != null) // Her plukker vi opp ting, vis vi har ting nærme nok fra å plukke opp fra
            {
                //Kode for å plukke opp ting
                if (Nearest_Object.name == "Unsorted_Ore_Tray") //Her finner vi ut av hvilken ting vi prøver å plukke opp fra. 
                {
                    Unsorted_Tray_Object = Find_Components.find_Unsorted_Tray();  // Her finner vi gameObjektet sin script-component, som vi prøver å loote
                    Item_in_inventory = Unsorted_Tray_Object.Ores_in_tray.ElementAt(0); //Her setter vi inventory til character til lik element 0 (alså første i arrayen)
                    Unsorted_Tray_Object.Ores_in_tray.RemoveAt(0); // Her fjerner vi objektet fra stockpilen //Etterpå så vil nr 1 i arrayen bli nr 0 osv, så det fikser seg selv
                    Inventory_Full = true; //Her har vi inventory fult.
                }
                //Alle disse nedover er gjentagende kode, bare med 1 forskjellig metodekall, ønsker tips for å slå dette sammen.
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
                if (Nearest_Object.name == "Sorted_Ore_Tray_Low")
                {
                    Sorted_Ore_Tray_Object = Find_Components.find_Sorted_Tray_Low();
                    Item_in_inventory = Sorted_Ore_Tray_Object.Ores_in_tray.ElementAt(0);
                    Sorted_Ore_Tray_Object.Ores_in_tray.RemoveAt(0);
                    Inventory_Full = true;
                }
                if (Nearest_Object.name == "Sorted_Ore_Tray_Medium")
                {
                    Sorted_Ore_Tray_Object = Find_Components.find_Sorted_Tray_Medium();
                    Item_in_inventory = Sorted_Ore_Tray_Object.Ores_in_tray.ElementAt(0);
                    Sorted_Ore_Tray_Object.Ores_in_tray.RemoveAt(0);
                    Inventory_Full = true;
                    
                }
                if (Nearest_Object.name == "Sorted_Ore_Tray_High")
                {
                    Sorted_Ore_Tray_Object = Find_Components.find_Sorted_Tray_High();
                    Item_in_inventory = Sorted_Ore_Tray_Object.Ores_in_tray.ElementAt(0);
                    Sorted_Ore_Tray_Object.Ores_in_tray.RemoveAt(0);
                    Inventory_Full = true;
                }
                if (Nearest_Object.name == "Ingot_Tray_Low")
                { 
                    Sorted_Ingots_Tray_Object = Find_Components.find_ingot_tray_low();
                    Item_in_inventory = Sorted_Ingots_Tray_Object.Ingots_in_tray.ElementAt(0);
                    Sorted_Ingots_Tray_Object.Ingots_in_tray.RemoveAt(0);
                    Inventory_Full = true;
                }
                if (Nearest_Object.name == "Ingot_Tray_Medium")
                {
                    Sorted_Ingots_Tray_Object = Find_Components.find_ingot_tray_medium();
                    Item_in_inventory = Sorted_Ingots_Tray_Object.Ingots_in_tray.ElementAt(0);
                    Sorted_Ingots_Tray_Object.Ingots_in_tray.RemoveAt(0);
                    Inventory_Full = true;
                }
                if (Nearest_Object.name == "Ingot_Tray_High")
                {
                    Sorted_Ingots_Tray_Object = Find_Components.find_ingot_tray_high();
                    Item_in_inventory = Sorted_Ingots_Tray_Object.Ingots_in_tray.ElementAt(0);
                    Sorted_Ingots_Tray_Object.Ingots_in_tray.RemoveAt(0);
                    Inventory_Full = true;
                }
                if (Nearest_Object.name == "Anvil")
                {
                    Anvil_Object = Find_Components.find_anvil();
                    Item_in_inventory = Anvil_Object.Converted_Object;
                    Anvil_Object.Converted_Object = null;
                    Inventory_Full = true;
                }
            }
            
            
        }
        if (Input.GetKeyDown(KeyCode.E)) //Her aktiverer vi furnacen, evnt så kan vi ha en spak vi interacter med for å gjøre det samme, vis alt ska være på space-knappen
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
        
        float moveByX = horizontal * 4; //Movement speed 
        float moveByY = vertical * 4; // Movement speed 
        rb.velocity = new Vector2(moveByX, moveByY); //Legge til krefer på fysikken, slik at figuren beveger seg
    }

    private void Cleanup() // Som nevnt, denne rydder opp i inventory for å forhindre bugs
    {
        Item_in_inventory = null;
        Inventory_Full = false;
    }

    GameObject find_nearest_interactable_object_within_range(int Range) //Her finner vi nærmeste objekt innenfor objekter med riktig tag, returnerer ett GameObject
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Pickable Object");
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

    
}
