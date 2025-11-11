using UnityEngine;
using UnityEngine.SceneManagement; // Necessário para trocar de cena

public class PlayerCollision : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        // Verifica se o objeto colidido tem a tag "Enemy"
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Pausa o jogo (opcional)
            Time.timeScale = 1f; // Garante que o tempo volte ao normal antes de mudar de cena

            // Chama o método GameOver no GameManager
            GameObject.Find("GameManager").GetComponent<GameManager>().GameOver();
        }
    }
}
