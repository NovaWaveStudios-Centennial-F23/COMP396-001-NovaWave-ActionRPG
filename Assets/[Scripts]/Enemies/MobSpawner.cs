// Author: Mithul Koshy
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobSpawner : MonoBehaviour
{
    public GameObject enemyPrefab1;
    public GameObject enemyPrefab2;
    public GameObject enemyPrefab3;

    public int numGroups = 5; // Number of enemy groups to spawn
    public int enemiesPerGroup = 3; // Number of enemies per group
    public float groupSpacing = 2.0f; // Distance between enemy groups
    public float zigzagSpacing = 1.0f; // Distance between enemies in the zigzag formation
    public Vector3 spawnAreaCenter; // Center of the spawn area
    public Vector3 spawnAreaSize; // Size of the spawn area

    private List<GameObject> enemies = new List<GameObject>();

    void Start()
    {
        SpawnEnemies();
    }

    void Update()
    {
        // Check if any enemy is in range of the player and activate them
        foreach (GameObject enemy in enemies)
        {
            if (enemy != null && enemy.activeSelf)
            {
                // Make sure the enemy has an EnemyAI component before accessing it
                EnemyAI enemyAI = enemy.GetComponent<EnemyAI>();
                if (enemyAI != null)
                {
                    float distance = Vector3.Distance(enemy.transform.position, PlayerPosition());
                    if (distance <= enemyAI.attackRange)
                    {
                        enemyAI.Activate();
                    }
                }
            }
        }
    }

    Vector3 PlayerPosition()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            return player.transform.position;
        }
        return Vector3.zero;
    }

    void SpawnEnemies()
    {
        for (int i = 0; i < numGroups; i++)
        {
            for (int j = 0; j < enemiesPerGroup; j++)
            {
                Vector3 randomPosition = GetRandomSpawnPosition();

                // Randomly choose an enemy prefab to spawn
                GameObject enemyPrefab = GetRandomEnemyPrefab();

                GameObject enemy = Instantiate(enemyPrefab, randomPosition, Quaternion.identity);
                enemies.Add(enemy);
                enemy.SetActive(true); // Initially, enemies are idle
            }
        }
    }

    GameObject GetRandomEnemyPrefab()
    {
        int randomIndex = Random.Range(0, 3); // Change 3 to the number of enemy prefabs you have
        switch (randomIndex)
        {
            case 0:
                return enemyPrefab1;
            case 1:
                return enemyPrefab2;
            case 2:
                return enemyPrefab3;
            default:
                return enemyPrefab1;
        }
    }

    Vector3 GetRandomSpawnPosition()
    {
        Vector3 minBounds = spawnAreaCenter - spawnAreaSize * 0.5f;
        Vector3 maxBounds = spawnAreaCenter + spawnAreaSize * 0.5f;

        float randomX = Random.Range(minBounds.x, maxBounds.x);
        float randomY = Random.Range(minBounds.y, maxBounds.y);
        float randomZ = Random.Range(minBounds.z, maxBounds.z);

        return new Vector3(randomX, randomY, randomZ);
    }
}
