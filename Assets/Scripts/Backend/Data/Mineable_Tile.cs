using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class Mineable_Tile
{
    public Tile tile;
    public RuleTile rule;
    public int percent_ore_quality;
    public Mineable_Tile(Tile tile, int Percent_ore_quality)
    {
        this.tile = tile;
        this.percent_ore_quality = Percent_ore_quality;
    }
    public Mineable_Tile(RuleTile rule, int Percent_ore_quality)
    {
        this.rule = rule;
        this.percent_ore_quality = Percent_ore_quality;
    }
}
