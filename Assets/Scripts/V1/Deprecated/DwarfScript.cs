using System;
using UnityEngine;
[Serializable]
public class DwarfScript : MonoBehaviour
{
    Rigidbody2D rb;
    public bool Inventory_Full = false;
    public GameObject Item_in_inventory = null;
    public GameObject Nearest_Object = null;
    public GameObject All_Scenes_UI;
    public int last_direction = 0; // W = 1; A = 2; D = 3; S = 4;
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
        if (Inventory_Full) //Vis inventory er tomt (alså false på testen), og vi ikke trykker space, så ska vi flytte med oss inventoriet
        {
            Item_in_inventory.transform.position = transform.position + new Vector3(0, 0.5f, 0);
            Item_in_inventory.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Nearest_Object = Find_Nearest_Interactable_Object_Within_Range(2);
            Nearest_Object.transform.parent.SendMessage("Player_Enter", SendMessageOptions.DontRequireReceiver);
            gameObject.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.E)) //Her aktiverer vi objekter, evnt så kan vi ha en spak vi interacter med for å gjøre det samme, vis alt ska være på space-knappen
        {
            try
            {
                Nearest_Object = Find_Nearest_Interactable_Object_Within_Range(5); // 
                Nearest_Object.transform.parent.SendMessage("Work", gameObject, SendMessageOptions.DontRequireReceiver);
            }
            catch
            {
                //Do nothing
            }
        }
        
        if(Input.GetKey(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow))
        {
            last_direction = 1;
        }
        else if (Input.GetKey(KeyCode.A) || Input.GetKeyUp(KeyCode.LeftArrow))
        {
            last_direction = 2;
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKeyUp(KeyCode.RightArrow))
        {
            last_direction = 3;
        }
        else if (Input.GetKey(KeyCode.S) || Input.GetKeyUp(KeyCode.DownArrow))
        {
            last_direction = 4;
        }
        if (Input.GetKeyDown(KeyCode.Q)) //Her prøver vi å plukke opp eller sette ned objekter, avhengig om vi har eller ikke har objekter allerede. 
        {
            Nearest_Object = Find_Nearest_Interactable_Object_Within_Range(5);
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
        rb.velocity = new Vector2(moveByX, moveByY); //Legge til krefer på fysikken, slik at figuren beveger seg
    }
    private void Cleanup() // Som nevnt, denne rydder opp i inventory for å forhindre bugs
    {
        Item_in_inventory = null;
        Inventory_Full = false;
    }
    GameObject Find_Nearest_Interactable_Object_Within_Range(int Range) //Her finner vi nærmeste objekt innenfor objekter med riktig tag, returnerer ett GameObject
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
    public void Set_Item_In_Inventory(GameObject inventory_object_inn)
    {
        Item_in_inventory = inventory_object_inn;
    }
    public GameObject Get_Item_In_Inventory()
    {
        return Item_in_inventory;
    }
    public int Get_Direction()
    {
        return last_direction;
    }
}