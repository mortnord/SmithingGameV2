using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridController : MonoBehaviour
{
    private Grid grid;
    public Tilemap backgroundmap = null;
    public Tilemap rock_map = null;
    public Tile mouse_tile = null;
    public RuleTile mined_tile = null;
    public GameObject dwarf = null;
    GameObject Nearest_object = null;
    DwarfScript dwarfScript = null;

    private Vector3Int prev_mouse_pos = new Vector3Int();

    // Start is called before the first frame update
    void Start()
    {
        grid = gameObject.GetComponent<Grid>();
        rock_map.CompressBounds();
        SendMessage("mapToGenerateIn", rock_map);
        dwarfScript = dwarf.GetComponent<DwarfScript>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3Int mousePos = GetMousePosition();
        if (!mousePos.Equals(prev_mouse_pos))
        {
            
            backgroundmap.SetTile(prev_mouse_pos, null); // Remove old hoverTile

            backgroundmap.SetTile(mousePos, mouse_tile);

            prev_mouse_pos = mousePos;

        }
       


        // Right mouse click -> remove path tile

        if (Input.GetMouseButtonDown(0)) 
        {
            Vector3Int dwarfPos = grid.WorldToCell(dwarf.transform.position);
            print(dwarfPos);
            print(mousePos);
            if(Vector3.Distance(dwarfPos, mousePos) <= 1)
            {
                rock_map.SetTile(mousePos, null);
                Nearest_object = Find_nearest_interactable_object_within_range(0.5f);
                if (Nearest_object != null)
                {
                    dwarfScript.Item_in_inventory = Nearest_object;
                    dwarf.SendMessage("Inventory_Full_Message", true);
                    Nearest_object = null;
                }
            }
               
            
            // if (Vector3.Distance(dwarf.transform.position, List_of_ores[i].transform.position) > 5)


        }
    }
    
    Vector3Int GetMousePosition()
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
