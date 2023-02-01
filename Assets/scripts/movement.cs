using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class movement : MonoBehaviour
{

    public float jump_height = 1;

    private bool is_jumping = false;


    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    void FixedUpdate()
    {
        if (Input.GetKey("space") && is_jumping == false)
        {
            rb.AddForce(Vector2.up * jump_height, ForceMode2D.Impulse);
            is_jumping = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "ground")
        {
            is_jumping = false;
        }
    }
}
