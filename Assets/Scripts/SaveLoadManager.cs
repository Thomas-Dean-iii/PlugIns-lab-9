using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoadManager : MonoBehaviour
{
    public Transform player;
    //public List<Transform> enemies = new List<Transform>();
    public int score = 0;

    private TransformSaver transformSaver;
    private ScoreSaver scoreSaver;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize savers
        transformSaver = new TransformSaver(player, "Enemy");
        scoreSaver = new ScoreSaver();
        scoreSaver.currentScore = score;
    }

    // Update is called once per frame
    void Update()
    {
        // Update current score in saver
        scoreSaver.currentScore = score;

        if (Input.GetKeyDown(KeyCode.S))
        {
            transformSaver.Save();
            scoreSaver.Save();
            Debug.Log("Game Saved!");
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            transformSaver.Load();
            scoreSaver.Load();
            score = scoreSaver.currentScore;
            Debug.Log("Game Loaded!" + score);
        }
    }

}
