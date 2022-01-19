using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Enumtypes;

public class Sword : MonoBehaviour
{
    public int value;
    public Sprite[] spriteArray;
    public SpriteRenderer spriteRenderer;    
    // Start is called before the first frame update

    public int quality; //0 = low, 1 = normal, 2 = high
    public Object_Types object_tag;
    void Start()
    {
        value = (100 * quality) + 100;
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>(); //Dette er spirit-renderen, vi kan bruke denne til å bytte sprites.
        spriteRenderer.sprite = spriteArray[quality];
        object_tag = Enumtypes.Object_Types.Sword;
    }

   
}
