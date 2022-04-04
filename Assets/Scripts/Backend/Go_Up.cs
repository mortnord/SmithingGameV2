using UnityEngine;
using UnityEngine.SceneManagement;
public class Go_Up : MonoBehaviour, IInteract_Work
{
    public void Work(GameObject main_character) //Her ska vi kun bytte scene opp. 
    {
        SceneManager.LoadSceneAsync("Smithing_Screen", LoadSceneMode.Single);
    }
    
}
