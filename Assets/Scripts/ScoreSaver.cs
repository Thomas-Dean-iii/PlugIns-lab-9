using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

[System.Serializable]
public class ScoreData
{
    public int score;
}

public class ScoreSaver : IsSaveable
{
    public int currentScore;
    private string savePath;
    
    public ScoreSaver(string fileName = "score.dat")
    {
        savePath = Path.Combine(Application.persistentDataPath, fileName);
    }

    public void Save()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        using (FileStream file = File.Create(savePath))
        {
            ScoreData data = new ScoreData { score = currentScore };
            formatter.Serialize(file, data);
        }

        Debug.Log($"Score saved: {currentScore} to {savePath}");
    }

    public void Load()
    {
        if (!File.Exists(savePath))
        {
            Debug.LogWarning("No score save file found.");
            return;
        }

        BinaryFormatter formatter = new BinaryFormatter();
        using (FileStream file = File.Open(savePath, FileMode.Open))
        {
            ScoreData data = (ScoreData)formatter.Deserialize(file);
            currentScore = data.score;
        }

        Debug.Log($"Score loaded: {currentScore}");
    }
}
