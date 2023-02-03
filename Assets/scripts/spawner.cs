using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner : MonoBehaviour
{
    public float spawn_delay = 2;

    private float timer = 0;

    public GameObject obstacle_1;
    public GameObject obstacle_2;
    public GameObject obstacle_3;
    public GameObject obstacle_4;
    public GameObject obstacle_5;

    List<GameObject> obstacles = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        obstacles.Add(obstacle_1);
        // obstacles.Add(obstacle_2);
        // obstacles.Add(obstacle_3);
        // obstacles.Add(obstacle_4);
        // obstacles.Add(obstacle_5);

        GameObject new_obstacle = Instantiate(obstacles[Random.Range(0, obstacles.Count)]);
        new_obstacle.transform.position = transform.position;
        Destroy(new_obstacle, 10);
    }

    // Update is called once per frame
    void Update()
    {
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
