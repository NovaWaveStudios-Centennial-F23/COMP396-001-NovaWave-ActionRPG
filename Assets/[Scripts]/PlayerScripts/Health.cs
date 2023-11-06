// Author: Mithul Koshy
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    public ValuePool lifepool; // Assuming this is your character's health pool
    public float reloadDelay = 5.0f; // Time in seconds before the scene reloads
    // You may want to set these in the inspector or in a Start() method if they are constant
    private void Start()
    {
        if (lifepool == null)
        {
            lifepool = new ValuePool
            {
                maxValue = 100f,
                currentValue = 100f // Initialize currentValue to the maximum value
            };
        }
    }

    // This method is used to apply damage to the character
    public void TakeDamage(float damageAmount)
    {
        lifepool.currentValue -= damageAmount;
        Debug.Log("Lifepool: " + lifepool.currentValue);

        if (lifepool.currentValue <= 0)
        {
            Die(); // Call the death method if health goes to 0 or below
        }
    }

    // Method to handle death logic
    private void Die()
    {
        StartCoroutine(ReloadCurrentSceneWithDelay());
        Debug.Log(gameObject.name + " has died.");

        // Play death animation
        Animator animator = GetComponent<Animator>();
        if (animator != null)
        {
            animator.SetTrigger("Die");

        }

        // Add XP to player's experience
        ExperienceManager experienceManager = FindObjectOfType<ExperienceManager>();
        if (experienceManager != null)
        {
            experienceManager.AddExperience(50); // Replace 50 with the actual experience value you want to give
        }
        ReloadCurrentSceneWithDelay();
        // Destroy(gameObject, 2f); // Waits for 2 seconds before destroying the game object
    }

    private IEnumerator ReloadCurrentSceneWithDelay()
    {
        yield return new WaitForSeconds(reloadDelay);
        string sceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(sceneName);
    }
}
