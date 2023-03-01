using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class score : MonoBehaviour
{
    // Text objects
    public TextMeshProUGUI score_text;
    public TextMeshProUGUI game_over_score_text;
    public TextMeshProUGUI high_score_text;
    public TextMeshProUGUI collectible_text;
    public TextMeshProUGUI game_over_collectible_text;

    public int current_score;
    public int high_score;
    public int collectible_counter;
    private float score_counter;

    public AudioSource collectible_audio;

    void Start()
    {
        // Reset the score
        current_score = 0;
        high_score = 0;
        score_text.text = current_score.ToString();

        collectible_counter = 0;
        collectible_text.text = collectible_counter.ToString();
        game_over_collectible_text.text = collectible_counter.ToString();
    }

    void FixedUpdate()
    {
        // Update the score counter
        score_counter += Time.deltaTime;

        // Update the score text
        score_text.text = current_score.ToString();

        // If the score counter is greater than 1.5 seconds we increase the score by 1
        if (score_counter > 1.5f)
        {
            current_score++;
            score_counter = 0;
        }

        // Update high score
        if (current_score > high_score)
        {
            high_score = current_score;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "pick_up")
        {
            if (game_manager.instance.audio)
            {
                collectible_audio.Play();
            }

            collectible_counter++;
            collectible_text.text = collectible_counter.ToString();
            game_over_collectible_text.text = collectible_counter.ToString();
            Destroy(collision.gameObject);
        }
    }
}
