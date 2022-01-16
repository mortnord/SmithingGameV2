using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minecart : MonoBehaviour
{
    TimerScript Timer_Object;
    Object_Creation Generation_Object;
    Unsorted_Ore_container Unsorted_Tray_Object;
    bool not_moved = true;
    bool dumped_ore = false;
    public int movement_speed = 0;
    Vector2 position; 
    public Sprite[] spriteArray;
    public SpriteRenderer spriteRenderer;
    public int cooldown_time;
    
    public int amount_of_ore = 0;
   
    // Start is called before the first frame update
    void Start()
    {
        //første vi gjør er å finne objekten til andre scripts vi trenger å gjøre noe med, dette skjer her. 
        Timer_Object = Find_Components.find_Timer_Object();
        Generation_Object = Find_Components.find_Object_Creation();
        Unsorted_Tray_Object = Find_Components.find_Unsorted_Tray();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>(); //Dette er spirit-renderen, vi kan bruke denne til å bytte sprites.
        // Mengden ore som skal generates, må endres til mindre hardcoding. 
        amount_of_ore = Random.Range(1, 7);
        
    }

    // Update is called once per frame
    void Update()
    {
        //Alt dette blir vell endret av deg når du lager en patrolpath?
        position = transform.position;
        if (Timer_Object.time_Remaining < 715 && not_moved) //Når det har gått 5 sekund (siden vi starter på 720), så flytter minecarten seg
        {
            print("Tid for å flytte seg");
            not_moved = false;
            movement_speed = 4; //Movement speed minecart
        }
        if(not_moved == false)
        {
            position.y -= movement_speed * Time.deltaTime; //Vi flytter bilde nedover
            transform.position = position;
        }
        if(position.y < 1 && dumped_ore == false) //Når vi er i posisjon, gjør dette
        {
            movement_speed = 0; //stop
            spriteRenderer.sprite = spriteArray[1]; //Bytt sprite
            dumped_ore = true;
            for (int i = 0; i < amount_of_ore; i++) //Generer ore og legg det i stockpilen for usortert ore
            {
                Unsorted_Tray_Object.Ores_in_tray.Add(Generation_Object.create_ore(Random.Range(1, 4)));
            }
        }
    }
}
