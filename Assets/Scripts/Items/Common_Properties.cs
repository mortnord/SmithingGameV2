using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Enumtypes;

public class Common_Properties : MonoBehaviour
{
    public int quality; //0 = low, 1 = normal, 2 = high
    public Object_Types object_tag;
    public Ore_Quality ore_quality;
    public Mission_Objects mission_tag;
    // Start is called before the first frame update
    void Start()
    {
        object_tag = Enumtypes.Object_Types.Ore;
        ore_quality = (Ore_Quality)quality;

    }
}
