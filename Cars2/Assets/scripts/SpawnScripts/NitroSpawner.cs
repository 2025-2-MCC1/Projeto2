using System.Collections;
using UnityEngine;

public class NitroSpawner : MonoBehaviour
{
    [Header("Spawn Configurações")]
    public Transform[] spawnPoints;             // pontos possíveis de spawn
    public GameObject[] nitroPrefabs;           // prefab(s) do cilindro de nitro

    [Header("Tempo de Spawn")]
    public float initialDelay = 5f;             // atraso inicial antes do primeiro spawn
    public float spawnInterval = 8f;            // tempo base entre spawns
    public float randomIntervalVariance = 2f;   // variação aleatória (+/-)

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
            wait = Mathf.Max(2f, wait);
            yield return new WaitForSeconds(wait);
        }
    }

    void SpawnOne()
    {
        if (spawnPoints == null || spawnPoints.Length == 0) return;
        if (nitroPrefabs == null || nitroPrefabs.Length == 0) return;

        Transform sp = spawnPoints[Random.Range(0, spawnPoints.Length)];
        GameObject prefab = nitroPrefabs[Random.Range(0, nitroPrefabs.Length)];

        Instantiate(prefab, sp.position, sp.rotation);
    }
}
