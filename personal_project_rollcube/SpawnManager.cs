using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] objPrefabs;
    private int spawnRange = 10;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (objPrefabs.Length > 0)
            InvokeRepeating("CreateObject", 1f, 1f);
    }

    void  CreateObject()
    {
        int index = Random.Range(0, objPrefabs.Length);
        GameObject prefab = objPrefabs[index];
        Vector3 pos = new Vector3(Random.Range(0, spawnRange), 1, Random.Range(0, spawnRange));
        Instantiate(prefab, pos, prefab.transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
