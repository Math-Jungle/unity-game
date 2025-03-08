using UnityEngine;
using UnityEngine.tvOS;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public LocalDataManager localDataManager;
    public BackendDataManager remoteDataManager;
    public SyncManager syncManager;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Keeps GameManager alive across scenes
        }
        else
        {
            Destroy(gameObject); // Prevents duplicate GameManagers
            return;
        }

        // Ensure managers are assigned
        localDataManager = GetComponent<LocalDataManager>();
        remoteDataManager = GetComponent<BackendDataManager>();
        syncManager = GetComponent<SyncManager>();

        if (localDataManager == null || remoteDataManager == null || syncManager == null)
        {
            Debug.LogError("One or more manager components are missing on GameManager!");
        }
    }
}
