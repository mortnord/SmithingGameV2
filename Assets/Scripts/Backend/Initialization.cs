using UnityEngine;
using UnityEngine.SceneManagement;
public class Initialization : MonoBehaviour 
{
    private void Start()
    {
        Application.targetFrameRate = 60;
    }
    private void Update()
    {
        SceneManager.LoadSceneAsync("Smithing_Screen", LoadSceneMode.Single);
        print("Loading");
    }
}
