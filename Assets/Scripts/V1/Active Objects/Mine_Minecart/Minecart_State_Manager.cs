using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minecart_State_Manager : MonoBehaviour, IInteractor_Connector
{
    Rigidbody2D rb;
    Minecart_Base_State current_State;
    Minecart_Moving_State minecart_moving_state = new Minecart_Moving_State();
    Minecart_Stationary_State minecart_stationary_state = new Minecart_Stationary_State();
    public bool player_inside = false;

    public void Drop_Off(GameObject main_character)
    {
        current_State.Drop_Off(main_character);
    }

    public void Pickup(GameObject main_character)
    {
        current_State.Pickup(main_character);
    }

    public void Return_Answer(GameObject main_character, bool result)
    {
        current_State.Return_Answer(main_character, result);
    }

    // Start is called before the first frame update
    void Start()
    {
        current_State = minecart_stationary_state;
        rb = GetComponent<Rigidbody2D>(); // Denne trengs for å kunne gjøre physicsbasert movement
        current_State.EnterState(this, rb);
        rb.position = new Vector3(-4.5f, 4.7f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        current_State.UpdateState(this, rb);
    }

    void Player_Enter(GameObject main_character)
    {
        if(player_inside == false)
        {
            current_State = minecart_moving_state;
            current_State.EnterState(this, rb);
            player_inside = true;

            main_character.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 0);
            main_character.transform.parent = gameObject.transform;
            main_character.GetComponent<Rigidbody2D>().isKinematic = true;
        }
        else
        {
            main_character.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 0);
            main_character.GetComponent<Rigidbody2D>().isKinematic = false;
            main_character.transform.parent = null;
            current_State = minecart_stationary_state;
            current_State.EnterState(this, rb);
            player_inside = false;
            
        }
    }
}
