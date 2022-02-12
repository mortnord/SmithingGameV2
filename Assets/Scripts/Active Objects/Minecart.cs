using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minecart : MonoBehaviour
{

    Object_Creation Generation_Object;
    Unsorted_Ore_container Unsorted_Tray_Object;
    bool not_moved = true;
    bool dumped_ore = false;
    public bool tilbake_tid = false;
    public int movement_speed = 0;
    Vector2 position; 
    public Sprite[] spriteArray;
    public SpriteRenderer spriteRenderer;
    public int cooldown_time;
    
    public int amount_of_ore = 0;
    public float time_until_move = 0;
   
    // Start is called before the first frame update
    void Start()
    {
        //første vi gjør er å finne objekten til andre scripts vi trenger å gjøre noe med, dette skjer her. 
        
        Generation_Object = Find_Components.find_Object_Creation();
        Unsorted_Tray_Object = Find_Components.find_Unsorted_Tray();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>(); //Dette er spirit-renderen, vi kan bruke denne til å bytte sprites.
        // Mengden ore som skal generates, må endres til mindre hardcoding. 
        
        time_until_move = 5;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(not_moved)
        {
            time_until_move = time_until_move - Time.deltaTime;
        }
        //Alt dette blir vell endret av deg når du lager en patrolpath?
        position = transform.position;
        if (time_until_move < 0 && not_moved) 
        {
            print("Tid for å flytte seg");
            not_moved = false;
            movement_speed = 4; //Movement speed minecart
            time_until_move = 0;
        }
        if (not_moved == false && tilbake_tid == false)
        {
            position.y -= movement_speed * Time.deltaTime; //Vi flytter bilde nedover
            transform.position = position;
        }
        if(position.y < 1 && dumped_ore == false) //Når vi er i posisjon, gjør dette
        {
            movement_speed = 0; //stop
            spriteRenderer.sprite = spriteArray[1]; //Bytt sprite
            dumped_ore = true;
            for (int i = 0; i < StaticData.Transition_Ores.Count; i++) //Generer ore og legg det i stockpilen for usortert ore
            {
                Unsorted_Tray_Object.Ores_in_tray.Add(Generation_Object.create_ore((int)StaticData.Transition_Ores[i]));
            }
            amount_of_ore = 0;
            StaticData.Transition_Ores.Clear();
            tilbake_tid = true;
        }
        if(Unsorted_Tray_Object.Ores_in_tray.Count == 0 && dumped_ore == true && tilbake_tid)
        {
            print("Tid for å dra tilbake");
            movement_speed = 2;
            position.y += movement_speed * Time.deltaTime; //Vi flytter bilde nedover
            transform.position = position;
            if(position.y > 8)
            {
                Reset();
            }
        }
    }

    private void Reset()
    {
        time_until_move = 5;
        not_moved = true;
        dumped_ore = false;
        tilbake_tid = false;
        transform.position = new Vector3(-8.7f, 8f);
        amount_of_ore = Random.Range(1, 7);
    }
}
