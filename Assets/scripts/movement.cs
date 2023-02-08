using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class movement : MonoBehaviour
{
    // Public variables
    public float jump_height;

    // Private variables
    private bool in_air;
    private float air_time;
    private float jump_cooldown = 0.6f;

    Rigidbody2D rb;
    Animator animator;

    // Game objects
    public GameObject camera;


    void Start()
    {
        // We get the rigidbody and animator components
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        // Calling functions that should run every frame
        jump();
        camera_follow();
        falling();

        // If the player falls off the left side of the screen we move him to the right
        if (rb.position.x < -5.804 && !in_air)
        {
            transform.position += Vector3.right * 5.61f * Time.deltaTime;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // If the player collides with the ground we set the in_air variable to false
        if (collision.gameObject.tag == "ground")
        {
            in_air = false;

            // Update the animator
            animator.SetBool("jump", false);
        }
    }

    private void jump()
    {
        // Update the animator
        animator.SetFloat("y_velocity", rb.velocity.y);

        // Check if the player is in the air or on the ground and if the jump cooldown is over
        if (Input.GetKey("space") && !in_air && jump_cooldown > 0.5)
        {
            // If the player is on the ground and presses the space key we jump
            rb.AddForce(Vector2.up * jump_height, ForceMode2D.Impulse);
            in_air = true;

            // Reset the jump cooldown
            jump_cooldown = 0;

            // Update the animator
            animator.SetBool("jump", true);
        }

        // Jump cooldown to prevent bug making the player jump multiple times
        jump_cooldown += Time.deltaTime;
    }

    // This function makes the camera follow the player
    private void camera_follow()
    {
        if (rb.position.y > 0)
        {
            camera.transform.position = new Vector3(camera.transform.position.x, rb.position.y, camera.transform.position.z);
        }
        // Reset the camera to the default position if the player is on the ground
        else
        {
            camera.transform.position = new Vector3(camera.transform.position.x, 0, camera.transform.position.z);
        }
    }

    // This function updates the animator if the player is falling
    private void falling()
    {
        if (rb.velocity.y < -1)
        {
            in_air = true;
            animator.SetBool("jump", true);
            animator.SetFloat("y_velocity", rb.velocity.y);
        }
    }
}