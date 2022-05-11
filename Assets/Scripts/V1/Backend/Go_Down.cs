using UnityEngine;
using UnityEngine.SceneManagement;
public class Go_Down : MonoBehaviour, IInteract_Work
{
    Mission_System mission_system_object;
    public void Work(GameObject main_character) //N�r E trykkes, s� finner vi hovedGameobjektet, s� calles storage p� alt nedover
                                                //Dette gj�r at all info lagres i staticData. Dette gj�res ogs�
                                                // i mission objektet. og s� er vi klar til � bytte scene
    {
        GameObject MainObject = GameObject.FindGameObjectWithTag("GameController");
        MainObject.BroadcastMessage("Storage", SendMessageOptions.DontRequireReceiver);
        mission_system_object = Find_Components.Find_Mission_System(); //Mission system objektet
        mission_system_object.Storage();
        SceneManager.LoadSceneAsync("Mining_Screen", LoadSceneMode.Single);
    }
    
}
