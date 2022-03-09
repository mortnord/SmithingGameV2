using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using static Enumtypes;

public class Random_Ore_Generator : MonoBehaviour
{
    // Start is called before the first frame update

    public Tilemap map_to_generate_in;

    public Ore_Quality ore_quality;

    public RuleTile rock_tile;
    public Tile copper_tile;
    public Tile iron_tile;
    public Tile mithril_tile;
    public List<int> copper_spread = new List<int>();
    public List<int> iron_spread = new List<int>();
    public List<int> mithril_spread = new List<int>();

    System.Random rand = new System.Random();


    void Awake()
    {
        copper_spread.Add(1); //Her legger vi til de random verdiene vi har, finn en bedre måte...
        copper_spread.Add(1);
        copper_spread.Add(2);
        copper_spread.Add(2);
        copper_spread.Add(4);

        iron_spread.Add(1);
        iron_spread.Add(1);
        iron_spread.Add(1);
        iron_spread.Add(2);
        iron_spread.Add(3);

        mithril_spread.Add(0);
        mithril_spread.Add(1);
        mithril_spread.Add(1);
        mithril_spread.Add(1);
        mithril_spread.Add(2);
        generateMap();
       
    }

    private void generateMap()
    {
        for (int i = -StaticData.map_size_x; i < StaticData.map_size_x; i++)
        {
            for (int j = -StaticData.map_size_y; j < StaticData.map_size_y; j++)
            {
                StaticData.Digging_Map_information.Add(new Vector3Int(i, j, 0), new Mineable_Tile(rock_tile, 0));
            }
        }
        for (int i = -StaticData.map_size_x; i < StaticData.map_size_x; i += 12)
        {
            for (int j = -StaticData.map_size_y; j < StaticData.map_size_y; j += 12)
            {
                int ore_generate = UnityEngine.Random.Range(0, 3); //En av copper, iron eller mithril
                int rnd_seed = rand.Next(100, 301);
                print(rnd_seed);
                Ore_chunk_Bloom(ore_generate, i, j, rnd_seed); //Hvor denne bloomen skal generates
            }
        }

        PerlinNoiseMap(copper_tile, StaticData.seed_Copper, 0.80f); //Her henter vi info fra vårt perlin noise map med seed for randomness.
        PerlinNoiseMap(iron_tile, StaticData.seed_Iron, 0.80f);
        PerlinNoiseMap(mithril_tile, StaticData.seed_Mithril, 0.85f);
        PerlinNoiseMap(StaticData.seed_caves, 0.60f);
    }

    private void Ore_chunk_Bloom(int ore_generate, int i, int j, int Percent_ore_quality)
    {
        int spreadXplus;
        int spreadYplus;
        int spreadXminus;
        int spreadYminux;
        int randNr;

        if (ore_generate == 0) //Hvis copper, bruk copper sine sprednings verdier
        {
            randNr = rand.Next(0, copper_spread.Count);
            spreadXplus = copper_spread[randNr];
            randNr = rand.Next(0, copper_spread.Count);
            spreadYplus = copper_spread[randNr];
            randNr = rand.Next(0, copper_spread.Count);
            spreadXminus = copper_spread[randNr];
            randNr = rand.Next(0, copper_spread.Count);
            spreadYminux = copper_spread[randNr];
            StaticData.Digging_Map_information[(new Vector3Int(i, j, 0))] = new Mineable_Tile(copper_tile, Percent_ore_quality); //Først sett den initielle blokken imidten
            calculateSpread(spreadXplus, spreadXminus, spreadYminux, spreadYplus, copper_tile, i, j, Percent_ore_quality); //Beregn hvor langt ut til hver siden den skal spre.

        }

        else if (ore_generate == 1) //Iron
        {

            randNr = rand.Next(0, iron_spread.Count);
            spreadXplus = iron_spread[randNr];
            randNr = rand.Next(0, copper_spread.Count);
            spreadYplus = iron_spread[randNr];
            randNr = rand.Next(0, copper_spread.Count);
            spreadXminus = iron_spread[randNr];
            randNr = rand.Next(0, copper_spread.Count);
            spreadYminux = iron_spread[randNr];

            StaticData.Digging_Map_information[(new Vector3Int(i, j, 0))] = new Mineable_Tile(iron_tile, Percent_ore_quality);
            calculateSpread(spreadXplus, spreadXminus, spreadYminux, spreadYplus, iron_tile, i, j, Percent_ore_quality);
        }
        else if (ore_generate == 2) //Mithril
        {

            randNr = rand.Next(0, mithril_spread.Count);
            spreadXplus = mithril_spread[randNr];
            randNr = rand.Next(0, copper_spread.Count);
            spreadYplus = mithril_spread[randNr];
            randNr = rand.Next(0, copper_spread.Count);
            spreadXminus = mithril_spread[randNr];
            randNr = rand.Next(0, copper_spread.Count);
            spreadYminux = mithril_spread[randNr];


            StaticData.Digging_Map_information[(new Vector3Int(i, j, 0))] = new Mineable_Tile(mithril_tile, Percent_ore_quality);
            calculateSpread(spreadXplus, spreadXminus, spreadYminux, spreadYplus, mithril_tile, i, j,Percent_ore_quality);
        }
    }

