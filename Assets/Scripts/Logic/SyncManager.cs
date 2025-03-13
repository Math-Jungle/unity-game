using System;
using UnityEngine;

public class SyncManager : MonoBehaviour
{
    public LocalDataManager localDataManager;
    public BackendDataManager backendDataManager;

    // Call this method when internet is available
    // 
    public void SyncData(string jwtToken)
    {
        GameData localData = localDataManager.LoadGameData();


        StartCoroutine(backendDataManager.FetchGameData(jwtToken, (backendData) =>
        /* 
            this function executes when the FetchGameData method return backendData As a GameData object.
        */
        {
            if (backendData == null)
            {
                Debug.LogError("Backend Game data wasn't recieved to Sync Data.");
                return;
            }

            if (localData == null && backendData != null) // when the application first downloaded and the user already has an account or when the app data is erased.
            {
                Debug.Log("Local data missing; saving backend data locally.");
                localDataManager.SaveGameData(backendData);
            }
            else if (localData != null && backendData == null) // when the backend doesn't contain game data that the frontend game contain . Likely won't happen.
            {
                Debug.Log("No backend data; updating backend with local data.");

                StartCoroutine(backendDataManager.SendGameData(localData, jwtToken));
            }
            else if (localData != null && backendData != null) // when the back end and the front end both contains gamedata but the version is different.
            {
                DateTime localTime = DateTime.Parse(localData.lastUpdated);
                DateTime backendTime = DateTime.Parse(backendData.lastUpdated);

                if (localTime > backendTime)// Local storage contains newer data. Most frequent case. when the game isn't connected to the internet and stored data only in local storage
                {
                    Debug.Log("Local data is newer. Updating backend.");

                    StartCoroutine(backendDataManager.SendGameData(localData, jwtToken));
                }
                else if (localTime < backendTime)// Backend  storage contains newer data.  In edge cases where user plays the game from a differnt device.
                {
                    Debug.Log("Backend data is newer. Updating local storage.");
                    localDataManager.SaveGameData(backendData);
                }
                else
                {
                    Debug.Log("Data is already synced.");
                }
            }
        }));
    }
}
