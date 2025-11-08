using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Game Over")]
    public bool isGameOver = false;
    public string gameOverSceneName = "GameOver";

    [Header("Combustível")]
    public float maxFuel = 100f;
    public float currentFuel = 100f;
    public float baseFuelDecrease = 3f;     // consumo normal por segundo
    public float accelFuelExtra = 5f;       // consumo extra ao acelerar
    public float speedFuelMultiplier = 0.05f; // consumo proporcional à velocidade
    public Slider fuelSlider;

    [Header("Gasolina Coletada")]
    public int gasolinasColetadas = 0;
    public TextMeshProUGUI gasolinaText;

    [Header("Referência ao carro")]
    public CarForward carForward; // arrasta o carro aqui no Inspector

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

        // Consumo base
        float fuelConsumption = baseFuelDecrease * Time.deltaTime;

        // Consumo proporcional à velocidade do carro
        if (carForward != null)
        {
            fuelConsumption += carForward.GetCurrentSpeed() * speedFuelMultiplier * Time.deltaTime;
        }

        // Consumo extra ao acelerar
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            fuelConsumption += accelFuelExtra * Time.deltaTime;
        }

        // Diminui o combustível
        currentFuel -= fuelConsumption;
        currentFuel = Mathf.Clamp(currentFuel, 0f, maxFuel);

        // Atualiza UI
        if (fuelSlider != null)
            fuelSlider.value = currentFuel / maxFuel;

        // Game Over se acabar
        if (currentFuel <= 0f && !isGameOver)
            GameOver();
    }

    public void GameOver()
    {
        isGameOver = true;
        Time.timeScale = 1f;
        SceneManager.LoadScene(gameOverSceneName);
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

    // 👇 Quando pegar um galão
    public void AddFuel(float amount)
    {
        if (isGameOver) return;

        currentFuel += amount;
        currentFuel = Mathf.Clamp(currentFuel, 0f, maxFuel);

        AddGasolineCollect();
    }

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
