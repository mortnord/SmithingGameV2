using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minecart_Moving_State : Minecart_Base_State
{
    Vector3 temp_position;
    public override void Drop_Off(GameObject main_character)
    {
        //DO nothing
    }

    public override void EnterState(Minecart_State_Manager minecart, Rigidbody2D rb)
    {
       
    }

    public override void Pickup(GameObject main_character)
    {
        //DO nothing
    }

    public override void Return_Answer(GameObject main_character, bool result)
    {
        //DO nothing
    }

    public override void UpdateState(Minecart_State_Manager minecart, Rigidbody2D rb)
    {
        if(Input.anyKey)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                temp_position = new Vector3(rb.position.x - 1, rb.position.y, 0);
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                temp_position = new Vector3(rb.position.x + 1, rb.position.y, 0);
            }
            else if (Input.GetKeyDown(KeyCode.W))
            {
                temp_position = new Vector3(rb.position.x, rb.position.y + 1, 0);
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                temp_position = new Vector3(rb.position.x, rb.position.y - 1, 0);
            }
            if (CheckLegalPosition(temp_position))
            {
                rb.position = temp_position;
            }
        }
        
        
    }

    private bool CheckLegalPosition(Vector3 temp_position)
    {
        Vector3Int Corrected_temp_position = Vector3Int.FloorToInt(temp_position);
        if (StaticData.track_positions.Contains(Corrected_temp_position))
        {
            Debug.Log("Exists");
            return true;
        }
        else
        {
            Debug.Log("OFFROAD");
            return false;
        }
    }
}
