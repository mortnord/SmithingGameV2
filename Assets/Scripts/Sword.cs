using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public int value;
    // Start is called before the first frame update

    public int quality; //0 = low, 1 = normal, 2 = high
    void Start()
    {
        value = (100 * quality) + 100;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
