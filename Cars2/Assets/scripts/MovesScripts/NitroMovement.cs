using UnityEngine;

public class NitroMovement3D : MonoBehaviour
{
    public float speed = 10f;       // velocidade do cilindro
    public float destroyZ = -10f;   // destrói quando sai da tela

    void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);

        if (transform.position.z < destroyZ)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Recarrega o nitro no GameManager
            GameManager.Instance.AddNitro(2.5f); // ajusta o valor conforme quiser
            Destroy(gameObject);
        }
    }
}
