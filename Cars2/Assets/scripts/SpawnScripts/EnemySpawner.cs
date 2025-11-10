using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform[] spawnPoints; // 4 spawn points (arraste os SpawnPoint0..3)
    public GameObject[] enemyPrefabs; // CarroInimigo1 e CarroInimigo2
    public float initialDelay = 1f;
    public float spawnInterval = 1.2f; // tempo entre spawns (ajuste)
    public float randomIntervalVariance = 0.6f; // variação aleatória

    private void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        yield return new WaitForSeconds(initialDelay);

        while (!GameManager.Instance || !GameManager.Instance.isGameOver)
        {
            if (GameManager.Instance != null && GameManager.Instance.isGameOver)
                break;

            SpawnOne();
            float wait = spawnInterval + Random.Range(-randomIntervalVariance, randomIntervalVariance);
            wait = Mathf.Max(0.2f, wait);
            yield return new WaitForSeconds(wait);
            
        }
    }

    void SpawnOne()
    {
        if (spawnPoints == null || spawnPoints.Length == 0) return;
        if (enemyPrefabs == null || enemyPrefabs.Length == 0) return;

        // escolhe aleatoriamente spawnPoint e prefab
        Transform sp = spawnPoints[Random.Range(0, spawnPoints.Length)];
        GameObject prefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];

        Instantiate(prefab, sp.position, sp.rotation);
        
    }
}
