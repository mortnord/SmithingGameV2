using UnityEngine;

public abstract class MainCharacterBaseState 
{
    // Start is called before the first frame update
    public abstract void EnterState(MainCharacterStateManager main_character);

    public abstract void EnterMovementState(MainCharacterStateManager main_character, Rigidbody2D rb, Vector2 movement);

    public abstract void UpdateState(Rigidbody2D rb, Vector2 movement);
}
