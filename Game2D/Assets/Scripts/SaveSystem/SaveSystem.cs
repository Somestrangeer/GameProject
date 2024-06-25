using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System.Text;


[System.Serializable]
public static class SaveData
{
    public static Vector3 position;
    public static float health;
    public static string sceneName = "ForestScene"; //beginning
    public static string gameProgress;
}

/*public class SaveSystem : MonoBehaviour
{
    private const string saveFileName = "saveFile.json";

    public static void SaveGame(Hero player)
    {
        //SaveData data = new SaveData();

        SaveData.position = player.transform.position;
        SaveData.health = player.getHp();
        SaveData.sceneName = SceneManager.GetActiveScene().name;
        SaveData.gameProgress = "???????????? ????????? ????????";

        // Convert data to JSON
        //string jsonString = JsonConvert.SerializeObject(data);

        // Write JSON to file
        //string path = Application.persistentDataPath + "/" + saveFileName;
        //File.WriteAllText(path, jsonString);

    }

    public static SaveData LoadGame()
    {
        string path = Application.persistentDataPath + "/" + saveFileName;
        if (File.Exists(path))
        {
            string jsonString = File.ReadAllText(path);

            // Deserialize JSON to object
            SaveData data = JsonConvert.DeserializeObject<SaveData>(jsonString);
            return data;
        }
        else
        {
            return null;
        }

    }

    public static void LoadAndRestore(Hero player)
    {
        //SaveData data = LoadGame();
        if (SaveData.sceneName != null)
        {
            SceneManager.LoadScene(SaveData.sceneName);

            while (!SceneManager.GetActiveScene().isLoaded) { }

            player.transform.position = SaveData.position;
            Hero.setHp(SaveData.health);
            
        }
    }

}*/
