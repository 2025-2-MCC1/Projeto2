using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public void Retry()
    {
        // Recarrega a cena principal do jogo
        SceneManager.LoadScene("Cars3");
    }

    public void MainMenu()
    {
        // Volta para o menu principal
        SceneManager.LoadScene("menu");
    }
}
