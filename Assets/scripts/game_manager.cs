using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class game_manager : MonoBehaviour
{
    // Initialize the game manager
    public static game_manager instance;

    // Public variables
    public game_state state;
    public static event Action<game_state> on_game_state_changed;


    // Game objects
    public GameObject player;
    public GameObject start_canvas;
    public GameObject main_game_canvas;
    public GameObject game_over_canvas;

    Rigidbody2D rb_player;

    void Awake()
    {
        // We set the instance
        instance = this;
    }

    void Start()
    {
        // We set the game state to the start screen
        set_game_state(game_state.start_screen);

        // We get the rigidbody component of the player
        rb_player = player.GetComponent<Rigidbody2D>();
    }

    // Function to set the game state
    public void set_game_state(game_state new_state)
    {
        state = new_state;

        switch (state)
        {
            case game_state.start_screen:
                start();
                break;
            case game_state.playing:
                main_game();
                break;
            case game_state.game_over:
                game_over();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(new_state), new_state, null);
        }

        on_game_state_changed?.Invoke(new_state);
    }

    // Function to set up the start screen
    public void start()
    {
        player.SetActive(false);
        start_canvas.SetActive(true);
        main_game_canvas.SetActive(false);
        game_over_canvas.SetActive(false);
        Time.timeScale = 0;
    }

    // Function to set up the main game
    public void main_game()
    {
        player.GetComponent<score>().current_score = 0;
        player.SetActive(true);
        start_canvas.SetActive(false);
        main_game_canvas.SetActive(true);
        game_over_canvas.SetActive(false);
        Time.timeScale = 1;
    }

    // Function to set up the game over screen
    public void game_over()
    {
        Time.timeScale = 0;

        // Show the score
        player.GetComponent<score>().game_over_score_text.text = player.GetComponent<score>().current_score.ToString();
        player.GetComponent<score>().high_score_text.text = "High Score: " + player.GetComponent<score>().high_score.ToString();

        // We reset the player position
        player.transform.position = new Vector2(-5.804f, -2.8007f);
        rb_player.velocity = Vector3.zero;

        main_game_canvas.SetActive(false);
        game_over_canvas.SetActive(true);
        var obstacles = GameObject.FindGameObjectsWithTag("obstacle");
        foreach (var obstacle in obstacles)
        {
            Destroy(obstacle);
        }
    }
}

// Set up the game state enum
public enum game_state
{
    start_screen,
    playing,
    game_over
}