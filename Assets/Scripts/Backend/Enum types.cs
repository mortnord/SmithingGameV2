using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enumtypes : MonoBehaviour
{
    // Start is called before the first frame update
    public enum Ore_Quality //Enum type av materiale
    {
        Copper,
        Iron,
        Mithril
    }
    public enum Object_Types //Enum type objekt
    {
        Ore,
        Ingot,
        Sword
    }
    public enum Mission_Objects //Enum type mission objekter
    {
        Sword
    }
}
