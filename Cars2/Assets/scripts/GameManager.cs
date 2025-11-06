using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro; // ← para usar texto na tela (TextMeshPro)

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Game Over")]
    public bool isGameOver = false;
    public string gameOverSceneName = "GameOver"; // nome da cena de Game Over

    [Header("Combustível")]
    public float maxFuel = 100f;
    public float currentFuel = 100f;
    public float fuelDecreaseRate = 5f;
    public Slider fuelSlider;

    [Header("Gasolina Coletada")]
    public int gasolinasColetadas = 0;          // contador de galões
    public TextMeshProUGUI gasolinaText;        // texto na tela (arrastar no Inspector)

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        AtualizarGasolinaUI();
    }

    private void Update()
    {
        if (isGameOver) return;

        // Consome combustível com o tempo
        currentFuel -= fuelDecreaseRate * Time.deltaTime;
        currentFuel = Mathf.Clamp(currentFuel, 0f, maxFuel);

        // Atualiza barra (se tiver)
        if (fuelSlider != null)
            fuelSlider.value = currentFuel / maxFuel;

        // Se o combustível acabar → Game Over
        if (currentFuel <= 0f && !isGameOver)
            GameOver();
    }

    public void GameOver()
    {
        isGameOver = true;
        Time.timeScale = 1f; // garante que o tempo não fique pausado
        SceneManager.LoadScene(gameOverSceneName); // vai pra cena de Game Over
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

    // 👇 Quando pegar um galão de gasolina
    public void AddFuel(float amount)
    {
        if (isGameOver) return;

        currentFuel += amount;
        currentFuel = Mathf.Clamp(currentFuel, 0f, maxFuel);

        // Contabiliza também a coleta
        AddGasolineCollect();
    }

    // ✅ Aumenta o contador de gasolina pega
    public void AddGasolineCollect()
    {
        gasolinasColetadas++;
        AtualizarGasolinaUI();
    }

    void AtualizarGasolinaUI()
    {
        if (gasolinaText != null)
            gasolinaText.text = "Gasolina: " + gasolinasColetadas;
    }
}
