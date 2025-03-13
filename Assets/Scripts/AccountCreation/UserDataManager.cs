using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Text;
using System;

public class UserDataManager : MonoBehaviour
{
    private static UserDataManager _instance;
    public static UserDataManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject go = new GameObject("UserDataManager");
                _instance = go.AddComponent<UserDataManager>();
                DontDestroyOnLoad(go);
            }
            return _instance;
        }
    }

    private string _email;
    private string _password;
    private string _childName;
    private int _childAge;
    private string _avatarId;

    // Backend API URL - replace with your actual backend URL
    private const string API_URL = "http://localhost:8080/api/register";

    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void SetUserCredentials(string email, string password)
    {
        _email = email;
        _password = password;
        Debug.Log("User credentials set");
    }

    public void SetChildInfo(string childName, int childAge)
    {
        _childName = childName;
        _childAge = childAge;
        Debug.Log("Child info set: " + childName + ", " + childAge);
    }

    public void SetAvatarId(string avatarId)
    {
        _avatarId = avatarId;
        Debug.Log("Avatar selected: " + avatarId);
    }

    public IEnumerator RegisterUser(Action<bool, string> callback)
    {
        // Create JSON payload
        string jsonPayload = JsonUtility.ToJson(new RegisterRequest
        {
            email = _email,
            password = _password,
            childName = _childName,
            childAge = _childAge,
            avatarId = _avatarId
        });

        Debug.Log("Sending registration data: " + jsonPayload);

        // Create request
        UnityWebRequest request = new UnityWebRequest(API_URL, "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonPayload);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        // Send request
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Registration successful: " + request.downloadHandler.text);
            callback(true, request.downloadHandler.text);
        }
        else
        {
            Debug.LogError("Registration failed: " + request.error);
            callback(false, request.error);
        }
    }

    // Class to match the RegisterRequest expected by the backend
    [Serializable]
    private class RegisterRequest
    {
        public string email;
        public string password;
        public string childName;
        public int childAge;
        public string avatarId;
    }
}