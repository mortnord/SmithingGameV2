using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene_Transition_Script : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F1))
        {
            SceneManager.LoadSceneAsync("Smithing_Screen", LoadSceneMode.Single);
        }
        if (Input.GetKeyDown(KeyCode.F2))
        {
            SceneManager.LoadSceneAsync("Mining_Screen", LoadSceneMode.Single);
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadSceneAsync("Home_Screen", LoadSceneMode.Single);
        }
    }
}
