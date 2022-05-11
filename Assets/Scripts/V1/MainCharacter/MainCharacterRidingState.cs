using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacterRidingState : MainCharacterBaseState
{
    public override void EnterMovementState(MainCharacterStateManager main_character, Rigidbody2D rb, Vector2 movement)
    {
        //Add Idle animation
        Debug.Log("Riding animation missing");
        rb.velocity = new Vector2(0, 0);
    }

    public override void EnterState(MainCharacterStateManager main_character)
    {
        Debug.Log("In riding state");
    }

    public override void UpdateState(Rigidbody2D rb, Vector2 movement)
    {
        rb.velocity = movement * 0;
    }
}