    private void PerlinNoiseMap(int seed_caves, float v)
    {
        float oreNoise;
        float xCoord;
        float yCoord;
        for (int i = -StaticData.map_size_x; i < StaticData.map_size_x; i++) //Iterate over hele tilemappet
        {
            for (int j = -StaticData.map_size_y; j < StaticData.map_size_y; j++)
            {
                //(Mathf.Abs(map_in.cellBounds.xMin + Mathf.Abs(map_in.cellBounds.xMax)));
                xCoord = (float)i / (Mathf.Abs(-StaticData.map_size_x) + Mathf.Abs(StaticData.map_size_x))*40; //Fancy pancy matte eg ikke skjønner lengre... Men det funker
                yCoord = (float)j / (Mathf.Abs(-StaticData.map_size_y) + Mathf.Abs(StaticData.map_size_y))*40;

                oreNoise = Mathf.PerlinNoise(xCoord + seed_caves, yCoord + seed_caves); //Få en perlin noise map verdi

                if (oreNoise > v && oreNoise < 0.70f) //Vis høy nok, 
                {
                    StaticData.Digging_Map_information[new Vector3Int(i, j, 0)] =  null; //Set tile til null, ala lage en cavern
                }
            }
        }
    }

    private void PerlinNoiseMap(Tile tile, int seed, float v) //Se andre metode
    {

        float oreNoise;
        float xCoord;
        float yCoord;
        for (int i = -StaticData.map_size_x; i < StaticData.map_size_x; i++)
        {
            for (int j = -StaticData.map_size_y; j < StaticData.map_size_y; j++)
            {
                xCoord = (float)i / (Mathf.Abs(-StaticData.map_size_x) + Mathf.Abs(StaticData.map_size_x))*10;
                yCoord = (float)j / (Mathf.Abs(-StaticData.map_size_y) + Mathf.Abs(StaticData.map_size_y))*10;
                oreNoise = Mathf.PerlinNoise(xCoord + seed, yCoord + seed);
                if (oreNoise > v)
                {
                    StaticData.Digging_Map_information[new Vector3Int(i, j, 0)] = new Mineable_Tile(tile, 150); //Spre malm godsaker rundt om kring.
                }
            }
        }
    }
        
        
    private void calculateSpread(int spreadXplus, int spreadXminus, int spreadYminux, int spreadYplus, Tile tile_spread, int i, int j, int Percent_ore_quality)
    {
        int iterator = 0; //Iterator for hvor langt ut vi har dratt fra sentrum
        while (spreadXplus > 0) //så lenge vi skal utover
        {
            iterator++; //Øk distansen ut
            StaticData.Digging_Map_information[new Vector3Int(i + iterator, j, 0)] = new Mineable_Tile(tile_spread, Percent_ore_quality); //Set tilen til malmen
            spreadXplus--; //En mindre spredning
            if (spreadXplus > 0) //Hvis fortsatt pluss, sett en malm opp og ned for tilen, blir mer organisk då
            {
                StaticData.Digging_Map_information[new Vector3Int(i + iterator, j + 1, 0)] = new Mineable_Tile(tile_spread, Percent_ore_quality);
                StaticData.Digging_Map_information[new Vector3Int(i + iterator, j - 1, 0)] = new Mineable_Tile(tile_spread, Percent_ore_quality);
            }
        }
        iterator = 0;
        while (spreadYplus > 0)
        {
            iterator++;
            StaticData.Digging_Map_information[new Vector3Int(i, j + iterator, 0)] =  new Mineable_Tile(tile_spread, Percent_ore_quality);
            spreadYplus--;
            if(spreadYplus > 0)
            {
                StaticData.Digging_Map_information[new Vector3Int(i+ 1, j + iterator, 0)] = new Mineable_Tile(tile_spread, Percent_ore_quality);
                StaticData.Digging_Map_information[new Vector3Int(i-1, j + iterator, 0)] =  new Mineable_Tile(tile_spread, Percent_ore_quality);
            }
        }
        iterator = 0;
        while (spreadXminus > 0)
        {
            iterator--;
            StaticData.Digging_Map_information[new Vector3Int(i + iterator, j, 0)]= new Mineable_Tile(tile_spread, Percent_ore_quality);
            spreadXminus--;
            if (spreadXminus > 0)
            {
                StaticData.Digging_Map_information[new Vector3Int(i + iterator, j + 1, 0)]= new Mineable_Tile(tile_spread, Percent_ore_quality);
                StaticData.Digging_Map_information[new Vector3Int(i + iterator, j -1, 0)]= new Mineable_Tile(tile_spread, Percent_ore_quality);
            }
        }
        iterator = 0;
        while (spreadYminux > 0)
        {
            iterator--;
            StaticData.Digging_Map_information[new Vector3Int(i, j + iterator, 0)]= new Mineable_Tile(tile_spread, Percent_ore_quality);
            spreadYminux--;
            if (spreadYminux > 0)
            {
                StaticData.Digging_Map_information[new Vector3Int(i + 1, j + iterator, 0)]= new Mineable_Tile(tile_spread, Percent_ore_quality);
                StaticData.Digging_Map_information[new Vector3Int(i - 1, j + iterator, 0)]= new Mineable_Tile(tile_spread, Percent_ore_quality);
            }
        }
    }
}

