using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DwarfScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector2 position = transform.position;
        position.x += 2 * horizontal * Time.deltaTime;
        position.y += 2 * vertical * Time.deltaTime;
        transform.position = position;
    }
}
