using UnityEngine;
using static Enumtypes;

public class Ore : MonoBehaviour
{
    public Sprite[] spriteArray;
    public SpriteRenderer spriteRenderer;
    public int quality; //0 = low, 1 = normal, 2 = high
    public Ore_Quality ore_quality;
    public Object_Types object_tag;


    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>(); //Dette er spirit-renderen, vi kan bruke denne til å bytte sprites.
        spriteRenderer.sprite = spriteArray[quality];
        object_tag = Enumtypes.Object_Types.Ore;
        ore_quality = (Ore_Quality)quality;

    }

}
