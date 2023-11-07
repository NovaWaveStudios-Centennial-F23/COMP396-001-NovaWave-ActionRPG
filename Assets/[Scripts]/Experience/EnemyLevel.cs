/**Created by Mithul Koshy
 * Used for enemy death and experience values
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLevel : MonoBehaviour
{
    public int experienceValue = 50; // Amount of XP this enemy gives

    private ExperienceManager experienceManager;

    private void Start()
    {
        // Find the ExperienceManager in the scene
        experienceManager = FindObjectOfType<ExperienceManager>();
    }

    // Call this when the enemy dies
    public void Die()
    {
        // Give XP to the player
        experienceManager.AddExperience(experienceValue);

        // Destroy the enemy object, play death animation, etc.
        Destroy(gameObject);
    }
}
