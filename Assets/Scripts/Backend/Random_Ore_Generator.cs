using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using static Enumtypes;

public class Random_Ore_Generator : MonoBehaviour
{
    // Start is called before the first frame update
    
    Tilemap map_to_generate_in = null;

    public Ore_Quality ore_quality;

    public GameObject dwarf = null;
    public RuleTile rock_tile;
    public Tile copper_tile;
    public Tile iron_tile;
    public Tile mithril_tile;
    public List<int> copper_spread = new List<int>();
    public List<int> iron_spread = new List<int>();
    public List<int> mithril_spread = new List<int>();

    System.Random rand = new System.Random(); 
    
    // Update is called once per frame
    void Update() //Her gjør vi ore synlig og usynlig basert på distansen mellom dwarf og ore. 
    {
       
    }

    void Awake()
    {
        copper_spread.Add(1);
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
    }
    void mapToGenerateIn(Tilemap Map_in) //Metode som kalles for å generate ore. 
    {
        map_to_generate_in = Map_in;
        generateMap(map_to_generate_in);
    }
    
    private void generateMap(Tilemap Map_in) //Vi har en dobbel for-loop som iterater over hele griddet, og vis terningen viser 0 1 eller 2 blir det generert malm
                                             //Vis ikke så blir det generert ingenting enn så leng
    {
        

        int seed_Copper = Random.Range(0, 9999);

        int seed_Iron = Random.Range(0, 9999);

        int seed_Mithril = Random.Range(0, 9999);
        float oreNoise;
        float xCoord;
        float yCoord;


        for (int i = Map_in.cellBounds.xMin; i < Map_in.cellBounds.xMax; i += 12)
        {
            for (int j = Map_in.cellBounds.yMin; j < Map_in.cellBounds.yMax; j += 12)
            {
                int ore_generate = Random.Range(0, 3);
                
                Ore_chunk_Bloom(ore_generate, Map_in, i, j);
                
            }
        }
        for (int i = Map_in.cellBounds.xMin; i < Map_in.cellBounds.xMax; i++)
        {
            for (int j = Map_in.cellBounds.yMin; j < Map_in.cellBounds.yMax; j++)
            {
                xCoord = (float)i / (Mathf.Abs(Map_in.cellBounds.xMin + Mathf.Abs(Map_in.cellBounds.xMax)));
                yCoord = (float)j / (Mathf.Abs(Map_in.cellBounds.yMin + Mathf.Abs(Map_in.cellBounds.yMax)));
                oreNoise = Mathf.PerlinNoise(xCoord + seed_Copper, yCoord + seed_Copper);
                
                if(oreNoise > 0.75f)
                {
                    Map_in.SetTile(new Vector3Int(i, j, 0), copper_tile);
                }
            }
        }
        for (int i = Map_in.cellBounds.xMin; i < Map_in.cellBounds.xMax; i++)
        {
            for (int j = Map_in.cellBounds.yMin; j < Map_in.cellBounds.yMax; j++)
            {
                xCoord = (float)i / (Mathf.Abs(Map_in.cellBounds.xMin + Mathf.Abs(Map_in.cellBounds.xMax)));
                yCoord = (float)j / (Mathf.Abs(Map_in.cellBounds.yMin + Mathf.Abs(Map_in.cellBounds.yMax)));
                oreNoise = Mathf.PerlinNoise(xCoord + seed_Iron, yCoord + seed_Iron);
                
                if (oreNoise > 0.80f)
                {
                    Map_in.SetTile(new Vector3Int(i, j, 0), iron_tile);
                }
            }
        }
        for (int i = Map_in.cellBounds.xMin; i < Map_in.cellBounds.xMax; i++)
        {
            for (int j = Map_in.cellBounds.yMin; j < Map_in.cellBounds.yMax; j++)
            {
                xCoord = (float)i / (Mathf.Abs(Map_in.cellBounds.xMin + Mathf.Abs(Map_in.cellBounds.xMax)));
                yCoord = (float)j / (Mathf.Abs(Map_in.cellBounds.yMin + Mathf.Abs(Map_in.cellBounds.yMax)));
                oreNoise = Mathf.PerlinNoise(xCoord + seed_Mithril, yCoord + seed_Mithril);
               
                if (oreNoise > 0.85f)
                {
                    Map_in.SetTile(new Vector3Int(i, j, 0), mithril_tile);
                }
            }
        }
        for (int i = Map_in.cellBounds.xMin; i < Map_in.cellBounds.xMax; i++)
        {
            for (int j = Map_in.cellBounds.xMin; j < Map_in.cellBounds.xMax; j++)
            {

                xCoord = (float)i / (Mathf.Abs(Map_in.cellBounds.xMin + Mathf.Abs(Map_in.cellBounds.xMax)));
                yCoord = (float)j / (Mathf.Abs(Map_in.cellBounds.yMin + Mathf.Abs(Map_in.cellBounds.yMax)));

                oreNoise = Mathf.PerlinNoise(xCoord + StaticData.seed_caves, yCoord + StaticData.seed_caves);
                
                if (oreNoise > 0.80f)
                {
                    Map_in.SetTile(new Vector3Int(i, j, 0), null);
                }
            }
        }
        Map_in.SetTile(new Vector3Int(-7, 4, 0), null);
        Map_in.SetTile(new Vector3Int(-6, 4, 0), null);
        Map_in.SetTile(new Vector3Int(-5, 4, 0), null);
        Map_in.SetTile(new Vector3Int(-4, 4, 0), null);

    }

