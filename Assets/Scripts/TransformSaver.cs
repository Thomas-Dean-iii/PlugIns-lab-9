using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class TransformData
{
    public Vector3 position;
}

[System.Serializable]
public class TransformListWrapper
{
    public List<TransformData> transforms = new List<TransformData>();
}

public class TransformSaver : ISaveable
{
    private string savePath;
    private Transform player;
    private string enemyTag;

    public TransformSaver(Transform playerTransform, string enemyTag = "Enemy", string fileName = "transforms.json")
    {
        player = playerTransform;
        this.enemyTag = enemyTag;
        savePath = Path.Combine(Application.persistentDataPath, fileName);
    }


    public void Save()
    {
        List<TransformData> data = new List<TransformData>();

        // Save player position as first entry
        if (player != null)
            data.Add(new TransformData { position = player.position });
        else
            Debug.LogWarning("TransformSaver: player reference is null when saving.");

        // Save all enemies found by tag
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        foreach (GameObject enemy in enemies)
        {
            data.Add(new TransformData { position = enemy.transform.position });
        }

        TransformListWrapper wrapper = new TransformListWrapper { transforms = data };
        string json = JsonUtility.ToJson(wrapper, true);
        File.WriteAllText(savePath, json);

        Debug.Log($"Transforms saved ({data.Count - 1} enemies + player) to: {savePath}");
    }

    public void Load()
    {
        if (!File.Exists(savePath))
        {
            Debug.LogWarning("TransformSaver.Load: No transform save file found.");
            return;
        }

        string json = File.ReadAllText(savePath);
        TransformListWrapper wrapper = JsonUtility.FromJson<TransformListWrapper>(json);

        if (wrapper == null || wrapper.transforms == null || wrapper.transforms.Count == 0)
        {
            Debug.LogWarning("TransformSaver.Load: Save file empty or malformed.");
            return;
        }

        if (player != null)
        {
            player.position = wrapper.transforms[0].position;
        }
        else
        {
            Debug.LogWarning("TransformSaver.Load: player reference is null.");
        }

        Debug.Log("TransformSaver.Load: player position restored. Enemy positions available via LoadEnemyPositions().");
    }

 
    public List<Vector3> LoadEnemyPositions()
    {
        List<Vector3> enemyPositions = new List<Vector3>();

        if (!File.Exists(savePath))
        {
            Debug.LogWarning("TransformSaver.LoadEnemyPositions: No transform save file found.");
            return enemyPositions;
        }

        string json = File.ReadAllText(savePath);
        TransformListWrapper wrapper = JsonUtility.FromJson<TransformListWrapper>(json);

        if (wrapper == null || wrapper.transforms == null || wrapper.transforms.Count == 0)
        {
            Debug.LogWarning("TransformSaver.LoadEnemyPositions: Save file empty or malformed.");
            return enemyPositions;
        }

        if (player != null)
        {
            player.position = wrapper.transforms[0].position;
        }

        for (int i = 1; i < wrapper.transforms.Count; i++)
        {
            enemyPositions.Add(wrapper.transforms[i].position);
        }

        Debug.Log($"TransformSaver.LoadEnemyPositions: loaded {enemyPositions.Count} enemy positions from {savePath}");
        return enemyPositions;
    }
}