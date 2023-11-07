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
        {
            // Initialize lifepool differently based on the tag of the GameObject
            if (gameObject.CompareTag("Player"))
            {
                lifepool = new ValuePool
                {
                    maxValue = 2000f, // Player has more health, for example
                    currentValue = 2000f
                };
            }
            else if (gameObject.CompareTag("Enemy"))
            {
                lifepool = new ValuePool
                {
                    maxValue = 100f, // Enemy has less health
                    currentValue = 100f
                };
            }
            else
            {
                lifepool = new ValuePool
                {
                    maxValue = 100f, // Default health value
                    currentValue = 100f
                };
            }
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
        //StartCoroutine(ReloadCurrentSceneWithDelay());
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
            Debug.Log("Exp",experienceManager);
        }
        //ReloadCurrentSceneWithDelay();
        // Destroy(gameObject, 2f); // Waits for 2 seconds before destroying the game object
    }

/*    private IEnumerator ReloadCurrentSceneWithDelay()
    {
        yield return new WaitForSeconds(reloadDelay);
        string sceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(sceneName);
    }*/
}
