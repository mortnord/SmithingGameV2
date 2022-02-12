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
}
