using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;  // The enemy prefab to spawn
    public Transform spawnPoint;    // The point where enemies will spawn
    public int maxEnemies = 5;     // The maximum number of enemies to spawn
    public float spawnInterval = 2.0f; // Time interval between enemy spawns

    private float timer;

    private void Update()
    {
        // Check if it's time to spawn a new enemy
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            timer = 0f;

            // Spawn a random number of enemies between 1 and maxEnemies
            int numEnemiesToSpawn = Random.Range(1, maxEnemies + 1);

            for (int i = 0; i < numEnemiesToSpawn; i++)
            {
                SpawnEnemy();
            }
        }
    }

    private void SpawnEnemy()
    {
        if (enemyPrefab != null && spawnPoint != null)
        {
            // Instantiate the enemy at the spawn point with a random position offset
            Vector3 randomOffset = new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f));
            Vector3 spawnPosition = spawnPoint.position + randomOffset;

            GameObject newEnemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

            // You can add any additional enemy setup or customization here
        }
    }
}
