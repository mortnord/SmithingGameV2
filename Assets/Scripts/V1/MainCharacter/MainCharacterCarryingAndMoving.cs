using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacterCarryingAndMoving : MainCharacterBaseState
{
    // Start is called before the first frame update
    public override void EnterState(MainCharacterStateManager main_character)
    {
        //Add Idle animation
        Debug.Log("Carrying and moving animation missing");
    }

    public override void UpdateState(Rigidbody2D rb, Vector2 movement)
    {
        rb.velocity = movement * 2;
    }
    public override void EnterMovementState(MainCharacterStateManager main_character, Rigidbody2D rb, Vector2 movement)
    {
        //Add Idle animation
        Debug.Log("Carrying and moving animation missing");
        rb.velocity = movement * 2;
    }
}
