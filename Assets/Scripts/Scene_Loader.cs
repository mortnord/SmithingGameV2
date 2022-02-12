using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene_Loader : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject MainObject = GameObject.FindGameObjectWithTag("GameController");
        MainObject.BroadcastMessage("Loading", SendMessageOptions.DontRequireReceiver);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
