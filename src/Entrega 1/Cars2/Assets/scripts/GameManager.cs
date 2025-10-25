

using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject gameOverPanel;
    public bool isGameOver = false; //  Adicione esta linha

    private void Awake()
    {
        Instance = this;
    }

    public void GameOver()
    {
        isGameOver = true; //  Define que o jogo acabou
        Time.timeScale = 0f;
        gameOverPanel.SetActive(true);
    }

    public void Retry()
    {
        isGameOver = false; //  Reseta quando reinicia
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void MainMenu()
    {
        isGameOver = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
