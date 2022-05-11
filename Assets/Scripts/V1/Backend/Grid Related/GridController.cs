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
    public Tilemap track_map = null;
    public Tile mouse_tile = null;
    public Tile copper_tile;
    public Tile iron_tile;
    public Tile mithril_tile;
    public RuleTile mined_tile = null;
    public RuleTile track_tile = null;
    public GameObject dwarf = null;
    public GameObject UI = null;
    MainCharacterStateManager dwarfScript = null;
    public Mineable_Tile mine_tile;
    private Vector3Int prev_mouse_pos = new Vector3Int();
    private Vector3Int target_pos;
    public List<Mineable_Tile> results = new List<Mineable_Tile>();
    // Start is called before the first frame update
    void Start() //Vi finner nødvendige objekter, og gir beskjed til rock mappet om å generate ore. 
    {
        grid = gameObject.GetComponent<Grid>();
        dwarfScript = dwarf.GetComponent<MainCharacterStateManager>();
        Load_Map();
        Generation_Object = Find_Components.Find_Object_Creation();
    }
    private void Load_Map()
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
        InitialSetup();
        
        Load_Tracks();
    }

    private void InitialSetup()
    {
        rock_map.SetTile(new Vector3Int(-7, 4, 0), null);
        rock_map.SetTile(new Vector3Int(-6, 4, 0), null);
        rock_map.SetTile(new Vector3Int(-5, 4, 0), null);
        rock_map.SetTile(new Vector3Int(-4, 4, 0), null);
        track_map.SetTile(new Vector3Int(-4, 4, 0), track_tile);
        track_map.SetTile(new Vector3Int(-5, 4, 0), track_tile);
        StaticData.track_positions.Add(new Vector3Int(-4, 4, 0));
        StaticData.track_positions.Add(new Vector3Int(-5, 4, 0));
    }

    private void Load_Tracks()
    {
        for (int i = 0; i < StaticData.track_positions.Count; i++)
        {
            track_map.SetTile(StaticData.track_positions[i], track_tile);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3Int dwarfPos = grid.WorldToCell(dwarf.transform.position); //Vi gjør dvergen sin posisjon om til cell-grid posisjon, 
        Vector3Int mousePos = Get_Mouse_Position();
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
        if(Input.anyKey)
        {
            HandleInputs(dwarfPos);
        }
    }

    private void HandleInputs(Vector3Int dwarfPos)
    {
        // Right mouse click -> remove path tile
        if (Input.GetKeyDown(KeyCode.C) && dwarfScript.Get_Item_In_Inventory() == null)
        {
            target_pos = Get_Direction(dwarfPos);
            if (Vector3.Distance(dwarfPos, target_pos) <= 1) //Så sammenligner vi om vi er nærme nok til å grave, vis vi er så fortsetter vi 
            {
                if (rock_map.GetTile(target_pos) != null && StaticData.Energy_mining_static > 0) //Hvis vi har energi, og vi prøver å grave på en ting
                {
                    if (rock_map.GetTile(target_pos) == copper_tile) //Hvis copper, gi copper
                    {
                        dwarfScript.Set_Item_In_Inventory(Generation_Object.Create_Ore((int)Enumtypes.Ore_Quality.Copper, dwarf, StaticData.Digging_Map_information[new Vector3Int(target_pos.x, target_pos.y, 0)].percent_ore_quality));
                        dwarf.SendMessage("Inventory_Full_Message", true);
                    }
                    else if (rock_map.GetTile(target_pos) == iron_tile) //Iron
                    {
                        dwarfScript.Set_Item_In_Inventory(Generation_Object.Create_Ore((int)Enumtypes.Ore_Quality.Iron, dwarf, StaticData.Digging_Map_information[new Vector3Int(target_pos.x, target_pos.y, 0)].percent_ore_quality));
                        dwarf.SendMessage("Inventory_Full_Message", true);
                    }
                    else if (rock_map.GetTile(target_pos) == mithril_tile) //Mithril
                    {
                        dwarfScript.Set_Item_In_Inventory(Generation_Object.Create_Ore((int)Enumtypes.Ore_Quality.Mithril, dwarf, StaticData.Digging_Map_information[new Vector3Int(target_pos.x, target_pos.y, 0)].percent_ore_quality));
                        dwarf.SendMessage("Inventory_Full_Message", true);
                    }
                    rock_map.SetTile(target_pos, null);//Tilen på mousa sin posisjon blir satt til null. 
                    StaticData.Digging_Map_information[new Vector3Int(target_pos.x, target_pos.y, 0)] = null;

                }
            }
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            if (Check_Legal_Track_Position(dwarfPos))
            {
                track_map.SetTile(dwarfPos, track_tile);
                StaticData.track_positions.Add(dwarfPos);
                StaticData.amount_standard_tracks--;
                UI.SendMessage("Set_Position_Tracks");
            }
        }
        if(Input.GetKeyDown(KeyCode.L))
        {
            for (int i = 0; i < StaticData.track_positions.Count; i++)
            {
                print(StaticData.track_positions[i]);
            }
        }
        if (Input.GetKeyDown(KeyCode.R)) //Scanneren
        {
            target_pos = Get_Direction(dwarfPos);
            if (Vector3.Distance(dwarfPos, target_pos) <= 1) //Så sammenligner vi om vi er nærme nok til å grave, vis vi er så fortsetter vi 
            {
                target_pos = Get_Direction(dwarfPos);
                for (int i = 0; i < 10; i++)
                {
                    results.Add(StaticData.Digging_Map_information[target_pos]);
                    print(target_pos);
                    target_pos = Get_Direction(target_pos);
                }
            }
        }
    }

    private bool Check_Legal_Track_Position(Vector3Int dwarfpos)
    {
        if(StaticData.amount_standard_tracks == 0)
        {
            return false;
        }
        if(StaticData.track_positions.Contains(dwarfpos))
        {
            return false;
        }
        return true;
    }

    private Vector3Int Get_Direction(Vector3Int dwarfPos)
    {
        if (dwarf.GetComponent<MainCharacterStateManager>().Get_Direction() == 1) //W
        {
            target_pos = new Vector3Int(dwarfPos.x, dwarfPos.y + 1, 0);
        }
        else if (dwarf.GetComponent<MainCharacterStateManager>().Get_Direction() == 2) //A
        {
            target_pos = new Vector3Int(dwarfPos.x - 1, dwarfPos.y, 0);
        }
        else if (dwarf.GetComponent<MainCharacterStateManager>().Get_Direction() == 3) //D
        {
            target_pos = new Vector3Int(dwarfPos.x + 1, dwarfPos.y, 0);
        }
        else if (dwarf.GetComponent<MainCharacterStateManager>().Get_Direction() == 4) //S
        {
            target_pos = new Vector3Int(dwarfPos.x, dwarfPos.y - 1, 0);
        }
        return target_pos;
    }
    Vector3Int Get_Mouse_Position() //Gjør musa sin posisjon om til Gridcell posisjon. 
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return grid.WorldToCell(mouseWorldPos);
    }
}
