using UnityEngine;


public class EnemyBuilder
{
    private GameObject enemyPrefab;      
    private Vector3 spawnPosition;       
    private float speed = 2f;            
    private Vector3 leftPatrolPoint;     
    private Vector3 rightPatrolPoint;    

    
    public EnemyBuilder CreateNewEnemy(GameObject prefab, Vector3 position)
    {
        enemyPrefab = prefab;
        spawnPosition = position;
        return this;
    }

    
    public EnemyBuilder WithSpeed(float patrolSpeed)
    {
        speed = patrolSpeed;
        return this;
    }

   
    public EnemyBuilder WithPatrolPoints(Vector3 left, Vector3 right)
    {
        leftPatrolPoint = left;
        rightPatrolPoint = right;
        return this;
    }

    
    public Enemy Build()
    {
        if (enemyPrefab == null)
        {
            Debug.LogError("EnemyBuilder: No enemy prefab assigned!");
            return null;
        }

        GameObject enemyObj = Object.Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

        Enemy enemyComponent = enemyObj.GetComponent<Enemy>();
        if (enemyComponent == null)
        {
            Debug.LogError("EnemyBuilder: Enemy prefab is missing the Enemy component!");
            return null;
        }

        
        enemyComponent.Initialize(speed, leftPatrolPoint, rightPatrolPoint);

        return enemyComponent;
    }
}