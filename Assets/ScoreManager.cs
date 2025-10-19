using UnityEngine;
using TMPro; 

public class ScoreManager : MonoBehaviour
{
    public TMP_Text scoreText; 
    private int score = 0;

    void Awake()
    {
        score = 0;
        UpdateScoreText();
    }

    void OnEnable()
    {
        Enemy.OnEnemyKilled += HandleEnemyKilled;
    }

    void OnDisable()
    {
        Enemy.OnEnemyKilled -= HandleEnemyKilled;
    }

    void HandleEnemyKilled(Enemy enemy)
    {
        score++;
        UpdateScoreText();
    }

    void UpdateScoreText()
    {
        if (scoreText != null)
            scoreText.text = "Score: " + score;
    }
}