using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trashcan : MonoBehaviour
{
    public GameObject Destroyable_Object = null;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Destroyable_Object != null) //vis objekt i inventory, slett.
        {
            Destroy(Destroyable_Object);
            Destroyable_Object = null;
        }
    }
}
