using UnityEngine;
using System.IO;

[System.Serializable]
public class SettingsData
{
    public float VolumeSetting = 1.0f;
    public int Background = 1;
}

public class SaveSettings : MonoBehaviour
{
    public static float VolumeSetting;
    public static int Background;

    private string settingsFilePath;
    private SettingsData settingsData = new SettingsData();

    void Start()
    {
        settingsFilePath = Path.Combine(Application.persistentDataPath, "settings.json");
        LoadSettings();
    }

    void Update()
    {
        settingsData.VolumeSetting = VolumeValue.Volume; // Обновляем текущую громкость
        settingsData.Background = Background;            // Обновляем фон
        SaveSettingsToFile();
    }

    public void SetBg1Setting()
    {
        Background = 1;
    }

    public void SetBg2Setting()
    {
        Background = 2;
    }

    void LoadSettings()
    {
        if (File.Exists(settingsFilePath))
        {
            string json = File.ReadAllText(settingsFilePath);
            settingsData = JsonUtility.FromJson<SettingsData>(json);
            VolumeSetting = settingsData.VolumeSetting;
            Background = settingsData.Background;
            Debug.Log("Настройки загружены.");
        }
        else
        {
            SaveSettingsToFile();  // Если файла нет, создаем его
        }
    }

    void SaveSettingsToFile()
    {
        string json = JsonUtility.ToJson(settingsData, true);
        File.WriteAllText(settingsFilePath, json);
    }
}
