using UnityEngine;
using UnityEngine.SceneManagement;
public class Go_Down : MonoBehaviour, IInteract_Work
{
    Mission_System mission_system_object;
    public void Work(GameObject main_character) //Når E trykkes, så finner vi hovedGameobjektet, så calles storage på alt nedover
                                                //Dette gjør at all info lagres i staticData. Dette gjøres også
                                                // i mission objektet. og så er vi klar til å bytte scene
    {
        GameObject MainObject = GameObject.FindGameObjectWithTag("GameController");
        MainObject.BroadcastMessage("Storage", SendMessageOptions.DontRequireReceiver);
        mission_system_object = Find_Components.Find_Mission_System(); //Mission system objektet
        mission_system_object.Storage();
        SceneManager.LoadSceneAsync("Mining_Screen", LoadSceneMode.Single);
    }
    
}
