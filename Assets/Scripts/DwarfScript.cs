using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DwarfScript : MonoBehaviour
{
    Rigidbody2D rb;


    Unsorted_Ore_container Unsorted_Tray_Object;

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

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Nearest_Object = find_nearest_interactable_object_within_range(2);
            if (Inventory_Full)
            {
                //Kode for å slippe ting

            }
            else if (Inventory_Full == false)
            {
                //Kode for å plukke opp ting
                if (Nearest_Object.name == "Unsorted_Ore_Tray")
                {
                    Unsorted_Tray_Object = Find_Components.find_Unsorted_Tray();
                    Item_in_inventory = Unsorted_Tray_Object.Ores_in_tray.ElementAt(0);
                }
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
            if (curDistance < distance && curDistance < 2)
            {
                closest = go;
                distance = curDistance;
            }
        }
        return closest;
    }

}
