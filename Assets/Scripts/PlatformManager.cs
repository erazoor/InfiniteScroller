using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    public GameObject platformPrefab;
    public GameObject collectiblePrefab;
    public float spawnRate = 1.5f;
    public float collectibleChance = 0.25f;
    public float platformLifetime = 10f;
    public float timer = 0f;
    
    public float spawnX = 20f;
    public float minY = -2f;
    public float maxY = 2f;
    
    public float initialSpeed = 5f;
    private float currentSpeed;
    public float maxSpeed = 50f;
    public float speedIncrement = .25f;
    private int platformsGenerated = 0;
    
    public float minPlatformWidth = 3f;
    public float maxPlatformWidth = 5f;
    
    void Start()
    {
        currentSpeed = initialSpeed;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer > spawnRate)
        {
            GeneratePlatform();
            timer = 0;
        }
    }
    
    void GeneratePlatform()
    {
        float spawnY = Random.Range(minY, maxY);
        Vector3 spawnPosition = new Vector3(spawnX, spawnY, 0);
        GameObject newPlatform = Instantiate(platformPrefab, spawnPosition, Quaternion.identity);
        newPlatform.GetComponent<ScrollingObject>().SetSpeed(currentSpeed);
        
        float platformWidth = Random.Range(minPlatformWidth, maxPlatformWidth);
        newPlatform.transform.localScale = new Vector3(platformWidth, newPlatform.transform.localScale.y, newPlatform.transform.localScale.z);

        platformsGenerated++;

        if (platformsGenerated % 2 == 0 && currentSpeed < maxSpeed)
        {
            currentSpeed += speedIncrement;
            currentSpeed = Mathf.Min(currentSpeed, maxSpeed);
        }

        if (Random.value < collectibleChance)
        {
            Vector3 collectiblePosition = spawnPosition + new Vector3(0, 2.25f, 0);
            Instantiate(collectiblePrefab, collectiblePosition, Quaternion.identity, newPlatform.transform);
        }
        
        Destroy(newPlatform, platformLifetime);
    }
}