using UnityEngine;
using System.Collections;

public class CloudGeneraterScript : MonoBehaviour
{
    [SerializeField] private GameObject[] clouds;
    [SerializeField] private float spawnInterval = 2f;
    [SerializeField] private GameObject endPoint;
    [SerializeField] private int maxClouds = 6; // Limit clouds to avoid overpopulation 
    private Vector3 startPos;
    private int currentCloudCount = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPos = transform.position;
        Prewarm();
        StartCoroutine(SpawnCloudsRoutine());
        // Invoke("AttemptSpawn", spawnInterval);
    }

    private void SpawnCloud(Vector3 spawnPos)
    {
        if(currentCloudCount >= maxClouds) return; // Limit active clouds

        int randomIndex = Random.Range(0, clouds.Length);
        GameObject cloud = Instantiate(clouds[randomIndex]);

        float startY = Random.Range(startPos.y - 1.5f, startPos.y + 1.5f);
        cloud.transform.position = new Vector3(startPos.x, startY, startPos.z);

        float scale = Random.Range(0.8f, 1.4f);
        cloud.transform.localScale = new Vector3(scale, scale, scale);

        float speed = Random.Range(0.5f, 2f);
       
        cloud.GetComponent<CloudScript>().StartFloating(speed, endPoint.transform.position.x);

        currentCloudCount++;
        StartCoroutine(DestroyCloud(cloud, 20f));
        // cloud.GetComponent<CloudScript>().Invoke(nameof(DestroyCloud), 20f); // Ensures cleanup

    }   

    // void AttemptSpawn()
    // {
    //     //Check some things.
    //     SpawnCloud(startPos);

    //     Invoke("AttemptSpawn", spawnInterval);
    // }

    private IEnumerator SpawnCloudsRoutine()
    {
        while(true)
        {
            SpawnCloud(startPos);
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void Prewarm()
    {
        for(int i=0; i < 5; i++) // Adjusted to 5 for better distribution
        {
            Vector3 spawnPos = startPos + Vector3.right * (i * 3);
            SpawnCloud(spawnPos);
        }
    }

    private IEnumerator DestroyCloud(GameObject cloud, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(cloud);
        currentCloudCount--;
    }
}
