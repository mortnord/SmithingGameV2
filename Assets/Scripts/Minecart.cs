using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minecart : MonoBehaviour
{
    TimerScript Timer_Object;
    Object_Creation Generation_Object;
    bool not_moved = true;
    bool dumped_ore = false;
    public int movement_speed = 0;
    Vector2 position; 
    public Sprite[] spriteArray;
    public SpriteRenderer spriteRenderer;
    public int amount_of_ore = 0;
   
    // Start is called before the first frame update
    void Start()
    {
        Timer_Object = Find_Components.find_Timer_Object();
        Generation_Object = Find_Components.find_Object_Creation();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        amount_of_ore = 5;
        
    }

    // Update is called once per frame
    void Update()
    {

        position = transform.position;
        if (Timer_Object.time_Remaining < 715 && not_moved)
        {
            print("Tid for å flytte seg");
            not_moved = false;
            movement_speed = 4;
        }
        if(not_moved == false)
        {
            position.y -= movement_speed * Time.deltaTime;
            transform.position = position;
        }
        if(position.y < 1 && dumped_ore == false)
        {
            movement_speed = 0;
            spriteRenderer.sprite = spriteArray[1];
            dumped_ore = true;
            for (int i = 0; i < amount_of_ore; i++)
            {
                Generation_Object.create_ore(Random.Range(1,4));
            }
        }
    }
}
