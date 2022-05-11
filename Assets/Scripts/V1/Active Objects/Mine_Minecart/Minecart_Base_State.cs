using System.Collections.Generic;
using UnityEngine;

public abstract class Minecart_Base_State : IInteractor_Connector
{
    public List<GameObject> Ores_in_tray = new List<GameObject>();

    public abstract void Drop_Off(GameObject main_character);
    public abstract void EnterState(Minecart_State_Manager minecart, Rigidbody2D rb);
    public abstract void Pickup(GameObject main_character);
    public abstract void Return_Answer(GameObject main_character, bool result);
    public abstract void UpdateState(Minecart_State_Manager minecart, Rigidbody2D rb);
}
