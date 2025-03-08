using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Text;

[System.Serializable]
public class RegistrationRequest
{
    public string email;
    public string password;
    public string childName;
    public int childAge;
    // Note: The backend expects "avatarId" (or adjust to "avatar" if you update your backend)
    public string avatar;
}

public class RegistrationManager : MonoBehaviour
{
    public IEnumerator RegisterNewUser(string email, string password, string childName, int childAge, string avatar)
    {
        // Create a registration request matching your backend DTO
        RegistrationRequest req = new RegistrationRequest
        {
            email = email,
            password = password,
            childName = childName,
            childAge = childAge,
            avatar = avatar
        };

        // Convert the request object to JSON
        string jsonData = JsonUtility.ToJson(req);

        // Update the URL to your Spring Boot backend endpoint
        string url = "http://localhost:8080/api/register";
        UnityWebRequest www = new UnityWebRequest(url, "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonData);
        www.uploadHandler = new UploadHandlerRaw(bodyRaw);
        www.downloadHandler = new DownloadHandlerBuffer();

        // Set the content type header
        www.SetRequestHeader("Content-Type", "application/json");

        // Send the request and wait for the response
        yield return www.SendWebRequest();

        // Check the result
        if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError("Registration Error: " + www.error +
                           " | Response: " + www.downloadHandler.text);
        }
        else
        {
            Debug.Log("Registration successful! Response: " + www.downloadHandler.text);
            // Optionally, load a next scene on success
            // SceneManager.LoadScene("nextScene");
        }
    }
}
