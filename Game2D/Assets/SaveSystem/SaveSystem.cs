using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveSystem : MonoBehaviour
{
    private string saveFilePath;

    private void Awake()
    {
        // Определяем путь к файлу сохранения
        saveFilePath = Path.Combine(Application.persistentDataPath, "saveData.json");
    }

    public void Save(string sceneName, float health, bool visited, Vector3 coordinates, List<string> talkedWith)
    {
        saveFilePath = Path.Combine(Application.persistentDataPath, "saveData.json");
        SaveData data = new SaveData
        {
            sceneName = sceneName,
            health = health,
            visited = visited,
            coordinates = coordinates,
            talked = talkedWith
        };

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(saveFilePath, json);
        Debug.Log("Данные сохранены: " + json);
    }

    public SaveData Load()
    {
        saveFilePath = Path.Combine(Application.persistentDataPath, "saveData.json");
        if (File.Exists(saveFilePath))
        {
            string json = File.ReadAllText(saveFilePath);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            //Debug.Log("Данные загружены: " + json);
            return data;
        }
        else
        {
            Debug.LogWarning("Файл сохранения не найден. Создаем новый файл с начальными данными.");

            // Создание начальных данных
            SaveData initialData = new SaveData
            {
                sceneName = "",
                health = 0,
                visited = false,
                coordinates = new Vector3(0, 0, 0),
                talked = new List<string>()
            };

            // Сохранение начальных данных в файл
            string json = JsonUtility.ToJson(initialData, true);
            File.WriteAllText(saveFilePath, json);
            Debug.Log("Начальные данные сохранены: " + json);

            return initialData; // Возвращаем начальные данные
        }
    }

    public static SaveData LoadStatic()
    {
        string saveFilePat1h = Path.Combine(Application.persistentDataPath, "saveData.json");
        if (File.Exists(saveFilePat1h))
        {
            string json = File.ReadAllText(saveFilePat1h);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            //Debug.Log("Данные загружены: " + json);
            return data;
        }
        else
        {
            Debug.LogWarning("Файл сохранения не найден.");
            return null;
        }
    }
}

[System.Serializable]
public class SaveData
{
    public string sceneName;
    public float health;
    public bool visited;
    public Vector3 coordinates;
    public List<string> talked;
}
