using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject boxPrefab;

    private float spawnInterval = 2.0f;

    void Start()
    {
        InvokeRepeating("SpawnBox", 0f, spawnInterval);
    }

    void SpawnBox()
    {
        float randomX = Random.Range(-3f, 2f);
        Vector3 spawnPosition = new Vector3(randomX, 1, -16);
        Instantiate(boxPrefab, spawnPosition, Quaternion.identity);
        GameManager.Instance.AddBox();
    }
}
