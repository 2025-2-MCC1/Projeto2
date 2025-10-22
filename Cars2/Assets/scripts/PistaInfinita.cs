using UnityEngine;
using System.Collections.Generic;

public class PistaInfinita : MonoBehaviour
{
    public Transform player;               // Referência ao jogador

    // Referências públicas para seus prefabs (arraste no Inspector)
    public GameObject Bloco1_2;
    public GameObject Bloco3_4;
    public GameObject Bloco5_6;
    public GameObject Bloco7_8;
    public GameObject Bloco9_10;
    public GameObject Bloco11_12;

    public float spawnDistance = 50f;     // Distância para spawnar o próximo bloco
    public int initialBlocks = 3;         // Quantidade de blocos para começar
    public float blockLength = 30f;       // Tamanho do bloco (eixo Z)

    private float nextSpawnZ = 0f;         // Posição para spawnar o próximo bloco
    private List<GameObject> trackPrefabs = new List<GameObject>();
    private List<GameObject> activeBlocks = new List<GameObject>();

    void Start()
    {
        // Adiciona seus prefabs à lista para usar no spawn
        trackPrefabs.Add(Bloco1_2);
        trackPrefabs.Add(Bloco3_4);
        trackPrefabs.Add(Bloco5_6);
        trackPrefabs.Add(Bloco7_8);
        trackPrefabs.Add(Bloco9_10);
        trackPrefabs.Add(Bloco11_12);

        // Instancia os blocos iniciais da pista
        for (int i = 0; i < initialBlocks; i++)
        {
            SpawnTrackBlock();
        }
    }

    void Update()
    {
        if (player.position.z + spawnDistance > nextSpawnZ)
        {
            SpawnTrackBlock();
            RemoveOldestBlock();
        }
    }

    void SpawnTrackBlock()
    {
        int prefabIndex = Random.Range(0, trackPrefabs.Count);
        GameObject block = Instantiate(trackPrefabs[prefabIndex], new Vector3(0, 0, nextSpawnZ), Quaternion.identity);
        activeBlocks.Add(block);
        nextSpawnZ += blockLength;
    }

    void RemoveOldestBlock()
    {
        if (activeBlocks.Count > initialBlocks)
        {
            Destroy(activeBlocks[0]);
            activeBlocks.RemoveAt(0);
        }
    }
}
