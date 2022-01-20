using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Find_Components : MonoBehaviour
{
    //Her er alt tilsvarnede likt, så jeg forklarer kun en. Alle gjøre egentlig det samme,
    //bare med en forskjellig parameter i string og i navn, kanskje det kan slås sammen til en?
    public static TimerScript find_Timer_Object()
    {
        GameObject timer_object = GameObject.Find("TimerText"); //Vi finner ett gameobject basert på navnet, dette krever at hver av objektene vi søker opp
                                                                //har unike navn, kanskje noe å endre på i framtiden
        TimerScript timer_script_object = timer_object.GetComponent<TimerScript>(); // Her finner vi script-objektet til gameObjektet, det vi er ute etter
        return timer_script_object; //Returner script-objektet.
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
    public static Anvil find_anvil()
    {
        GameObject anvil_GameObject = GameObject.Find("Anvil");
        Anvil anvil_object = anvil_GameObject.GetComponent<Anvil>();
        return anvil_object;
    }

    public static Export_Chute find_export_chute()
    {
        GameObject export_chute_GameObject = GameObject.Find("Export_Chute");
        Export_Chute export_chute_object = export_chute_GameObject.GetComponent<Export_Chute>();
        return export_chute_object;
    }
    public static Score find_score()
    {
        GameObject score_GameObject = GameObject.Find("Score");
        Score score_object = score_GameObject.GetComponent<Score>();
        return score_object;
    }
    public static Mission_System find_mission_system()
    {
        GameObject mission_system = GameObject.Find("Mission_System");
        Mission_System mission_system_object = mission_system.GetComponent<Mission_System>();
        return mission_system_object;
    }

}
