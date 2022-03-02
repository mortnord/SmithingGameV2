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
    

    private Vector3Int prev_mouse_pos = new Vector3Int();

    // Start is called before the first frame update
    void Start() //Vi finner nødvendige objekter, og gir beskjed til rock mappet om å generate ore. 
    {
        grid = gameObject.GetComponent<Grid>();
        rock_map.CompressBounds();
        SendMessage("mapToGenerateIn", rock_map);
        dwarfScript = dwarf.GetComponent<DwarfScript>();

        Generation_Object = Find_Components.find_Object_Creation();
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

        for (int i = shade_map.cellBounds.xMin; i < shade_map.cellBounds.xMax; i++)
        {
            for (int j = shade_map.cellBounds.yMin; j < shade_map.cellBounds.yMax; j++)
            {
                if(Vector3Int.Distance(dwarfPos, new Vector3Int(i, j, 0)) < 500)
                {
                    shade_map.SetTile(new Vector3Int(i, j, 0), null);
                }
            }
        }
                // Right mouse click -> remove path tile

        if (Input.GetMouseButtonDown(0))
        {
            
            if (Vector3.Distance(dwarfPos, mousePos) <= 1) //Så sammenligner vi om vi er nærme nok til å grave, vis vi er så fortsetter vi 
            {
                if (rock_map.GetTile(mousePos) != null && StaticData.Energy_mining_static > 0)
                {
                    if(rock_map.GetTile(mousePos) == copper_tile)
                    {
                        dwarfScript.Item_in_inventory = Generation_Object.create_ore((int)Enumtypes.Ore_Quality.Copper, dwarf);
                        dwarf.SendMessage("Inventory_Full_Message", true);
                    }
                    else if (rock_map.GetTile(mousePos) == iron_tile)
                    {
                        dwarfScript.Item_in_inventory = Generation_Object.create_ore((int)Enumtypes.Ore_Quality.Iron, dwarf);
                        dwarf.SendMessage("Inventory_Full_Message", true);
                    }
                    else if (rock_map.GetTile(mousePos) == mithril_tile)
                    {
                        dwarfScript.Item_in_inventory = Generation_Object.create_ore((int)Enumtypes.Ore_Quality.Mithril, dwarf);
                        dwarf.SendMessage("Inventory_Full_Message", true);
                    }
                    rock_map.SetTile(mousePos, null); //Tilen på mousa sin posisjon blir satt til null. 
                    StaticData.Energy_mining_static = StaticData.Energy_mining_static - 1;
                    UI.SendMessage("SetPosition_Energy");
                    UI.SendMessage("change_sprite");
                }
            }
        }
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
