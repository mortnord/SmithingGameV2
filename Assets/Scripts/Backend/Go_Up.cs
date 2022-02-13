using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Go_Up : MonoBehaviour, IInteract_Work
{
    public void Work(GameObject main_character) //Her ska vi kun bytte scene opp. 
    {
        SceneManager.LoadSceneAsync("Smithing_Screen", LoadSceneMode.Single);
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
