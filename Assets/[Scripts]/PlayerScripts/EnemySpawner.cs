using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;          // The enemy prefab to spawn.
    public Transform spawnPoint;            // The position where enemies will spawn.
    public float spawnInterval = 2.0f;      // The time interval between spawns.
    public int maxEnemies = 10;             // The maximum number of enemies to spawn.

    private int currentEnemyCount = 0;      // The current number of spawned enemies.

    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            // Check if the maximum number of enemies has been reached.
            if (currentEnemyCount < maxEnemies)
            {
                // Spawn an enemy at the specified spawn point.
                Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
                currentEnemyCount++;
            }

            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
