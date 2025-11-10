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
    public float baseFuelDecrease = 3f;
    public float accelFuelExtra = 5f;
    public float speedFuelMultiplier = 0.05f;
    public Slider fuelSlider;

    [Header("Nitro")]
    public float maxNitro = 5f;
    public float currentNitro = 0f;
    public float nitroConsumptionRate = 1f;
    public float boostedSpeedMultiplier = 1.8f;
    public Slider nitroSlider;

    [Header("Gasolina Coletada")]
    public int gasolinasColetadas = 0;
    public TextMeshProUGUI gasolinaText;

    [Header("Referência ao carro")]
    public CarForward carForward;

    private bool isUsingNitro = false;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        AtualizarGasolinaUI();

        if (fuelSlider != null)
            fuelSlider.value = currentFuel / maxFuel;

        if (nitroSlider != null)
            nitroSlider.value = currentNitro / maxNitro;
    }

    private void Update()
    {
        if (isGameOver) return;

        AtualizarCombustivel();
        AtualizarNitro();
    }

    void AtualizarCombustivel()
    {
        float fuelConsumption = baseFuelDecrease * Time.deltaTime;

        if (carForward != null)
            fuelConsumption += carForward.GetCurrentSpeed() * speedFuelMultiplier * Time.deltaTime;

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            fuelConsumption += accelFuelExtra * Time.deltaTime;

        currentFuel -= fuelConsumption;
        currentFuel = Mathf.Clamp(currentFuel, 0f, maxFuel);

        if (fuelSlider != null)
            fuelSlider.value = currentFuel / maxFuel;

        if (currentFuel <= 0f && !isGameOver)
            GameOver();
    }

    void AtualizarNitro()
    {
        if (carForward == null) return;

        bool isPressingNitro = Input.GetKey(KeyCode.Space);

        // 🚀 Nitro ativo apenas enquanto pressiona e há carga
        if (isPressingNitro && currentNitro > 0f)
        {
            if (!isUsingNitro)
            {
                isUsingNitro = true;
                carForward.SetSpeedMultiplier(boostedSpeedMultiplier);
            }

            currentNitro -= nitroConsumptionRate * Time.deltaTime;
            currentNitro = Mathf.Clamp(currentNitro, 0f, maxNitro);

            // se zerar, desativa imediatamente
            if (currentNitro <= 0f)
                StopNitro();
        }
        else
        {
            // 🧯 soltar espaço desativa instantaneamente
            if (isUsingNitro)
                StopNitro();
        }

        // atualiza UI sempre
        if (nitroSlider != null)
            nitroSlider.value = currentNitro / maxNitro;
    }

    private void StopNitro()
    {
        if (!isUsingNitro) return;

        isUsingNitro = false;

        // retorna velocidade normal
        if (carForward != null)
            carForward.SetSpeedMultiplier(1f);
    }

    public void AddFuel(float amount)
    {
        if (isGameOver) return;

        currentFuel += amount;
        currentFuel = Mathf.Clamp(currentFuel, 0f, maxFuel);
        AddGasolineCollect();
    }

    public void AddNitro(float amount)
    {
        if (isGameOver) return;

        currentNitro += amount;
        currentNitro = Mathf.Clamp(currentNitro, 0f, maxNitro);

        if (nitroSlider != null)
            nitroSlider.value = currentNitro / maxNitro;
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
}
