using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public TMP_Text scoreText;
    private int score = 0;

    void Awake()
    {
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

    
    public void SetScore(int newScore)
    {
        score = newScore;
        UpdateScoreText();
    }

    
    public int GetScore()
    {
        return score;
    }
}
