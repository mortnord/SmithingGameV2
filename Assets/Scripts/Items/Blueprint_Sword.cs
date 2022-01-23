using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blueprint_Sword : MonoBehaviour
{
    public Sprite[] spriteArray;
    public SpriteRenderer spriteRenderer;
    public int sprite_nr = 0;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>(); //Dette er spirit-renderen, vi kan bruke denne til å bytte sprites.
        spriteRenderer.sprite = spriteArray[sprite_nr];
    }

    // Update is called once per frame
    void Update()
    {

    }
}
