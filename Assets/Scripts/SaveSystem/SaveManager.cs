using System.IO;
using UnityEngine;
using System.Collections.Generic;
using UnityEditor.Overlays;

[System.Serializable]
public class WorldData
{
    public int level;
}

[System.Serializable]
public class SaveData
{
    public Dictionary<string, WorldData> worlds = new Dictionary<string, WorldData>();
}

public class SaveManager : MonoBehaviour
{
    public static string savePath => Application.persistentDataPath + "/save.json";

    public static void SaveWorldProgress(string worldName, int level)
    {
        SaveData data = LoadSave();
        if (data.worlds.ContainsKey(worldName))
            data.worlds[worldName].level = level;
        else
            data.worlds.Add(worldName, new WorldData { level = level });

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(savePath, json);
        Debug.Log("Сохранено: " + savePath);
    }

    public static SaveData LoadSave()
    {
        if (!File.Exists(savePath))
            return new SaveData(); // вернёт null

        string json = File.ReadAllText(savePath);
        return JsonUtility.FromJson<SaveData>(json);
    }

    public static int GetWorldLevel(string worldName)
    {
        SaveData data = LoadSave();
        if (data.worlds.ContainsKey(worldName))
            return data.worlds[worldName].level;
        return 1;
    }
}
