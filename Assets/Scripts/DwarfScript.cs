using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]public class DwarfScript : MonoBehaviour
{
    Rigidbody2D rb;

    public bool Inventory_Full = false;
    public GameObject Item_in_inventory = null;
    public GameObject Nearest_Object = null;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Denne trengs for � kunne gj�re physicsbasert movement
        Application.targetFrameRate = 60;
    }
    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal"); // H�yre og venstre verdiene
        float vertical = Input.GetAxis("Vertical"); // Opp og ned Verdien

        if (Inventory_Full && Input.GetKeyDown(KeyCode.Space) == false) //Vis inventory er tomt (als� false p� testen), og vi ikke trykker space, s� ska vi flytte med oss inventoriet
        {
            Item_in_inventory.transform.position = transform.position + new Vector3(0, 0.5f, 0);

        }
        if (Input.GetKeyDown(KeyCode.E)) //Her aktiverer vi objekter, evnt s� kan vi ha en spak vi interacter med for � gj�re det samme, vis alt ska v�re p� space-knappen
        {
            try
            {
                Nearest_Object = Find_nearest_interactable_object_within_range(5); // 
                Nearest_Object.transform.parent.SendMessage("Work", gameObject, SendMessageOptions.DontRequireReceiver); 
            }
            catch
            {
                //Do nothing
            }
        }
        if (Input.GetKeyDown(KeyCode.Q)) //Her pr�ver vi � plukke opp eller sette ned objekter, avhengig om vi har eller ikke har objekter allerede. 
        {
            Nearest_Object = Find_nearest_interactable_object_within_range(5);
            if (Inventory_Full == false && Nearest_Object != null)
            {
                try
                {
                    Nearest_Object.transform.parent.SendMessage("Pickup", gameObject, SendMessageOptions.DontRequireReceiver);
                }
                catch
                {
                    //Do nothing
                }
            }
            else if (Inventory_Full == true && Nearest_Object != null)
            {
                try
                {
                    Nearest_Object.transform.parent.SendMessage("Drop_Off", gameObject, SendMessageOptions.DontRequireReceiver);
                }
                catch
                {
                    //Do nothing
                }
                
            }
        }
        
        
        float moveByX = horizontal * 4; //Movement speed 
        float moveByY = vertical * 4; // Movement speed 
        rb.velocity = new Vector2(moveByX, moveByY); //Legge til krefer p� fysikken, slik at figuren beveger seg
    }
    private void Cleanup() // Som nevnt, denne rydder opp i inventory for � forhindre bugs
    {
        Item_in_inventory = null;
        Inventory_Full = false;
    }
    GameObject Find_nearest_interactable_object_within_range(int Range) //Her finner vi n�rmeste objekt innenfor objekter med riktig tag, returnerer ett GameObject
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
#pragma warning disable IDE0051 // Remove unused private members
    private void Inventory_Full_Message(bool result)
#pragma warning restore IDE0051 // Remove unused private members
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