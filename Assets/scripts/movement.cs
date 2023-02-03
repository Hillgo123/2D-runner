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

    public GameObject camera;

    // public GameObject game_manager;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        jump();
        camera_follow();

        if (rb.position.x < -5.61 && !in_air)
        {
            transform.position += Vector3.right * 5.61f * Time.deltaTime;
        }
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

        if (collision.gameObject.tag == "spike")
        {

            GameObject.Find("game_manager").GetComponent<manager>().game_over();
        }
    }

    private void jump()
    {
        // First we check if the player is in the air or on the ground
        if (Input.GetKey("space") && !in_air)
        {
            // If the player is on the ground and presses the space key we jump
            rb.AddForce(Vector2.up * jump_height, ForceMode2D.Impulse);
            in_air = true;
            animator.SetBool("jump", true);
            jump_counter--;
            air_time = 0;
        }

        if (in_air)
        {
            // If the player is in the air we start the air_time counter
            air_time += Time.deltaTime;
            // If the player presses the space key and has jumps left and has been in the air for a bit
            if (Input.GetKey("space") && jump_counter > 0 && air_time > jump_delay)
            {
                // We jump again
                rb.AddForce(Vector2.up * jump_height, ForceMode2D.Impulse);
                animator.SetBool("jump", true);
                jump_counter--;
                air_time = 0;
            }
        }
    }

    private void camera_follow()
    {
        if (rb.position.y > 0)
        {
            camera.transform.position = new Vector3(camera.transform.position.x, rb.position.y, camera.transform.position.z);
        }
        else
        {
            camera.transform.position = new Vector3(camera.transform.position.x, 0, camera.transform.position.z);
        }
    }
}