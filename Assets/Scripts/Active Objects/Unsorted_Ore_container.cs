using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unsorted_Ore_container : MonoBehaviour, IInteractor_Connector
{
    public List<GameObject> Ores_in_tray = new List<GameObject>();
    public Sprite[] spriteArray;
    public SpriteRenderer spriteRenderer;

    public void Drop_Off(GameObject item_to_deposit)
    {
        throw new System.NotImplementedException();
    }

    
    public void Pickup(GameObject main_character)
    {
        main_character.GetComponent<DwarfScript>().Item_in_inventory = Ores_in_tray[0];
    }

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //En mulighet å lage en array med plasser, der vi fyller en og en plass med en sprite når vi legger noe inni?
        if (Ores_in_tray.Count > 0) //Sprite endring fra tomt til fult. 
        {
            spriteRenderer.sprite = spriteArray[1];
        }
        if(Ores_in_tray.Count == 0) //Tilbake til tomt når det er tomt. 
        {
            spriteRenderer.sprite = spriteArray[0];
        }
    }
}
