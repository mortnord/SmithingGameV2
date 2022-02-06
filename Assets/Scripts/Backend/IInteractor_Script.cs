using System.Collections;
using System.Collections.Generic;
using UnityEngine;


interface IInteractor_Connector
{
    public void Pickup(GameObject main_character);
    public void Drop_Off(GameObject main_character);

    public void Return_Answer(GameObject main_character, bool result);
}
interface IInteract_Work
{
    public void Work(GameObject main_character);
}
    
