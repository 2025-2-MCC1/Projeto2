using UnityEngine;

public class carrosvindo : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float intervalo = 1.5f;
    public float limiteX = 7f;
    public float posicaoZ = 30f;

    void Start()
    {
        InvokeRepeating("SpawnEnemy", 1f, intervalo);
    }

    void SpawnEnemy()
    {
        float posX = Random.Range(-limiteX, limiteX);
        Vector3 spawnPosition = new Vector3(posX, 0f, posicaoZ);

        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }
}
