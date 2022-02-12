using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Go_Down : MonoBehaviour, IInteract_Work
{

    Mission_System mission_system_object;
    public void Work(GameObject main_character)
    {
        GameObject MainObject = GameObject.FindGameObjectWithTag("GameController");
        MainObject.BroadcastMessage("Storage", SendMessageOptions.DontRequireReceiver);
        mission_system_object = Find_Components.find_mission_system(); //Mission system objektet
        mission_system_object.Storage();
        SceneManager.LoadSceneAsync("Mining_Screen", LoadSceneMode.Single);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
