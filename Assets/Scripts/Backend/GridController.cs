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
    void Start() //Vi finner nødvendige objekter, og gir beskjed til rock mappet om å generate ore. 
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
        if (!mousePos.Equals(prev_mouse_pos)) //Her finner vi posisjonen til musepeker tilen, som viser hvilken vi peker på. 
        {
            
            backgroundmap.SetTile(prev_mouse_pos, null); // Remove old hoverTile

            backgroundmap.SetTile(mousePos, mouse_tile);

            prev_mouse_pos = mousePos;

        }
       


        // Right mouse click -> remove path tile

        if (Input.GetMouseButtonDown(0)) 
        {
            Vector3Int dwarfPos = grid.WorldToCell(dwarf.transform.position); //Vi gjør dvergen sin posisjon om til cell-grid posisjon, 
            if(Vector3.Distance(dwarfPos, mousePos) <= 1) //Så sammenligner vi om vi er nærme nok til å grave, vis vi er så fortsetter vi 
            {
                rock_map.SetTile(mousePos, null); //Tilen på mousa sin posisjon blir satt til null. 
            }
        }
        if(Input.GetKeyDown(KeyCode.Q))
        {
            Nearest_object = Find_nearest_interactable_object_within_range(0.5f); //Vi prøver å plukke opp metal //Endre til annen knapp i framtiden.
            if (Nearest_object != null)
            {
                dwarfScript.Item_in_inventory = Nearest_object;
                dwarf.SendMessage("Inventory_Full_Message", true);

                grid.SendMessage("RemoveFromList", Nearest_object); //Her fjerner vi oren fra lista over synlig / ikke synlig
                Nearest_object = null;
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