    private void Ore_chunk_Bloom(int ore_generate, Tilemap map_in, int i, int j)
    {
        int spreadXplus;
        int spreadYplus;
        int spreadXminus;
        int spreadYminux;
        int randNr;
        if (ore_generate == 0)
        {
            randNr = rand.Next(0, copper_spread.Count);
            spreadXplus = copper_spread[randNr];
            randNr = rand.Next(0, copper_spread.Count);
            spreadYplus = copper_spread[randNr];
            randNr = rand.Next(0, copper_spread.Count);
            spreadXminus = copper_spread[randNr];
            randNr = rand.Next(0, copper_spread.Count);
            spreadYminux = copper_spread[randNr];
            map_in.SetTile(new Vector3Int(i, j, 0), copper_tile);
            calculateSpread(spreadXplus, spreadXminus, spreadYminux, spreadYplus, map_in, copper_tile, i, j);
            
        }
        else if(ore_generate == 1)
        {

            randNr = rand.Next(0, iron_spread.Count);
            spreadXplus = iron_spread[randNr];
            randNr = rand.Next(0, copper_spread.Count);
            spreadYplus = iron_spread[randNr];
            randNr = rand.Next(0, copper_spread.Count);
            spreadXminus = iron_spread[randNr];
            randNr = rand.Next(0, copper_spread.Count);
            spreadYminux = iron_spread[randNr];

            map_in.SetTile(new Vector3Int(i, j, 0), iron_tile);
            calculateSpread(spreadXplus, spreadXminus, spreadYminux, spreadYplus, map_in, iron_tile, i, j);
        }
        else if (ore_generate == 2)
        {

            randNr = rand.Next(0, mithril_spread.Count);
            spreadXplus = mithril_spread[randNr];
            randNr = rand.Next(0, copper_spread.Count);
            spreadYplus = mithril_spread[randNr];
            randNr = rand.Next(0, copper_spread.Count);
            spreadXminus = mithril_spread[randNr];
            randNr = rand.Next(0, copper_spread.Count);
            spreadYminux = mithril_spread[randNr];

            
            map_in.SetTile(new Vector3Int(i, j, 0), mithril_tile);
            calculateSpread(spreadXplus, spreadXminus, spreadYminux, spreadYplus, map_in, mithril_tile, i, j);
        }
        
    }

    private void calculateSpread(int spreadXplus, int spreadXminus, int spreadYminux, int spreadYplus, Tilemap map_in, Tile tile_spread, int i, int j)
    {
        int iterator = 0;
        while (spreadXplus > 0)
        {
            iterator++;
            map_in.SetTile(new Vector3Int(i + iterator, j, 0), tile_spread);
            spreadXplus--;
            if(spreadXplus > 0)
            {
                map_in.SetTile(new Vector3Int(i + iterator, j + 1, 0), tile_spread);
                map_in.SetTile(new Vector3Int(i + iterator, j - 1, 0), tile_spread);
            }
        }
        iterator = 0;
        while (spreadYplus > 0)
        {
            iterator++;
            map_in.SetTile(new Vector3Int(i, j + iterator, 0), tile_spread);
            spreadYplus--;
            if(spreadYplus > 0)
            {
                map_in.SetTile(new Vector3Int(i+ 1, j + iterator, 0), tile_spread);
                map_in.SetTile(new Vector3Int(i-1, j + iterator, 0), tile_spread);
            }
        }
        iterator = 0;
        while (spreadXminus > 0)
        {
            iterator--;
            map_in.SetTile(new Vector3Int(i + iterator, j, 0), tile_spread);
            spreadXminus--;
            if (spreadXminus > 0)
            {
                map_in.SetTile(new Vector3Int(i + iterator, j + 1, 0), tile_spread);
                map_in.SetTile(new Vector3Int(i + iterator, j -1, 0), tile_spread);
            }
        }
        iterator = 0;
        while (spreadYminux > 0)
        {
            iterator--;
            map_in.SetTile(new Vector3Int(i, j + iterator, 0), tile_spread);
            spreadYminux--;
            if (spreadYminux > 0)
            {
                map_in.SetTile(new Vector3Int(i + 1, j + iterator, 0), tile_spread);
                map_in.SetTile(new Vector3Int(i - 1, j + iterator, 0), tile_spread);
            }
        }
    }
}
