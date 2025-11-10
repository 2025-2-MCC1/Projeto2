using UnityEngine;

public class DestroyPista : MonoBehaviour
{
    public float destroyDelay = 5f; // tempo até destruir a pista antiga

    private bool destruindo = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !destruindo)
        {
            destruindo = true;

            // destrói o objeto principal da pista (raiz do prefab)
            Destroy(transform.root.gameObject, destroyDelay);

            Debug.Log("Pista antiga será destruída em " + destroyDelay + " segundos");
        }
    }
}
