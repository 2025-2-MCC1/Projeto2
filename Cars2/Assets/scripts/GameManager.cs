using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // ← necessário se você quiser exibir o combustível na UI

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Game Over")]
    public GameObject gameOverPanel;
    public bool isGameOver = false;

    [Header("Combustível")]
    public float maxFuel = 100f;          // combustível máximo
    public float currentFuel = 100f;      // combustível atual
    public float fuelDecreaseRate = 5f;   // consumo por segundo
    public Slider fuelSlider;             // opcional (arraste o Slider da UI)

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (isGameOver) return;

        // Consome combustível gradualmente
        currentFuel -= fuelDecreaseRate * Time.deltaTime;
        currentFuel = Mathf.Clamp(currentFuel, 0f, maxFuel);

        // Atualiza a UI (se tiver Slider)
        if (fuelSlider != null)
        {
            fuelSlider.value = currentFuel / maxFuel;
        }

        // Se o combustível acabar → Game Over
        if (currentFuel <= 0f)
        {
            GameOver();
        }
    }

    public void GameOver()
    {
        isGameOver = true;
        Time.timeScale = 0f;
        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);
    }

    public void Retry()
    {
        isGameOver = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void MainMenu()
    {
        isGameOver = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    // 🔥 Método chamado quando o jogador pega um galão de gasolina
    public void AddFuel(float amount)
    {
        if (isGameOver) return;
        currentFuel += amount;
        currentFuel = Mathf.Clamp(currentFuel, 0f, maxFuel);
    }
}

