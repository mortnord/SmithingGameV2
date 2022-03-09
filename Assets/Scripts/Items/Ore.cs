using UnityEngine;
using static Enumtypes;

public class Ore : MonoBehaviour
{
    public Sprite[] spriteArray;
    public SpriteRenderer spriteRenderer;
    public int quality; //0 = low, 1 = normal, 2 = high
   
    public int percent_ore_to_ingot;


    void Start()
    {
        gameObject.SetActive(false);
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>(); //Dette er spirit-renderen, vi kan bruke denne til å bytte sprites.
        spriteRenderer.sprite = spriteArray[quality];
      
    }
}
