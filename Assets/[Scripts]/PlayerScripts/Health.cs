// Author: Mithul Koshy
using Mirror;
using UnityEngine;

public class Health : NetworkBehaviour
{
    [SyncVar(hook = nameof(OnHealthChanged))]
    public ValuePool lifepool; // Assuming this is your character's health pool
    public float reloadDelay = 5.0f; // Time in seconds before the scene reloads

    [SerializeField]
    float healthRegen;//will need to get this from calculator later
    // You may want to set these in the inspector or in a Start() method if they are constant

    [SerializeField]
    int experience;

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
                    maxValue = 100000f, // Player has more health, for example
                    currentValue = 100000f
                };
                experience = 0;
            }
            else if (gameObject.CompareTag("EnemyEasy"))
            {
                lifepool = new ValuePool
                {
                    maxValue = 100f, // Enemy has less health
                    currentValue = 100f
                };
                experience = 8;
            }

            else if (gameObject.CompareTag("EnemyMedium"))
            {
                lifepool = new ValuePool
                {
                    maxValue = 200f, // Enemy has less health
                    currentValue = 200f
                };
                experience = 14;
            }

            else if (gameObject.CompareTag("EnemyHard"))
            {
                lifepool = new ValuePool
                {
                    maxValue = 300f, // Enemy has less health
                    currentValue = 300f
                };
                experience = 25;
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
        if (isOwned)
        {
            CmdApplyHealthRegen();
        }
    }

    // This method is used to apply damage to the character
    public void TakeDamage(float damageAmount)
    {
        lifepool.currentValue -= damageAmount;
        //Debug.Log("Lifepool: " + lifepool.currentValue);
        RpcUpdateHealth(lifepool.currentValue, lifepool.maxValue);
        if (lifepool.currentValue <= 0)
        {
            Die(); // Call the death method if health goes to 0 or below
        }
    }

    [Command]
    private void CmdApplyHealthRegen()
    {
        if (lifepool.currentValue < lifepool.maxValue)
        {
            lifepool.currentValue += healthRegen * Time.deltaTime;
            RpcUpdateHealth(lifepool.currentValue, lifepool.maxValue);
        }
    }

    [Command(requiresAuthority = false)]
    public void CmdHealPlayer(float value)
    {
        lifepool.currentValue = Mathf.Clamp(value + lifepool.currentValue, 0, lifepool.maxValue);
    }

    /// <summary>
    /// Commmand to set the max health of the player character
    /// </summary>
    [Command]
    public void CmdSetupHealth(float maxHealth, float regen)
    {
        lifepool.maxValue = maxHealth;
        lifepool.currentValue = lifepool.maxValue;
        healthRegen = regen;
        RpcUpdateHealth(lifepool.currentValue, lifepool.maxValue);
    }

    [ClientRpc]
    private void RpcUpdateHealth(float newHealthValue, float newMaxHealth)
    {
        lifepool.currentValue = newHealthValue;
        lifepool.maxValue = newMaxHealth;
    }

    private void OnHealthChanged(ValuePool oldPool, ValuePool newPool)
    {
        // Add any client-side logic here, such as updating UI elements or triggering visual effects.
        Debug.Log("Health changed on client. New value: " + newPool.currentValue);
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
            ExperienceManager.Instance.AddExperience(experience); // Replace 50 with the actual experience value you want to give
            Debug.Log("addedExp");
        }
        //ReloadCurrentSceneWithDelay();
        //Destroy(gameObject, 2f); // Waits for 2 seconds before destroying the game object

        // Spawn potion from enemy position
        if (!gameObject.CompareTag("Player"))
        {
            ItemController.Instance.SpawnPotion(transform.position);
        }

        // will need to implement the waiting some other way
        DestroySelf();
    }


    [Server]
    void DestroySelf()
    {
        NetworkServer.Destroy(gameObject);
    }

/*    private IEnumerator ReloadCurrentSceneWithDelay()
    {
        yield return new WaitForSeconds(reloadDelay);
        string sceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(sceneName);
    }*/
}
