using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            // opcional: tocar som, animação
            GameManager.Instance.GameOver();
        }
    }

    // se você usa OnCollision:
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            GameManager.Instance.GameOver();
        }
    }
}
