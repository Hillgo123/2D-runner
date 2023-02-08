using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_death : MonoBehaviour
{

    public AudioSource death_sound;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // If the player collides with a spike we set the game state to game_over
        if (collision.gameObject.tag == "spike")
        {
            if (game_manager.instance.audio)
            {
                death_sound.Play();
            }

            game_manager.instance.set_game_state(game_state.game_over);
        }
    }
}
