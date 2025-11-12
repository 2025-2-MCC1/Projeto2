using UnityEngine;

public class ArvoresSpawn : MonoBehaviour
{
    public GameObject pista;
    public GameObject spaw;
    public GameObject Arvores;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Player entrou");
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entrou");
            Instantiate(pista, spaw.transform.position, Quaternion.identity);
            Instantiate(pista, spaw.transform.position, Quaternion.identity);

        }
    }
}
