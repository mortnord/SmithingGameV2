using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Find_Components : MonoBehaviour
{
    public static TimerScript find_Timer_Object()
    {
        GameObject timer_object = GameObject.Find("TimerText");

        TimerScript timer_script_object = timer_object.GetComponent<TimerScript>();
        return timer_script_object;
    }
    public static Object_Creation find_Object_Creation()
    {
        GameObject Object_Creation = GameObject.Find("Object_Creation");

        Object_Creation Object_Creation_Object = Object_Creation.GetComponent<Object_Creation>();
        return Object_Creation_Object;
    }
    public static Unsorted_Ore_container find_Unsorted_Tray()
    {
        GameObject Unsorted_Tray = GameObject.Find("Unsorted_Ore_Tray");
        Unsorted_Ore_container Unsorted_Tray_Object = Unsorted_Tray.GetComponent<Unsorted_Ore_container>();
        return Unsorted_Tray_Object;
    }
    public static Sorted_Ore_Tray find_Sorted_Tray_Low()
    {
        GameObject Sorted_Ore_Tray = GameObject.Find("Sorted_Ore_Tray_Low");
        Sorted_Ore_Tray Sorted_Ore_Tray_Object = Sorted_Ore_Tray.GetComponent<Sorted_Ore_Tray>();
        return Sorted_Ore_Tray_Object;
    }
}
