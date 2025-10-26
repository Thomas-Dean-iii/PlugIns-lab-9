using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class TransformData
{
    public string name;
    public Vector3 position;
}

public class TransformSaver : IsSaveable
{
    public List<Transform> objectsToSave = new List<Transform>();
    private string savePath;
    private Transform player;
    private string enemyTag;
    
    public TransformSaver(Transform playerTransform, string enemyTag = "Enemey", string fileName = "transforms.json")
    {
        player = playerTransform;
        this.enemyTag = enemyTag;
        savePath = Path.Combine(Application.persistentDataPath, fileName);
    }

    public void Save()
    {
        List<TransformData> data = new List<TransformData>();

        // Save player
        data.Add(new TransformData
        {
            name = player.name,
            position = player.position
        });

        // Find all enemies in the scene
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        foreach (GameObject enemy in enemies)
        {
            data.Add(new TransformData
            {
                name = enemy.name,
                position = enemy.transform.position
            });
        }

        // Write JSON
        string json = JsonUtility.ToJson(new TransformListWrapper { transforms = data }, true);
        File.WriteAllText(savePath, json);

        Debug.Log($"Transforms saved: {data.Count} objects");

    }

    public void Load()
    {
        if (!File.Exists(savePath))
        {
            Debug.LogWarning("No transforms save file found.");
            return;
        }

        string json = File.ReadAllText(savePath);
        TransformListWrapper wrapper = JsonUtility.FromJson<TransformListWrapper>(json);

        // Restore player and enemies
        foreach (var tData in wrapper.transforms)
        {
            GameObject obj = null;
            if (tData.name == player.name)
                obj = player.gameObject;
            else
                obj = GameObject.Find(tData.name);

            if (obj != null)
                obj.transform.position = tData.position;
        }

        Debug.Log("Transforms loaded.");

    }

    [System.Serializable]
    private class TransformListWrapper
    {
        public List<TransformData> transforms;
    }
}
