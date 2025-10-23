using UnityEngine;
using System.Collections.Generic;

public class PistaInfinita : MonoBehaviour
{
    public Transform player;

    public GameObject Bloco1_2;
    public GameObject Bloco3_4;
    public GameObject Bloco5_6;
    public GameObject Bloco7_8;
    public GameObject Bloco9_10;
    public GameObject Bloco11_12;

    public float spawnDistance = 200f;
    public int initialBlocks = 6;    // coloque igual ao número de blocos da lista
    public float blockLength = 60f;  // <-- ajuste esse valor conforme o tamanho exato da sua estrada

    private float nextSpawnZ = 0f;
    private List<GameObject> trackPrefabs = new List<GameObject>();
    private List<GameObject> activeBlocks = new List<GameObject>();

    void Start()
    {
        trackPrefabs.Add(Bloco1_2);
        trackPrefabs.Add(Bloco3_4);
        trackPrefabs.Add(Bloco5_6);
        trackPrefabs.Add(Bloco7_8);
        trackPrefabs.Add(Bloco9_10);
        trackPrefabs.Add(Bloco11_12);

        // Instancia blocos iniciais na ordem
        for (int i = 0; i < initialBlocks; i++)
        {
            GameObject block = Instantiate(trackPrefabs[i], new Vector3(0, 0, nextSpawnZ), Quaternion.identity);
            activeBlocks.Add(block);
            nextSpawnZ += blockLength;
        }
    }

    void Update()
    {
        // Quando o jogador estiver perto do fim, move o primeiro bloco pro final
        if (player.position.z + spawnDistance > nextSpawnZ)
        {
            GameObject oldestBlock = activeBlocks[0];
            activeBlocks.RemoveAt(0);

            oldestBlock.transform.position = new Vector3(0, 0, nextSpawnZ);
            activeBlocks.Add(oldestBlock);

            nextSpawnZ += blockLength;
        }
    }
}


