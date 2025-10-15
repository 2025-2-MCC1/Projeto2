using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject obstaclePrefab;
    public float spawnRate = 2f;
    public float spawnXRange = 8f;
    private float nextSpawnTime;
    public GameManager gameManager;

    void Update()
    {
        if (gameManager.isGameOver) return;

        if (Time.time >= nextSpawnTime)
        {
            SpawnObstacle();
            nextSpawnTime = Time.time + 1f / spawnRate;
        }
    }

    void SpawnObstacle()
    {
        float randomX = Random.Range(-spawnXRange, spawnXRange);
        Vector3 spawnPosition = new Vector3(randomX, transform.position.y, transform.position.z);
        Instantiate(obstaclePrefab, spawnPosition, Quaternion.identity);
    }
}
