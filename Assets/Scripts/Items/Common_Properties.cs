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
    public Object_Types Get_Object_Tag()
    {
        return object_tag;
    }
    public void Set_Object_Tag(Object_Types object_type_inn)
    {
        object_tag = object_type_inn;
    }
    public Ore_Quality Get_Ore_Quality()
    {
        return ore_quality;
    }
    public void Set_Quality(int value_inn)
    {
        quality = value_inn;
    }
    public int Get_Quality()
    {
        return quality;
    }
    public Mission_Objects Get_Mission_Tag()
    {
        return mission_tag;
    }
    public void Set_Mission_Tag(Mission_Objects mission_type_inn)
    {
        mission_tag = mission_type_inn;
    }
}
