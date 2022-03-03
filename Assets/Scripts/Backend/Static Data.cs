using System.Collections.Generic;
using UnityEngine;

public static class StaticData //Her har vi static data som lagres mellom scenes. 
{
    //UI
    public static int score = 0;
    public static float time_Remaining = 720;

    public static List<Enumtypes.Ore_Quality> Transition_Ores = new List<Enumtypes.Ore_Quality>();
    public static List<List<Enumtypes.Ore_Quality>> Ore_Quality = new List<List<Enumtypes.Ore_Quality>>();
    public static List<List<Enumtypes.Ore_Quality>> Ingot_Quality = new List<List<Enumtypes.Ore_Quality>>();


    //Missions
    public static List<float> Time_remaining_static_data = new List<float>();
    public static List<int> type_of_object_for_mission_static_data = new List<int>();
    public static List<int> quality_of_object_for_mission_static_data = new List<int>();
    public static List<float> Time_for_mission_static_data = new List<float>();
    public static List<float> x_position_mission = new List<float>();
    public static List<float> y_position_mission = new List<float>();

    //Furnace
    public static float smelting_time_static;
    public static int smelting_input_static;
    public static List<Enumtypes.Ore_Quality> furnace_quality_static_object = new List<Enumtypes.Ore_Quality>();

    //Export Chute
    public static List<Enumtypes.Ore_Quality> export_chute_quality_static = new List<Enumtypes.Ore_Quality>();
    public static List<Enumtypes.Object_Types> export_chute_object_static = new List<Enumtypes.Object_Types>();
    public static List<float> x_position_export_chute = new List<float>();
    public static List<float> y_position_export_chute = new List<float>();

    //Energy for Mining
    public static int Energy_mining_static = 20;
    public static int Amount_of_energy_objects = 0;

    //Amount of Beers
    public static int amount_of_beer_static = 0;
    public static int Energy_value_beer = 5;

    //Map Generattion
    public static int seed_caves = Random.Range(0, 9999);
    public static int seed_Copper = Random.Range(0, 9999);
    public static int seed_Iron = Random.Range(0, 9999);
    public static int seed_Mithril = Random.Range(0, 9999);
    public static int size_chunk = 16;
    public static int size_total = size_chunk * size_chunk;
    public static int size_negative = -size_total;

}
