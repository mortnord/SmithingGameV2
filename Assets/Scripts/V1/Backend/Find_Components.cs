using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Find_Components : MonoBehaviour
{
    //Her er alt tilsvarnede likt, s? jeg forklarer kun en. Alle gj?re egentlig det samme,
    //bare med en forskjellig parameter i string og i navn, kanskje det kan sl?s sammen til en?
    public static TimerScript Find_Timer_Object()
    {
        GameObject timer_object = GameObject.Find("TimerText"); //Vi finner ett gameobject basert p? navnet, dette krever at hver av objektene vi s?ker opp
                                                                //har unike navn, kanskje noe ? endre p? i framtiden
        TimerScript timer_script_object = timer_object.GetComponent<TimerScript>(); // Her finner vi script-objektet til gameObjektet, det vi er ute etter
        return timer_script_object; //Returner script-objektet.
    }
    public static Object_Creation Find_Object_Creation()
    {
        GameObject Object_Creation = GameObject.Find("Object_Creation");
        Object_Creation Object_Creation_Object = Object_Creation.GetComponent<Object_Creation>();
        return Object_Creation_Object;
    }
    public static Ingot_Form Find_Ingot_Form()
    {
        GameObject ingot_form = GameObject.Find("Ingot_form");
        Ingot_Form ingot_form_object = ingot_form.GetComponent<Ingot_Form>();
        return ingot_form_object;
    }
    public static Furnace Find_Furnace()
    {
        GameObject furnace = GameObject.Find("Furnace");
        Furnace furnace_object = furnace.GetComponent<Furnace>();
        return furnace_object;
    }
    public static Mission_System Find_Mission_System()
    {
        GameObject mission_system = GameObject.Find("Mission_System");
        Mission_System mission_system_object = mission_system.GetComponent<Mission_System>();
        return mission_system_object;
    }
}
