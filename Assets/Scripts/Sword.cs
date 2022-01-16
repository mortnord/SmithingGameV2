using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public int value;
    public Sprite[] spriteArray;
    public SpriteRenderer spriteRenderer;    
    // Start is called before the first frame update

    public int quality; //0 = low, 1 = normal, 2 = high
    void Start()
    {
        value = (100 * quality) + 100;
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>(); //Dette er spirit-renderen, vi kan bruke denne til å bytte sprites.
        spriteRenderer.sprite = spriteArray[quality];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
