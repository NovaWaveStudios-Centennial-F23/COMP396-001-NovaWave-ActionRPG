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
            if (enemy.activeSelf)
            {
                float distance = Vector3.Distance(enemy.transform.position, PlayerPosition());
                if (distance <= enemy.GetComponent<EnemyAI>().attackRange)
                {
                    enemy.GetComponent<EnemyAI>().Activate();
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
        Vector3 spawnPosition = transform.position;
        for (int i = 0; i < numGroups; i++)
        {
            for (int j = 0; j < enemiesPerGroup; j++)
            {
                Vector3 offset = Vector3.right * j * zigzagSpacing;
                if (j % 2 == 1)
                {
                    offset = -offset;
                }

                // Randomly choose an enemy prefab to spawn
                GameObject enemyPrefab = GetRandomEnemyPrefab();

                GameObject enemy = Instantiate(enemyPrefab, spawnPosition + offset, Quaternion.identity);
                enemies.Add(enemy);
                enemy.SetActive(true); // Initially, enemies are idle
            }

            // Adjust the spawn position for the next group
            spawnPosition += Vector3.forward * groupSpacing;
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
}
