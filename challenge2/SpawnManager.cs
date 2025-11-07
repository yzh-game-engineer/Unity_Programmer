using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] animalPrefabs;
    private float spawnRangeX = 10.0f;
    private float spawnPosZ = 20.0f;
    private float startDelay = 2.0f;
    private float spawnInterval = 1.5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InvokeRepeating("SpawnRandomAnimal", startDelay, spawnInterval);

    }

    // Update is called once per frame
    void Update()
    {        
    }

    private void SpawnRandomAnimal()
    {
        int spawnIndex = Random.Range(0, animalPrefabs.Length);
        //Vector3 spawnPosition = new Vector3(Random.Range(-spawnRangeX, spawnRangeX), 0, spawnPosZ);
        //Instantiate(animalPrefabs[spawnIndex], spawnPosition, animalPrefabs[spawnIndex].transform.rotation);
        Instantiate(animalPrefabs[spawnIndex], transform.position + new Vector3(Random.Range(-spawnRangeX, spawnRangeX), 0, 0), transform.rotation);
    }
}
