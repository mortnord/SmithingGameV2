using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sorted_Ore_Tray : MonoBehaviour
{
    public int quality; 
    public List<GameObject> Ores_in_tray = new List<GameObject>();

    public Sprite[] spriteArray_iron;
    public Sprite[] spriteArray_copper;
    public Sprite[] spriteArray_mithril;

    public Sprite[] using_sprite;
    public SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        if (quality == 0)
        {
            using_sprite = spriteArray_copper;

        }
        else if (quality == 1)
        {
            using_sprite = spriteArray_iron;
        }

        else if (quality == 2)
        {
            using_sprite = spriteArray_mithril;
        }
    }

    // Update is called once per frame
    void Update()
    {
        handleSprite();

        
    }

    private void handleSprite()
    {
        if (Ores_in_tray.Count > 0 && Ores_in_tray.Count <= 3)//Sprite endring fra tomt til fult. 
        {
            spriteRenderer.sprite = using_sprite[1];
        }
        else if (Ores_in_tray.Count > 3 && Ores_in_tray.Count <= 6)
        {
            spriteRenderer.sprite = using_sprite[2];
        }
        else if (Ores_in_tray.Count > 6 && Ores_in_tray.Count <= 9)
        {
            spriteRenderer.sprite = using_sprite[3];
        }
        else if (Ores_in_tray.Count == 10)
        {
            spriteRenderer.sprite = using_sprite[4];
        }
        else if (Ores_in_tray.Count == 0) //Tilbake til tomt når det er tomt. 
        {
            spriteRenderer.sprite = using_sprite[0];
        }
    }
}
