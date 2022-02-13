using UnityEngine;

public class Scene_Loader : MonoBehaviour
{
    // Start is called before the first frame update
    void Start() //Vi laster inn når scenen blir loada. 
    {
        GameObject MainObject = GameObject.FindGameObjectWithTag("GameController");
        MainObject.BroadcastMessage("Loading", SendMessageOptions.DontRequireReceiver);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
