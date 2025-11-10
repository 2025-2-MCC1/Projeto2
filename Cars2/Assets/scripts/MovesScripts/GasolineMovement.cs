using UnityEngine;

public class GasolineMovement3D : MonoBehaviour
{
    public float speed = 10f;       // velocidade da gasolina
    public float destroyZ = -10f;   // quando sair da tela, destrói

    void Update()
    {
        // Move o galão para trás no eixo Z
        transform.Translate(Vector3.right * speed * Time.deltaTime);

        // Destroi o galão se sair da pista
        if (transform.position.z < destroyZ)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Aumenta combustível
            GameManager.Instance.AddFuel(40f);
            Destroy(gameObject);
        }
    }
}
