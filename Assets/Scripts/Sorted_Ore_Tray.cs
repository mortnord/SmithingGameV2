using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sorted_Ore_Tray : MonoBehaviour
{

    public int quality;
    public List<GameObject> Ores_in_tray = new List<GameObject>();

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
        if (Ores_in_tray.Count > 0)
        {
            spriteRenderer.sprite = spriteArray[1];
        }
        if (Ores_in_tray.Count == 0)
        {
            spriteRenderer.sprite = spriteArray[0];
        }
    }
}
