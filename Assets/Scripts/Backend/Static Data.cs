using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StaticData 
{
    public static List<Enumtypes.Ore_Quality> Transition_Ores = new List<Enumtypes.Ore_Quality>();
    public static int score;
    public static float time_Remaining = 720;
    public static List<List<Enumtypes.Ore_Quality>> Ore_Quality = new List<List<Enumtypes.Ore_Quality>>();
    public static List<List<Enumtypes.Ore_Quality>> Ingot_Quality = new List<List<Enumtypes.Ore_Quality>>();

    public static List<float> Time_remaining_static_data = new List<float>();
    public static List<int> type_of_object_for_mission_static_data = new List<int>();
    public static List<int> quality_of_object_for_mission_static_data = new List<int>();
    public static List<float> Time_for_mission_static_data = new List<float>();
    public static List<float> x_position = new List<float>();
    public static List<float> y_position = new List<float>();

}
