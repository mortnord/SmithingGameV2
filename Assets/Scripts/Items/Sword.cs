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
    public Ore_Quality ore_quality;
    public Mission_Objects mission_tag;
    void Start()
    {
        value = (100 * quality) + 100;
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>(); //Dette er spirit-renderen, vi kan bruke denne til å bytte sprites.
        spriteRenderer.sprite = spriteArray[quality];
        object_tag = Enumtypes.Object_Types.Sword;
        mission_tag = Enumtypes.Mission_Objects.Sword;
        ore_quality = (Ore_Quality)quality;
    }


}
