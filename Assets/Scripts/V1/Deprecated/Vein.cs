using UnityEngine;
using static Enumtypes;
public class Vein : MonoBehaviour
{
    // Start is called before the first frame update
    Object_Creation Generation_Object;
    public Ore_Quality ore_quality;
    public Sprite[] spriteArray;
    public SpriteRenderer spriteRenderer;
    void Start()
    {
        Generation_Object = Find_Components.Find_Object_Creation();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>(); //Dette er spirit-renderen, vi kan bruke denne til å bytte sprites.
        spriteRenderer.sprite = spriteArray[(int)ore_quality];
    }
    // Update is called once per frame
}
