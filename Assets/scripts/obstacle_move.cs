using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class obstacle_move : MonoBehaviour
{
    // Public variables
    public float speed;

    void Update()
    {
        // Move the obstacle to the left
        transform.position += Vector3.left * speed * Time.deltaTime;
    }
}
