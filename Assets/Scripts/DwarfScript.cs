using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DwarfScript : MonoBehaviour
{
    Rigidbody2D rb;
    public bool Inventory_Full = false;
    GameObject Item_in_inventory = null;
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
            GameObject Nearest_Object = find_nearest_interactable_object_within_range(2);
            if (Inventory_Full)
            {
                //Kode for å slippe ting
            }
            else if (Inventory_Full == false)
            {
                //Kode for å plukke opp ting
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
