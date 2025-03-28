using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class BackendDataManager : MonoBehaviour
{
    private readonly string sendDataUrl = "https://spring-app-249115746984.asia-south1.run.app/dashboard/save-userdata";
    private readonly string fetchDataUrl = "https://spring-app-249115746984.asia-south1.run.app/dashboard/get-userdata";
    private readonly string validateTokenUrl = "https://spring-app-249115746984.asia-south1.run.app/user/verifyJWT";
    private readonly string userdetails = "https://spring-app-249115746984.asia-south1.run.app/user/details";
    private bool isSendingData = false; // Flag to prevent multiple data sends

    // Send all of game data to the backend
    public IEnumerator SendGameData(GameData gameData, string jwtToken)
    {
        if (isSendingData)
        {
            Debug.LogWarning("Already sending data. Please wait.");
            yield break;
        }

        isSendingData = true;

        if (string.IsNullOrEmpty(jwtToken))
        {
            Debug.LogWarning("No JWT Token found! Continuing game without sending data.");
            isSendingData = false;
            yield break;
        }

        string jsonData = JsonUtility.ToJson(gameData);

        UnityWebRequest request = new UnityWebRequest(sendDataUrl, "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("Authorization", "Bearer " + jwtToken);

        // The yield happens outside of try/catch
        yield return request.SendWebRequest();

        // Handle the result after the request completes
        try
        {
            if (request.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("Game data sent successfully: " + request.downloadHandler.text);
            }
            else
            {
                // Log the error but don't throw exceptions
                HandleRequestError(request);
                Debug.LogWarning("Failed to send game data to backend, but game will continue.");
                yield break;
            }
        }
        catch (System.Exception e)
        {
            // Catch any unexpected exceptions to prevent game from breaking
            Debug.LogWarning($"Exception while processing response: {e.Message}. Game will continue.");
        }
        finally
        {
            // Always reset the sending flag, even if errors occur
            isSendingData = false;
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

        if (string.IsNullOrEmpty(jwtToken))
        {
            Debug.LogError("No JWT Token found! Cannot fetch game data.");
            yield break;
        }


        UnityWebRequest request = UnityWebRequest.Get(fetchDataUrl);
        request.SetRequestHeader("Authorization", "Bearer " + jwtToken);

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            string json = request.downloadHandler.text;

            if (string.IsNullOrEmpty(json) || json == "null")
            {
                Debug.LogWarning("Backend returned empty or null data.");
                onFetched?.Invoke(null);
            }
            else
            {
                try
                {
                    GameData backendData = JsonUtility.FromJson<GameData>(json);
                    if (backendData != null)
                    {
                        onFetched?.Invoke(backendData);
                    }
                    else
                    {
                        Debug.LogError("Failed to parse backend data.");
                        onFetched?.Invoke(null);
                    }
                }
                catch (System.Exception e)
                {
                    Debug.LogError("JSON parsing error: " + e.Message);
                    onFetched?.Invoke(null);
                }
            }
        }
        else
        {
            //HandleRequestError(request);
            Debug.LogError("Failed to fetch game data: ");
            onFetched?.Invoke(null);
        }
    }

    // Handles various errors from UnityWebRequest
    private void HandleRequestError(UnityWebRequest request)
    {
        switch (request.result)
        {
            case UnityWebRequest.Result.ConnectionError:
                Debug.LogError("Network connection error: " + request.error);
                break;
            case UnityWebRequest.Result.ProtocolError:
                Debug.LogError("HTTP error: " + request.responseCode + " - " + request.error);
                break;
            case UnityWebRequest.Result.DataProcessingError:
                Debug.LogError("Data processing error: " + request.error);
                break;
            default:
                Debug.LogError("Unknown error: " + request.error);
                break;
        }
    }
    public IEnumerator FetchUserData(string jwtToken, System.Action<UserData> onFetched)
    {
        if (string.IsNullOrEmpty(jwtToken))
        {
            Debug.LogError("No JWT Token found! Cannot fetch user data.");
            onFetched?.Invoke(null);
            yield break;
        }       
       

        UnityWebRequest request = UnityWebRequest.Get(userdetails);
        request.SetRequestHeader("Authorization", "Bearer " + jwtToken);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            string json = request.downloadHandler.text;
            if (string.IsNullOrEmpty(json) || json == "null")
            {
                Debug.LogWarning("Backend returned empty or null user data.");
                onFetched?.Invoke(null);
            }
            else
            {
                try
                {
                    UserData userData = JsonUtility.FromJson<UserData>(json);
                    onFetched?.Invoke(userData);
                }
                catch (System.Exception e)
                {
                    Debug.LogError("JSON parsing error (UserData): " + e.Message);
                    onFetched?.Invoke(null);
                }
            }
        }
        else
        {
            Debug.LogError("Failed to fetch user data: " + request.error);
            onFetched?.Invoke(null);
        }
    }

    public IEnumerator VerifyToken(string jwtToken)
    {
        if (string.IsNullOrEmpty(jwtToken))
        {
            Debug.LogError("No JWT Token found! Cannot verify user.");
            yield return false;
            yield break;
        }

        UnityWebRequest request = UnityWebRequest.Get(validateTokenUrl);
        request.SetRequestHeader("Authorization", "Bearer " + jwtToken);

        Debug.Log("Verifying user token...");

        yield return request.SendWebRequest();

        bool isValid = request.result == UnityWebRequest.Result.Success;

        if (isValid)
        {
            Debug.Log("User Verified");
        }
        else
        {
            Debug.Log("JWT Token invalid or verification failed");
            // Clear invalid token
            PlayerPrefs.DeleteKey("AuthToken");
            PlayerPrefs.Save();
        }

        yield return isValid;

    }
}
