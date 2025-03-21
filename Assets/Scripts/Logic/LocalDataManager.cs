using System.IO;
using UnityEngine;

public class LocalDataManager : MonoBehaviour
{
    private string filePath;
    private string userDataFilePath;
    void Awake()
    {
        filePath = Path.Combine(Application.persistentDataPath, "GameData.json");
        userDataFilePath = Path.Combine(Application.persistentDataPath, "UserData.json");
    }

    // Save game dta locally as a JSON file
    public void SaveGameData(GameData data)
    {
        string json = JsonUtility.ToJson(data);

        try
        {
            File.WriteAllText(filePath, json);
            Debug.Log("Game data saved to local storage: " + json);
        }
        catch (IOException e)
        {
            Debug.LogError("Error while writing game data to the file." + e.Message);
        }
    }

    // Load game data from local storage
    public GameData LoadGameData()
    {
        if (File.Exists(filePath))
        {
            string json;
            try
            {
                json = File.ReadAllText(filePath);
            }
            catch (FileLoadException e)
            {
                Debug.LogError("Error while loading game data from the file." + e.Message);
                return null;
            }

            GameData data = JsonUtility.FromJson<GameData>(json);
            Debug.Log("Local game data loaded: " + json);
            return data;

            // Check if the dat exists in the game file
        }
        else
        {
            Debug.Log("No local game data file found.");
            return null;
        }
    }
    // New: Save user data locally
    public void SaveUserData(UserData data)
    {
        string json = JsonUtility.ToJson(data);
        try
        {
            File.WriteAllText(userDataFilePath, json);
            Debug.Log("User data saved to local storage: " + json);
        }
        catch (IOException e)
        {
            Debug.LogError("Error while writing user data to file: " + e.Message);
        }
    }

    // New: Load user data from local storage
    public UserData LoadUserData()
    {
        if (File.Exists(userDataFilePath))
        {
            try
            {
                string json = File.ReadAllText(userDataFilePath);
                UserData data = JsonUtility.FromJson<UserData>(json);
                Debug.Log("Local user data loaded: " + json);
                return data;
            }
            catch (IOException e)
            {
                Debug.LogError("Error while loading user data from file: " + e.Message);
                return null;
            }
        }
        else
        {
            Debug.Log("No local user data file found.");
            return null;
        }
    }

}
