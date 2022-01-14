using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DwarfScript : MonoBehaviour
{
    Rigidbody2D rb;
    Unsorted_Ore_container Unsorted_Tray_Object;
    Sorted_Ore_Tray Sorted_Ore_Tray_Object;
    Furnace Furnace_Object;

    public bool Inventory_Full = false;
    public GameObject Item_in_inventory = null;
    public GameObject Nearest_Object = null;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        if(Inventory_Full && Input.GetKeyDown(KeyCode.Space) == false)
        {
            Item_in_inventory.transform.position = transform.position + new Vector3(0, 0.5f, 0);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Nearest_Object = find_nearest_interactable_object_within_range(5);
            if (Inventory_Full)
            {
                if (Nearest_Object.name == "Sorted_Ore_Tray_Low")
                {
                    Sorted_Ore_Tray_Object = Find_Components.find_Sorted_Tray_Low();
                    Sorted_Ore_Tray_Object.Ores_in_tray.Add(Item_in_inventory);

                    Item_in_inventory.transform.position = Sorted_Ore_Tray_Object.transform.position;
                    
                    Item_in_inventory = null;
                    Inventory_Full = false;
                }
                if (Nearest_Object.name == "Sorted_Ore_Tray_Medium")
                {
                    Sorted_Ore_Tray_Object = Find_Components.find_Sorted_Tray_Medium();
                    Sorted_Ore_Tray_Object.Ores_in_tray.Add(Item_in_inventory);

                    Item_in_inventory.transform.position = Sorted_Ore_Tray_Object.transform.position;

                    Item_in_inventory = null;
                    Inventory_Full = false;
                }
                if (Nearest_Object.name == "Sorted_Ore_Tray_High")
                {
                    Sorted_Ore_Tray_Object = Find_Components.find_Sorted_Tray_High();
                    Sorted_Ore_Tray_Object.Ores_in_tray.Add(Item_in_inventory);

                    Item_in_inventory.transform.position = Sorted_Ore_Tray_Object.transform.position;

                    Item_in_inventory = null;
                    Inventory_Full = false;
                }
                if (Nearest_Object.name == "Furnace")
                {
                    Furnace_Object = Find_Components.find_furnace();
                    Furnace_Object.Ores_in_furnace.Add(Item_in_inventory);

                    Item_in_inventory.transform.position = Furnace_Object.transform.position;

                    Item_in_inventory = null;
                    Inventory_Full = false;
                }

            }
            else if (Inventory_Full == false && Nearest_Object != null)
            {
                //Kode for å plukke opp ting
                if (Nearest_Object.name == "Unsorted_Ore_Tray")
                {
                    Unsorted_Tray_Object = Find_Components.find_Unsorted_Tray();
                    Item_in_inventory = Unsorted_Tray_Object.Ores_in_tray.ElementAt(0);
                    Unsorted_Tray_Object.Ores_in_tray.RemoveAt(0);
                    Item_in_inventory.transform.position = transform.position + new Vector3(0,0.5f,0);
                    Inventory_Full = true;
                }
            }
            
            
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            Nearest_Object = find_nearest_interactable_object_within_range(5);
            if (Nearest_Object.name == "Furnace")
            {
                Furnace_Object = Find_Components.find_furnace();
                Furnace_Object.smelting_ready = true;

            }
        }
        
        float moveByX = horizontal * 2;
        float moveByY = vertical * 2;
        rb.velocity = new Vector2(moveByX, moveByY);
    }
    GameObject find_nearest_interactable_object_within_range(int Range)
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
