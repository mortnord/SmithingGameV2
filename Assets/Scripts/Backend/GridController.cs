using System;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Tilemaps;


public class GridController : MonoBehaviour
{

    public Object_Creation Generation_Object;
    private Grid grid;
    public Tilemap backgroundmap = null;
    public Tilemap rock_map = null;
    public Tilemap shade_map = null;
    public Tile mouse_tile = null;

    public Tile copper_tile;
    public Tile iron_tile;
    public Tile mithril_tile;
    public RuleTile mined_tile = null;
    public GameObject dwarf = null;
    public GameObject UI = null;
    DwarfScript dwarfScript = null;
    public Mineable_Tile mine_tile;
    

    private Vector3Int prev_mouse_pos = new Vector3Int();
    private Vector3Int target_pos;

    public List<Mineable_Tile> results = new List<Mineable_Tile>();
    // Start is called before the first frame update
    void Start() //Vi finner nødvendige objekter, og gir beskjed til rock mappet om å generate ore. 
    {
        grid = gameObject.GetComponent<Grid>();
        
        dwarfScript = dwarf.GetComponent<DwarfScript>();
        LoadMap();
        Generation_Object = Find_Components.find_Object_Creation();
    }

    private void LoadMap()
    {
        for (int i = -StaticData.map_size_x; i < StaticData.map_size_x; i++)
        {
            for (int j = -StaticData.map_size_y; j < StaticData.map_size_y; j++)
            {
                mine_tile = StaticData.Digging_Map_information[new Vector3Int(i, j, 0)];
                if(mine_tile != null)
                {
                    if (mine_tile.rule != null)
                    {
                        rock_map.SetTile(new Vector3Int(i, j, 0), mine_tile.rule);
                    }
                    else if (mine_tile.tile != null)
                    {
                        rock_map.SetTile(new Vector3Int(i, j, 0), mine_tile.tile);
                    }
                    mine_tile = null;
                }
            }
        }
        rock_map.SetTile(new Vector3Int(-7, 4, 0), null);
        rock_map.SetTile(new Vector3Int(-6, 4, 0), null);
        rock_map.SetTile(new Vector3Int(-5, 4, 0), null);
        rock_map.SetTile(new Vector3Int(-4, 4, 0), null);
    }


