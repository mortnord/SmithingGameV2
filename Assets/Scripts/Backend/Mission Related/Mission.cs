using UnityEngine;
public class Mission : MonoBehaviour
{
    public float Time_remaining = 0;
    public int type_of_object_for_mission = 0;
    public int quality_of_object_for_mission = 0;
    public float time_for_mission = 0;
    public SpriteRenderer spriteRenderer;
    public void Set_Time(float time_remaining)
    {
        Time_remaining = time_remaining; //Tiden vi har igjen
        time_for_mission = Time_remaining - 180; //Rewrite all this shit. 
        print("i am alive");
    }
    public void Set_Quality_Of_Object_For_Mission(int value_inn)
    {
        quality_of_object_for_mission = value_inn;
    }
    public void Set_Type_Of_Object_For_Mission(int value_inn)
    {
        type_of_object_for_mission = value_inn;
    }
    public int Get_Quality_Of_Object_For_Mission()
    {
        return quality_of_object_for_mission;
    }
    public int Get_Type_Of_Object_For_Mission()
    {
        return type_of_object_for_mission;
    }
    public float Get_Time_Remaining()
    {
        return Time_remaining;
    }
}
