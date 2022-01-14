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
    public static Sorted_Ore_Tray find_Sorted_Tray_Medium()
    {
        GameObject Sorted_Ore_Tray = GameObject.Find("Sorted_Ore_Tray_Medium");
        Sorted_Ore_Tray Sorted_Ore_Tray_Object = Sorted_Ore_Tray.GetComponent<Sorted_Ore_Tray>();
        return Sorted_Ore_Tray_Object;
    }
    public static Sorted_Ore_Tray find_Sorted_Tray_High()
    {
        GameObject Sorted_Ore_Tray = GameObject.Find("Sorted_Ore_Tray_High");
        Sorted_Ore_Tray Sorted_Ore_Tray_Object = Sorted_Ore_Tray.GetComponent<Sorted_Ore_Tray>();
        return Sorted_Ore_Tray_Object;
    }
    public static Furnace find_furnace()
    {
        GameObject Furnace = GameObject.Find("Furnace");
        Furnace Furnace_Object = Furnace.GetComponent<Furnace>();
        return Furnace_Object;
    }
    public static Ingot_Form find_ingot_form()
    {
        GameObject ingot_form = GameObject.Find("Ingot_form");
        Ingot_Form ingot_form_object = ingot_form.GetComponent<Ingot_Form>();
        return ingot_form_object;
    }
    public static Sorted_Ingots_Tray find_ingot_tray_low()
    {
        GameObject ingot_tray = GameObject.Find("Ingot_Tray_Low");
        Sorted_Ingots_Tray ingot_tray_object = ingot_tray.GetComponent<Sorted_Ingots_Tray>();
        return ingot_tray_object;

    }
    public static Sorted_Ingots_Tray find_ingot_tray_medium()
    {
        GameObject ingot_tray = GameObject.Find("Ingot_Tray_Medium");
        Sorted_Ingots_Tray ingot_tray_object = ingot_tray.GetComponent<Sorted_Ingots_Tray>();
        return ingot_tray_object;
    }
    public static Sorted_Ingots_Tray find_ingot_tray_high()
    {
        GameObject ingot_tray = GameObject.Find("Ingot_Tray_High");
        Sorted_Ingots_Tray ingot_tray_object = ingot_tray.GetComponent<Sorted_Ingots_Tray>();
        return ingot_tray_object;
    }
}
