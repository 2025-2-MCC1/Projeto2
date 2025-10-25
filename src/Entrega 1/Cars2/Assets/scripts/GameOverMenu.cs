using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    public void Retry()
    {
        // Recarrega a cena atual
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GoToMainMenu()
    {
        // Carrega a cena do menu principal
        SceneManager.LoadScene("menu"); // ou "MainMenu", conforme o nome exato da cena
    }

    public void QuitGame()
    {
        // Fecha o jogo (funciona só em build)
        Application.Quit();
    }
}
