using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveLoadManager : MonoBehaviour
{
    [Header("References")]
    public Transform player;
    public EnemySpawner spawner;
    public ScoreManager scoreManager;

    private TransformSaver transformSaver;
    private ScoreSaver scoreSaver;

    void Start()
    {
        if (player == null)
            Debug.LogError("SaveLoadManager: Player reference is missing!");
        if (spawner == null)
            Debug.LogError("SaveLoadManager: EnemySpawner reference is missing!");
        if (scoreManager == null)
            Debug.LogError("SaveLoadManager: ScoreManager reference is missing!");

        transformSaver = new TransformSaver(player, "Enemy");
        scoreSaver = new ScoreSaver();

        
        scoreManager.SetScore(0);

        
        Debug.Log("Starting new game. Score = 0");
    }

    void Update()
    {
        
        if (scoreManager != null)
            scoreSaver.currentScore = scoreManager.GetScore();

        
        if (Input.GetKeyDown(KeyCode.S))
        {
            transformSaver.Save();
            scoreSaver.Save();
            Debug.Log("Game Saved!");
        }

        
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadGame();
        }
    }

    private void LoadGame()
    {
        if (scoreSaver != null && scoreManager != null)
        {
            scoreSaver.Load();                      
            scoreManager.SetScore(scoreSaver.currentScore); 
        }

        if (transformSaver != null && spawner != null)
        {
            List<Vector3> enemyPositions = transformSaver.LoadEnemyPositions();
            spawner.LoadEnemies(enemyPositions); 
        }

        Debug.Log($"Game Loaded! Score: {scoreSaver.currentScore}");
    }
}