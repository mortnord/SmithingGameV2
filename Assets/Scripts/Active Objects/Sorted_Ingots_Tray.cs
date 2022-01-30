using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sorted_Ingots_Tray : MonoBehaviour
{
    public int quality;
    public List<GameObject> Ingots_in_tray = new List<GameObject>();

    public Sprite[] spriteArray;
    public SpriteRenderer spriteRenderer;
   
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //En mulighet å lage en array med plasser, der vi fyller en og en plass med en sprite når vi legger noe inni?
        if (Ingots_in_tray.Count > 0)//Sprite endring fra tomt til fult. 
        {
            quality = ((int)Ingots_in_tray[0].GetComponent<Ingot>().ore_quality) + 1;
            spriteRenderer.sprite = spriteArray[quality];
        }
        if (Ingots_in_tray.Count == 0) //Tilbake til tomt når det er tomt. 
        {
            spriteRenderer.sprite = spriteArray[0];
        }
    }
}
