using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    public void Retry()
    {
        SceneManager.LoadScene("Cars3"); // Nome exato da cena do jogo
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("menu");
    }
}
