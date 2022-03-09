using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Initialization : MonoBehaviour 
{
    private void Update()
    {
        SceneManager.LoadSceneAsync("Smithing_Screen", LoadSceneMode.Single);
        print("Loading");
    }
}

