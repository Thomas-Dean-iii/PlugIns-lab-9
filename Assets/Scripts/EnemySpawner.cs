using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    public GameObject enemyPrefab;

    public float minX = -8f;
    public float maxX = 8f;
    public float minY = 3f;  
    public float maxY = 6f;

    public float patrolDistance = 2f;

    public float minSpawnDelay = 1f;
    public float maxSpawnDelay = 5f;

    public float minSpeed = 1.5f;
    public float maxSpeed = 3f;

    public int maxEnemiesOnScreen = 10;

    private List<Enemy> activeEnemies = new List<Enemy>();

    void OnEnable()
    {
        Enemy.OnEnemyKilled += HandleEnemyKilled;
    }

    void OnDisable()
    {
        Enemy.OnEnemyKilled -= HandleEnemyKilled;
    }

    void Start()
    {
        StartCoroutine(SpawnEnemiesRandomly());
    }

    IEnumerator SpawnEnemiesRandomly()
    {
        while (true)
        {
            
            float delay = Random.Range(minSpawnDelay, maxSpawnDelay);
            yield return new WaitForSeconds(delay);

            
            if (activeEnemies.Count < maxEnemiesOnScreen)
            {
                SpawnEnemy();
            }
        }
    }

    void SpawnEnemy()
    {
       
        float xPos = Random.Range(minX, maxX);
        float yPos = Random.Range(minY, maxY);
        Vector3 spawnPos = new Vector3(xPos, yPos, 0f);

        Vector3 left = spawnPos + Vector3.left * patrolDistance;
        Vector3 right = spawnPos + Vector3.right * patrolDistance;

       
        EnemyBuilder builder = new EnemyBuilder();
        Enemy enemy = builder.CreateNewEnemy(enemyPrefab, spawnPos)
                             .WithSpeed(Random.Range(minSpeed, maxSpeed))
                             .WithPatrolPoints(left, right)
                             .Build();

        
        activeEnemies.Add(enemy);
    }

    void HandleEnemyKilled(Enemy deadEnemy)
    {
        
        if (activeEnemies.Contains(deadEnemy))
        {
            activeEnemies.Remove(deadEnemy);
        }
    }
}
