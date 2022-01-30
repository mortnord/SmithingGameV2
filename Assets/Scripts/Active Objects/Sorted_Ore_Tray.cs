using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sorted_Ore_Tray : MonoBehaviour
{
    public int quality; 
    public List<GameObject> Ores_in_tray = new List<GameObject>();

    public Sprite[] spriteArray_iron;
    public Sprite[] spriteArray_copper;
    public Sprite[] spriteArray_Mithril;

    public SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(quality == 1)
        {
            //En mulighet å lage en array med plasser, der vi fyller en og en plass med en sprite når vi legger noe inni?
            if (Ores_in_tray.Count > 0 && Ores_in_tray.Count < 3)//Sprite endring fra tomt til fult. 
            {
                spriteRenderer.sprite = spriteArray_iron[1];
            }
            else if (Ores_in_tray.Count > 3 && Ores_in_tray.Count <= 6)
            {
                spriteRenderer.sprite = spriteArray_iron[2];
            }
            else if (Ores_in_tray.Count > 7 && Ores_in_tray.Count <= 9)
            {
                spriteRenderer.sprite = spriteArray_iron[3];
            }
            else if(Ores_in_tray.Count == 10)
            {
                spriteRenderer.sprite = spriteArray_iron[4];
            }
            else if (Ores_in_tray.Count == 0) //Tilbake til tomt når det er tomt. 
            {
                spriteRenderer.sprite = spriteArray_iron[0];
            }
        }
    }
}
