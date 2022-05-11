using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacterCarryingState : MainCharacterBaseState
{
    public override void EnterMovementState(MainCharacterStateManager main_character, Rigidbody2D rb, Vector2 movement)
    {
        //Add Idle animation
        Debug.Log("Carrying animation missing");
        rb.velocity = new Vector2(0, 0);
    }

    // Start is called before the first frame update
    public override void EnterState(MainCharacterStateManager main_character)
    {
        //Add Idle animation
        Debug.Log("Carrying animation missing");
    }

    public override void UpdateState(Rigidbody2D rb, Vector2 movement)
    {
        rb.velocity = movement * 0;
    }
}
