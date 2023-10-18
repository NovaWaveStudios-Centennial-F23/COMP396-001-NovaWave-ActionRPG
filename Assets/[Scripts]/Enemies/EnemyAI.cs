using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float movementSpeed = 3.0f; // Enemy movement speed
    public float attackRange = 2.0f; // Range at which the enemy attacks
    public float attackCooldown = 2.0f; // Cooldown between attacks
    public int attackDamage = 10; // Damage inflicted on the player

    private Transform player; // Reference to the player's Transform
    private float lastAttackTime; // Timestamp of the last attack
    private bool isActive = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (isActive)
        {
            // Check if the player is in range
            if (Vector3.Distance(transform.position, player.position) <= attackRange)
            {
                // Rotate towards the player
                Vector3 direction = (player.position - transform.position).normalized;
                Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5.0f);

                // Attack the player if enough time has passed since the last attack
                if (Time.time - lastAttackTime >= attackCooldown)
                {
                    Attack();
                    lastAttackTime = Time.time;
                }
            }
            else
            {
                // Move towards the player
                transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);
            }
        }
    }

    public void Activate()
    {
        isActive = true;
    }

    void Attack()
    {
     
    }
}
