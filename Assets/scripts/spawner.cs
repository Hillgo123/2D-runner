using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner : MonoBehaviour
{
    // Public variables
    public float spawn_delay = 2;

    // Private variables
    private float timer = 0;

    // Game objects
    public GameObject obstacle_1;
    public GameObject obstacle_2;
    public GameObject obstacle_3;
    public GameObject obstacle_4;
    public GameObject obstacle_5;

    // List of obstacles
    List<GameObject> obstacles = new List<GameObject>();

    void Start()
    {
        // Add the obstacles to the list
        obstacles.Add(obstacle_1);
        obstacles.Add(obstacle_2);
        // obstacles.Add(obstacle_3);
        // obstacles.Add(obstacle_4);
        // obstacles.Add(obstacle_5);

        // Create the first obstacle
        GameObject new_obstacle = Instantiate(obstacles[Random.Range(0, obstacles.Count)]);
        new_obstacle.transform.position = transform.position;
        Destroy(new_obstacle, 10);
    }

    void Update()
    {
        // If the timer is greater than the spawn delay we spawn a new obstacle
        if (timer > spawn_delay)
        {
            GameObject new_obstacle = Instantiate(obstacles[Random.Range(0, obstacles.Count)]);
            new_obstacle.transform.position = transform.position;
            Destroy(new_obstacle, 10);

            timer = 0;
        }

        timer += Time.deltaTime;
    }
}
