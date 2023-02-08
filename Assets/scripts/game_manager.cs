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

    public bool audio;


    // Game objects
    public GameObject player;
    public GameObject start_canvas;
    public GameObject main_game_canvas;
    public GameObject pause_game_canvas;
    public GameObject game_over_canvas;

    // Audio icons
    public GameObject audio_on_icon_start;
    public GameObject audio_on_icon_pause;
    public GameObject audio_off_icon_start;
    public GameObject audio_off_icon_pause;

    // Audio
    public AudioSource start_screen_audio;
    public AudioSource main_game_audio;
    public AudioSource game_over_audio;

    Rigidbody2D rb_player;

    void Awake()
    {
        // We set the instance
        instance = this;

        audio_on();
    }

    void Start()
    {
        // We set the game state to the start screen
        set_game_state(state);

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
            case game_state.pause_game:
                pause_game();
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
        // Audio

        // Activating the correct game objects
        player.SetActive(false);
        start_canvas.SetActive(true);
        main_game_canvas.SetActive(false);
        pause_game_canvas.SetActive(false);
        game_over_canvas.SetActive(false);

        Time.timeScale = 0;
    }

    // Function to set up the main game
    public void main_game()
    {
        // Audio
        main_game_audio.Play();
        main_game_audio.loop = true;

        // Activating the correct game objects
        player.SetActive(true);
        start_canvas.SetActive(false);
        main_game_canvas.SetActive(true);
        pause_game_canvas.SetActive(false);
        game_over_canvas.SetActive(false);

        Time.timeScale = 1;
    }

    public void pause_game()
    {
        // Audio
        main_game_audio.Pause();

        // Activating the correct game objects
        main_game_canvas.SetActive(false);
        pause_game_canvas.SetActive(true);

        Time.timeScale = 0;
    }

    public void resume_game()
    {
        set_game_state(game_state.playing);
    }

    // Function to set up the game over screen
    public void game_over()
    {
        Time.timeScale = 0;

        // Audio
        main_game_audio.Stop();

        // Show the score
        player.GetComponent<score>().game_over_score_text.text = player.GetComponent<score>().current_score.ToString();
        player.GetComponent<score>().high_score_text.text = "High Score: " + player.GetComponent<score>().high_score.ToString();

        // Activating the correct game objects
        main_game_canvas.SetActive(false);
        game_over_canvas.SetActive(true);
    }

    public void reset()
    {
        // We reset the player position
        player.transform.position = new Vector2(-5.804f, -2.8007f);
        rb_player.velocity = Vector3.zero;

        // Reset the score
        player.GetComponent<score>().current_score = 0;

        // Remove all obstacles
        var obstacles = GameObject.FindGameObjectsWithTag("obstacle");
        foreach (var obstacle in obstacles)
        {
            Destroy(obstacle);
        }
    }

    public void audio_on()
    {
        // Update the audio icons
        audio_on_icon_start.SetActive(true);
        audio_on_icon_pause.SetActive(true);

        audio_off_icon_start.SetActive(false);
        audio_off_icon_pause.SetActive(false);

        // start_screen_audio.mute = false;
        main_game_audio.mute = false;
        // game_over_audio.mute = false;

        audio = true;
    }

    public void audio_off()
    {
        // Update the audio icons
        audio_off_icon_start.SetActive(true);
        audio_off_icon_pause.SetActive(true);

        audio_on_icon_start.SetActive(false);
        audio_on_icon_pause.SetActive(false);

        // start_screen_audio.mute = true;
        main_game_audio.mute = true;
        // game_over_audio.mute = true;

        audio = false;
    }
}

// Set up the game state enum
public enum game_state
{
    start_screen,
    playing,
    pause_game,
    game_over
}