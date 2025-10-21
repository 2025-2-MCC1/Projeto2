

using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("UI")]
    public GameObject gameOverPanel; // arraste o painel de Game Over aqui

    [HideInInspector]
    public bool isGameOver = false;

    private void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
        //DontDestroyOnLoad(gameObject); // opcional se quiser manter entre cenas
    }

    private void Start()
    {
        if (gameOverPanel != null) gameOverPanel.SetActive(false);
        Time.timeScale = 1f;
        isGameOver = false;
    }

    public void GameOver()
    {
        if (isGameOver) return;
        isGameOver = true;
        Time.timeScale = 0f; // pausa o jogo
        if (gameOverPanel != null) gameOverPanel.SetActive(true);
    }

    // chamadas por botões UI:
    public void Retry()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu(string menuSceneName) // ou int buildIndex
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(menuSceneName);
    }
}
