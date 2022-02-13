using UnityEngine;


interface IInteractor_Connector //Interface metoder for å plukke opp og legge fra seg ting + svar. 
{
    public void Pickup(GameObject main_character);
    public void Drop_Off(GameObject main_character);

    public void Return_Answer(GameObject main_character, bool result);
}
interface IInteract_Work //Interface metode for "arbeid"
{
    public void Work(GameObject main_character);
}
interface IIData_transfer //Interface metode for lagring og lasting av data. 
{
    public void Storage();
    public void Loading();
}


