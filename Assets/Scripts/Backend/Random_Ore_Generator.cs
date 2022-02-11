using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Enumtypes;
using UnityEngine.Tilemaps;

public class Random_Ore_Generator : MonoBehaviour
{
    // Start is called before the first frame update
    public List<GameObject> List_of_ores = new List<GameObject>();
    Tilemap map_to_generate_in = null;

    System.Random rand = new System.Random();
    int randnr = 0;
    public Object_Creation Generation_Object;
    public Ore_Quality ore_quality;

    public GameObject dwarf = null;


    void Start()
    {
        Generation_Object = Find_Components.find_Object_Creation();
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < List_of_ores.Count; i++)
        {
            if (Vector3.Distance(dwarf.transform.position, List_of_ores[i].transform.position) > 5)
            {
                List_of_ores[i].SetActive(false);
            }
            else
            {
                List_of_ores[i].SetActive(true);
            }
        }
    }
    void mapToGenerateIn(Tilemap Map_in)
    {
        map_to_generate_in = Map_in;
        generateMap(map_to_generate_in);
    }

    private void generateMap(Tilemap Map_in)
    {
        for (int i = Map_in.cellBounds.xMin; i < Map_in.cellBounds.xMax; i++) 
        {
            for (int j = Map_in.cellBounds.yMin; j < Map_in.cellBounds.yMax; j++)
            {
                randnr = rand.Next(40);
                if(randnr == 0)
                {
                    List_of_ores.Add(Generation_Object.create_ore(i, j, (int)Ore_Quality.Copper));
                }
                else if (randnr == 1)
                {
                    List_of_ores.Add(Generation_Object.create_ore(i, j, (int)Ore_Quality.Iron));
                }
                else if (randnr == 2)
                {
                    List_of_ores.Add(Generation_Object.create_ore(i, j, (int)Ore_Quality.Mithril));
                }
            }
        }
    }
}
