using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Pickaxe : MonoBehaviour
{
    public Sprite[] spriteArray;
    public SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Awake()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>(); //Dette er spirit-renderen, vi kan bruke denne til å bytte sprites.
    }
    public void Set_Sprite(Sprite sprite_in)
    {
        spriteRenderer.sprite = sprite_in;
    }
    // Update is called once per frame
}
