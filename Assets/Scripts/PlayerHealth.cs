using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f;  // Maximum health of the player
    public float currentHealth;     // Current health of the player

    void Start()
    {
        // Initialize the current health to max health at the start
        currentHealth = maxHealth;
    }

    void Update()
    {

        // Check if health is zero or below
        if (currentHealth <= 0f)
        {
            Die();
        }
    }

    // Method that gets called when the player's health reaches 0
    void Die()
    {
        // Trigger death functionality here
        UnityEngine.Debug.Log("Player has died!");

        // Example of what you might want to do when the player dies:
        // - Disable player controls
        // - Play death animation
        // - Trigger game over screen
        // - Respawn the player, etc.
        // For now, we will just destroy the player object as an example:
    }
}
