using UnityEngine;
using UnityEngine.SceneManagement; // Necessário para trocar de cena

public class PlayerCollision : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Pausa o jogo (opcional)
            Time.timeScale = 1f; // garante que o tempo volte ao normal antes de mudar de cena
            SceneManager.LoadScene("GameOver"); // Nome da cena que você quer carregar
        }
    }
}
