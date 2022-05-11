using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacterStateManager : MonoBehaviour
{
    public GameObject Item_In_Inventory = null;
    public bool Inventory_Full = false;
    public GameObject Nearest = null;
    public MainCharacterBaseState Current_State;

    public MainCharacterCarryingState Carrying_State = new MainCharacterCarryingState();
    public MainCharacterStandingState Standing_State = new MainCharacterStandingState();
    public MainCharacterMovingState Moving_State = new MainCharacterMovingState();
    public MainCharacterCarryingAndMoving Carrying_And_Moving = new MainCharacterCarryingAndMoving();
    public MainCharacterRidingState Riding_state = new MainCharacterRidingState();
    Rigidbody2D Rigidbody_Main_Character;

    public int last_direction = 0; // W = 1; A = 2; D = 3; S = 4;

    // Start is called before the first frame update
    void Start()
    {
        Current_State = Standing_State;
        Current_State.EnterState(this);

        Rigidbody_Main_Character = GetComponent<Rigidbody2D>(); // Denne trengs for å kunne gjøre physicsbasert movement
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal"); // Høyre og venstre verdiene
        float vertical = Input.GetAxis("Vertical"); // Opp og ned Verdien
        Vector2 movement = new Vector2(horizontal, vertical);
        if(Input.anyKey)
        {
            HandleInputs(movement);
        }
        if (Inventory_Full) //Vis inventory er tomt (alså false på testen), og vi ikke trykker space, så ska vi flytte med oss inventoriet
        {
            Item_In_Inventory.transform.position = transform.position + new Vector3(0, 0.5f, 0);
            Item_In_Inventory.SetActive(true);
        }
        HandleMovementStates(movement);
        
        
        
        Current_State.UpdateState(Rigidbody_Main_Character, movement);
        
    }

    private void HandleInputs(Vector2 movement)
    {
        
        if (Input.GetKey(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow))
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
        
        
        
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Nearest = Find_Nearest_Interactable_Object_Within_Range(5);
            if (Inventory_Full == false && Nearest != null)
            {
                try
                {
                    Nearest.transform.parent.SendMessage("Pickup", gameObject, SendMessageOptions.DontRequireReceiver);
                    
                }
                catch
                {
                    //Do nothing
                }
            }
            else if (Inventory_Full == true && Nearest != null)
            {
                try
                {
                    Nearest.transform.parent.SendMessage("Drop_Off", gameObject, SendMessageOptions.DontRequireReceiver);
                    
                }
                catch
                {
                    //Do nothing
                }
            }
        }
    
        else if (Input.GetKeyDown(KeyCode.E))
        {
            Nearest = Find_Nearest_Interactable_Object_Within_Range(2);
            Nearest.transform.parent.SendMessage("Work", gameObject, SendMessageOptions.DontRequireReceiver);
        }
        else if(Input.GetKeyDown(KeyCode.Z))
        {
            try
            {
                Nearest = Find_Minecart(2);
                Nearest.transform.parent.SendMessage("Player_Enter", gameObject, SendMessageOptions.DontRequireReceiver);
                if(Nearest != null)
                {
                    if(Current_State == Riding_state)
                    {
                        SwitchState(Standing_State);
                    }
                    else
                    {
                        SwitchState(Riding_state);
                    }
                }
            }
            catch
            {
                //Do nothing
            }
            
        }
    }
    private void Cleanup() // Som nevnt, denne rydder opp i inventory for å forhindre bugs
    {
        Item_In_Inventory = null;
        Inventory_Full = false;
    }

    public GameObject Get_Item_In_Inventory()
    {
        return Item_In_Inventory;
    }

    public void Set_Item_In_Inventory(GameObject incoming_item)
    {
        Item_In_Inventory = incoming_item;
    }

    private void HandleMovementStates(Vector2 movement)
    {
        if (Item_In_Inventory == null)
        {
            if (movement != new Vector2(0, 0) && Current_State == Standing_State)
            {
                SwitchStateMovement(Moving_State, Rigidbody_Main_Character, movement);
            }
            else if (movement == new Vector2(0, 0) && Current_State == Moving_State)
            {
                SwitchStateMovement(Standing_State, Rigidbody_Main_Character, movement);
            }
        }
        else if (Item_In_Inventory != null)
        {
            if (movement != new Vector2(0, 0) && Current_State == Carrying_State)
            {
                SwitchStateMovement(Carrying_And_Moving, Rigidbody_Main_Character, movement);
            }
            else if (movement == new Vector2(0, 0) && Current_State == Carrying_And_Moving)
            {
                SwitchStateMovement(Carrying_State, Rigidbody_Main_Character, movement);
            }
        }
    }

    public void SwitchState(MainCharacterBaseState state)
    {
        Current_State = state;
        state.EnterState(this);
    }
    public void SwitchStateMovement(MainCharacterBaseState state, Rigidbody2D Rigidbody_Main_Character, Vector2 movement)
    {
        Current_State = state;
        state.EnterMovementState(this, Rigidbody_Main_Character, movement);
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
    GameObject Find_Minecart(int Range) //Her finner vi nærmeste objekt innenfor objekter med riktig tag, returnerer ett GameObject
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Minecart");
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
        
    public void Inventory_Full_Message(bool result)
    {
        if (result == true)
        {
            Inventory_Full = true;
            SwitchState(Carrying_State);
        }
        else
        {
            Cleanup();
            SwitchState(Standing_State);
        }
        print(result);
    }
    public int Get_Direction()
    {
        return last_direction;
    }
}
