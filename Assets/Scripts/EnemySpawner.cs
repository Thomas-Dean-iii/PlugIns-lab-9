using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    public GameObject enemyPrefab;

    [Header("Spawn Area")]
    public float minX = -8f;
    public float maxX = 8f;
    public float minY = 3f;
    public float maxY = 6f;

    [Header("Behavior Settings")]
    public float patrolDistance = 2f;
    public float minSpeed = 1.5f;
    public float maxSpeed = 3f;

    [Header("Spawn Timing")]
    public float minSpawnDelay = 1f;
    public float maxSpawnDelay = 5f;
    public int maxEnemiesOnScreen = 10;

    private List<Enemy> activeEnemies = new List<Enemy>();

    private Coroutine spawnRoutine;

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
        spawnRoutine = StartCoroutine(SpawnEnemiesRandomly());
    }

    IEnumerator SpawnEnemiesRandomly()
    {
        while (true)
        {
            float delay = Random.Range(minSpawnDelay, maxSpawnDelay);
            yield return new WaitForSeconds(delay);

            if (activeEnemies.Count < maxEnemiesOnScreen)
            {
                SpawnEnemyAtRandom();
            }
        }
    }

    private void SpawnEnemyAtRandom()
    {
        float xPos = Random.Range(minX, maxX);
        float yPos = Random.Range(minY, maxY);
        Vector3 spawnPos = new Vector3(xPos, yPos, 0f);

        SpawnEnemy(spawnPos);
    }

    public void SpawnEnemy(Vector3 spawnPos)
    {
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

   
    public void LoadEnemies(List<Vector3> positions)
    {
        
        if (spawnRoutine != null)
        {
            StopCoroutine(spawnRoutine);
            spawnRoutine = null;
        }

        
        foreach (var enemy in new List<Enemy>(activeEnemies))
        {
            if (enemy != null)
                Destroy(enemy.gameObject);
        }
        activeEnemies.Clear();

        
        foreach (var pos in positions)
        {
            SpawnEnemy(pos);
        }

        Debug.Log($"Spawned {activeEnemies.Count} enemies from saved data.");

        
        spawnRoutine = StartCoroutine(SpawnEnemiesRandomly());
    }
}
