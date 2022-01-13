using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DwarfScript : MonoBehaviour
{
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        
        float moveByX = horizontal * 2;
        float moveByY = vertical * 2;
        rb.velocity = new Vector2(moveByX, moveByY);
    }
}
