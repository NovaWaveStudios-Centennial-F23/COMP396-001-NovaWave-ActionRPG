// Author: Mithul Koshy
using UnityEngine;

public class Health : MonoBehaviour
{
    public ValuePool lifepool; // Assuming this is your character's health pool
    public float reloadDelay = 5.0f; // Time in seconds before the scene reloads

    [SerializeField]
    float healthRegen;//will need to get this from calculator later
    // You may want to set these in the inspector or in a Start() method if they are constant

    public enum CharacterState
    {
        Alive,
        Dead,
    }

    public CharacterState currentState = CharacterState.Alive;

    private void Start()
    {
        {
            // Initialize lifepool differently based on the tag of the GameObject
            if (gameObject.CompareTag("Player"))
            {
                lifepool = new ValuePool
                {
                    maxValue = 1000f, // Player has more health, for example
                    currentValue = 1000f
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

    private void Update()
    {
        if(lifepool.currentValue < lifepool.maxValue)
        {
            lifepool.currentValue += healthRegen * Time.deltaTime;
        }
    }

    // This method is used to apply damage to the character
    public void TakeDamage(float damageAmount)
    {
        lifepool.currentValue -= damageAmount;
        //Debug.Log("Lifepool: " + lifepool.currentValue);

        if (lifepool.currentValue <= 0)
        {
            Die(); // Call the death method if health goes to 0 or below
        }
    }

    // Method to handle death logic
    private void Die()
    {

        currentState = CharacterState.Dead;
        //StartCoroutine(ReloadCurrentSceneWithDelay());
        Debug.Log(gameObject.name + " has died.");

        // Play death animation
        Animator animator = GetComponent<Animator>();
        if (animator != null)
        {
            animator.SetTrigger("Die");
         
            
        }

        // Add XP to player's experience
        if (ExperienceManager.Instance != null)
        {
            ExperienceManager.Instance.AddExperience(5); // Replace 50 with the actual experience value you want to give
            Debug.Log("addedExp");
        }
        //ReloadCurrentSceneWithDelay();
         Destroy(gameObject, 2f); // Waits for 2 seconds before destroying the game object
    }

/*    private IEnumerator ReloadCurrentSceneWithDelay()
    {
        yield return new WaitForSeconds(reloadDelay);
        string sceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(sceneName);
    }*/
}
