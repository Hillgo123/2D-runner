using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class movement : MonoBehaviour
{
    public float jump_height;
    public int jump_count;
    public float jump_delay;

    private int jump_counter;
    private bool in_air;
    private float air_time;

    Rigidbody2D rb;
    Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }


    void FixedUpdate()
    {
        jump();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "ground")
        {
            jump_counter = jump_count;
            in_air = false;
            air_time = 0;
            animator.SetBool("jump", false);
        }
    }

    private void jump()
    {
        animator.SetFloat("y_velocity", rb.velocity.y);

        if (Input.GetKey("space") && !in_air)
        {
            rb.AddForce(Vector2.up * jump_height, ForceMode2D.Impulse);
            in_air = true;
            animator.SetBool("jump", true);
        }

        if (in_air)
        {
            air_time += Time.deltaTime;
            if (Input.GetKey("space") && jump_counter > 0 && air_time > jump_delay)
            {
                rb.AddForce(Vector2.up * jump_height, ForceMode2D.Impulse);
                animator.SetBool("jump", true);
                jump_counter--;
                air_time = 0;
            }
        }
    }
}