    // Update is called once per frame
    void Update()
    {
        Vector3Int dwarfPos = grid.WorldToCell(dwarf.transform.position); //Vi gjør dvergen sin posisjon om til cell-grid posisjon, 
        Vector3Int mousePos = GetMousePosition();
        if (!mousePos.Equals(prev_mouse_pos)) //Her finner vi posisjonen til musepeker tilen, som viser hvilken vi peker på. 
        {

            backgroundmap.SetTile(prev_mouse_pos, null); // Remove old hoverTile

            backgroundmap.SetTile(mousePos, mouse_tile);

            prev_mouse_pos = mousePos;

        }

        for (int i = rock_map.cellBounds.xMin; i < rock_map.cellBounds.xMax; i++) //Her fjerner vi fra shader mappet, som gjør at underlagene blir synlige
        {
            for (int j = rock_map.cellBounds.yMin; j < rock_map.cellBounds.yMax; j++)
            {
                if(Vector3Int.Distance(dwarfPos, new Vector3Int(i, j, 0)) < 5)
                {
                    shade_map.SetTile(new Vector3Int(i, j, 0), null);
                }
            }
        }
                // Right mouse click -> remove path tile

        if (Input.GetKeyDown(KeyCode.C)&& dwarfScript.Item_in_inventory == null)
        {
            target_pos = GetDirection(dwarfPos);
            if (Vector3.Distance(dwarfPos, target_pos) <= 1) //Så sammenligner vi om vi er nærme nok til å grave, vis vi er så fortsetter vi 
            {
                if (rock_map.GetTile(target_pos) != null && StaticData.Energy_mining_static > 0) //Hvis vi har energi, og vi prøver å grave på en ting
                {
                    if(rock_map.GetTile(target_pos) == copper_tile) //Hvis copper, gi copper
                    {   
                        dwarfScript.Item_in_inventory = Generation_Object.create_ore((int)Enumtypes.Ore_Quality.Copper, dwarf, StaticData.Digging_Map_information[new Vector3Int(mousePos.x, mousePos.y, 0)].percent_ore_quality);
                        dwarf.SendMessage("Inventory_Full_Message", true);
                    }
                    else if (rock_map.GetTile(target_pos) == iron_tile) //Iron
                    {
                        dwarfScript.Item_in_inventory = Generation_Object.create_ore((int)Enumtypes.Ore_Quality.Iron, dwarf, StaticData.Digging_Map_information[new Vector3Int(mousePos.x, mousePos.y, 0)].percent_ore_quality);
                        dwarf.SendMessage("Inventory_Full_Message", true);
                    }
                    else if (rock_map.GetTile(target_pos) == mithril_tile) //Mithril
                    {
                        dwarfScript.Item_in_inventory = Generation_Object.create_ore((int)Enumtypes.Ore_Quality.Mithril, dwarf, StaticData.Digging_Map_information[new Vector3Int(mousePos.x, mousePos.y, 0)].percent_ore_quality);
                        dwarf.SendMessage("Inventory_Full_Message", true);
                    }
                    rock_map.SetTile(target_pos, null);//Tilen på mousa sin posisjon blir satt til null. 
                
                    StaticData.Digging_Map_information[new Vector3Int(mousePos.x, mousePos.y, 0)] = null;
                    StaticData.Energy_mining_static = StaticData.Energy_mining_static - 1; //Mindre energi
                    UI.SendMessage("SetPosition_Energy"); //Metode for å beregne energi.
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {

            target_pos = GetDirection(dwarfPos);
            if (Vector3.Distance(dwarfPos, target_pos) <= 1) //Så sammenligner vi om vi er nærme nok til å grave, vis vi er så fortsetter vi 
            {
                target_pos = GetDirection(dwarfPos);
                for(int i = 0; i < 10; i++)
                {
                    results.Add(StaticData.Digging_Map_information[target_pos]);
                    print(target_pos);
                    target_pos = GetDirection(target_pos);
                }
               
            }
        }
    }

    private Vector3Int GetDirection(Vector3Int dwarfPos)
    {
        if (dwarf.GetComponent<DwarfScript>().last_direction == 1) //W
        {
            target_pos = new Vector3Int(dwarfPos.x, dwarfPos.y + 1, 0);
        }
        else if (dwarf.GetComponent<DwarfScript>().last_direction == 2) //A
        {
            target_pos = new Vector3Int(dwarfPos.x - 1, dwarfPos.y, 0);
        }
        else if (dwarf.GetComponent<DwarfScript>().last_direction == 3) //D
        {
            target_pos = new Vector3Int(dwarfPos.x + 1, dwarfPos.y, 0);
        }
        else if (dwarf.GetComponent<DwarfScript>().last_direction == 4) //S
        {
            target_pos = new Vector3Int(dwarfPos.x, dwarfPos.y - 1, 0);
        }
        return target_pos;
    }

    Vector3Int GetMousePosition() //Gjør musa sin posisjon om til Gridcell posisjon. 
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        return grid.WorldToCell(mouseWorldPos);

    }
    GameObject Find_nearest_interactable_object_within_range(float Range) //Her finner vi nærmeste objekt innenfor objekter med riktig tag, returnerer ett GameObject
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Mining_Ore");

        GameObject closest = null;

        float distance = Mathf.Infinity;
        Vector3 position = dwarf.transform.position;
        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;

            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance && curDistance < Range)
            {
                closest = go;
                distance = curDistance;
            }
        }

        return closest;
    }
}
