using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class BackendDataManager : MonoBehaviour
{
    private readonly string sendDataUrl = "";
    private readonly string fetchDataUrl = "";

    // Send all of game data to the backend
    public IEnumerator SendGameData(string jsonData, string jwtToken)
    {

        // string jwtToken = PlayerPrefs.GetString("UserToken", "");

        // if (string.IsNullOrEmpty(jwtToken))
        // {
        //     Debug.LogError("No JWT Token found! Cannot send game data.");
        //     yield break;
        // }

        UnityWebRequest request = new UnityWebRequest(sendDataUrl, "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer(); // Need to store recieved response // Not sure
        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("Authorization", "Bearer " + jwtToken);

        yield return request.SendWebRequest(); // Waiting for response

        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Game data sent successfully: " + request.downloadHandler.text);
        }
        else
        {
            Debug.LogError("Error sending game data: " + request.error);
        }
    }

    // Fetch game data from the backend
    public IEnumerator FetchGameData(string jwtToken, System.Action<GameData> onFetched)

    /*
        "System.Action<GameData> onFetched"  is a trigger for an method. When calling the FetchGameData method,
        we can pass a method that needs the GameData object as a prameter.
        When the data is fetched from the backend that method executes
    */
    {
        // string jwtToken = PlayerPrefs.GetString("UserToken", "");

        // if (string.IsNullOrEmpty(jwtToken))
        // {
        //     Debug.LogError("No JWT Token found! Cannot send game data.");
        //     yield break;
        // }


        UnityWebRequest request = UnityWebRequest.Get(fetchDataUrl);
        request.SetRequestHeader("Authorization", "Bearer " + jwtToken);

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            string json = request.downloadHandler.text;
            GameData backendData = JsonUtility.FromJson<GameData>(json);
            onFetched?.Invoke(backendData);
        }
        else
        {
            Debug.LogError("Error fetching game dta: " + request.error);
            onFetched?.Invoke(null); // Search about this.
        }
    }
}
