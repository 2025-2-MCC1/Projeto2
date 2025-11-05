using System.Collections;
using UnityEngine;

public class GasSpawner : MonoBehaviour
{
    [Header("Spawn Configurações")]
    public Transform[] spawnPoints;           // pontos possíveis de spawn (ex: mesma faixa dos carros)
    public GameObject[] gasCanPrefabs;        // prefab(s) do galão de gasolina

    [Header("Tempo de Spawn")]
    public float initialDelay = 2f;           // atraso inicial antes do primeiro spawn
    public float spawnInterval = 4f;          // tempo base entre spawns
    public float randomIntervalVariance = 1.5f; // variação aleatória (+/-)

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
            wait = Mathf.Max(0.5f, wait);
            yield return new WaitForSeconds(wait);
        }
    }

    void SpawnOne()
    {
        if (spawnPoints == null || spawnPoints.Length == 0) return;
        if (gasCanPrefabs == null || gasCanPrefabs.Length == 0) return;

        // escolhe aleatoriamente ponto e prefab
        Transform sp = spawnPoints[Random.Range(0, spawnPoints.Length)];
        GameObject prefab = gasCanPrefabs[Random.Range(0, gasCanPrefabs.Length)];

        Instantiate(prefab, sp.position, sp.rotation);
    }
}
