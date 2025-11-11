using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.Collections;

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
    public TextMeshProUGUI gasolinaWhiteText;
    public TextMeshProUGUI gasolinaRedText;

    [Header("Referência ao carro")]
    public CarForward carForward;

    private bool isUsingNitro = false;

    [Header("Contagem de Largada")]
    public TextMeshProUGUI whiteText;
    public TextMeshProUGUI redText;
    public Image leftFlag;
    public Image rightFlag;

    [Header("Textos do 'RACE!'")]
    public TextMeshProUGUI whiteRaceText;
    public TextMeshProUGUI redRaceText;

    [Header("Som da Corneta")]
    public AudioSource cornetaSom;

    private bool gameStarted = false;

    // =====================================================
    // ====================   CRONÔMETRO   =================
    // =====================================================

    [Header("Cronômetro")]
    public TextMeshProUGUI cronometroWhiteText;
    public TextMeshProUGUI cronometroRedText;
    private float tempoDeCorrida = 0f;
    private bool contandoTempo = false;

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

        // 🚦 Inicia a contagem regressiva
        StartCoroutine(StartCountdown());
    }

    private void Update()
    {
        if (!gameStarted || isGameOver) return;

        AtualizarCombustivel();
        AtualizarNitro();

        // 🕒 Atualiza o cronômetro
        if (contandoTempo)
        {
            tempoDeCorrida += Time.deltaTime;
            AtualizarCronometroUI();
        }
    }

    // =====================================================
    // ================   CONTAGEM DE LARGADA   ============
    // =====================================================

    IEnumerator StartCountdown()
    {
        if (carForward != null)
            carForward.canControl = false;

        whiteText.gameObject.SetActive(true);
        redText.gameObject.SetActive(true);

        yield return StartCoroutine(ShowCountdownNumber("3"));
        yield return StartCoroutine(ShowCountdownNumber("2"));
        yield return StartCoroutine(ShowCountdownNumber("1"));

        yield return StartCoroutine(ShowRaceMessage());

        if (carForward != null)
            carForward.canControl = true;

        gameStarted = true;

        // 🕒 Começa o cronômetro
        contandoTempo = true;
        tempoDeCorrida = 0f;
        AtualizarCronometroUI();
    }

    IEnumerator ShowCountdownNumber(string text, float duration = 1f)
    {
        whiteText.text = text;
        redText.text = text;
        yield return new WaitForSeconds(duration);
    }

    IEnumerator ShowRaceMessage()
    {
        whiteText.gameObject.SetActive(false);
        redText.gameObject.SetActive(false);

        whiteRaceText.gameObject.SetActive(true);
        redRaceText.gameObject.SetActive(true);
        whiteRaceText.text = "RACE!";
        redRaceText.text = "RACE!";

        if (cornetaSom != null && !cornetaSom.isPlaying)
            cornetaSom.Play();

        leftFlag.gameObject.SetActive(true);
        rightFlag.gameObject.SetActive(true);

        yield return new WaitForSeconds(0.8f);

        whiteRaceText.gameObject.SetActive(false);
        redRaceText.gameObject.SetActive(false);
        leftFlag.gameObject.SetActive(false);
        rightFlag.gameObject.SetActive(false);
    }

    // =====================================================
    // ===================   COMBUSTÍVEL   =================
    // =====================================================

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

    // =====================================================
    // ======================   NITRO   ====================
    // =====================================================

    void AtualizarNitro()
    {
        if (carForward == null) return;

        bool isPressingNitro = Input.GetKey(KeyCode.Space);

        if (isPressingNitro && currentNitro > 0f)
        {
            if (!isUsingNitro)
            {
                isUsingNitro = true;
                carForward.SetSpeedMultiplier(boostedSpeedMultiplier);
            }

            currentNitro -= nitroConsumptionRate * Time.deltaTime;
            currentNitro = Mathf.Clamp(currentNitro, 0f, maxNitro);

            if (currentNitro <= 0f)
                StopNitro();
        }
        else
        {
            if (isUsingNitro)
                StopNitro();
        }

        if (nitroSlider != null)
            nitroSlider.value = currentNitro / maxNitro;
    }

    private void StopNitro()
    {
        if (!isUsingNitro) return;

        isUsingNitro = false;

        if (carForward != null)
            carForward.SetSpeedMultiplier(1f);
    }

    // =====================================================
    // ===================   GASOLINA UI   =================
    // =====================================================

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
        string texto = "Gasolina: " + gasolinasColetadas;

        if (gasolinaWhiteText != null)
            gasolinaWhiteText.text = texto;

        if (gasolinaRedText != null)
            gasolinaRedText.text = texto;
    }

    // =====================================================
    // ===================   CRONÔMETRO UI   ===============
    // =====================================================

    void AtualizarCronometroUI()
    {
        int minutos = Mathf.FloorToInt(tempoDeCorrida / 60f);
        int segundos = Mathf.FloorToInt(tempoDeCorrida % 60f);
        int centesimos = Mathf.FloorToInt((tempoDeCorrida * 100f) % 100f);
        string texto = $"{minutos:00}:{segundos:00}.{centesimos:00}";

        if (cronometroWhiteText != null)
            cronometroWhiteText.text = texto;

        if (cronometroRedText != null)
            cronometroRedText.text = texto;
    }

    // =====================================================
    // ======================   GAME   =====================
    // =====================================================

    public void GameOver()
    {
        isGameOver = true;
        contandoTempo = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene(gameOverSceneName);
    }

    public void Retry()
    {
        isGameOver = false;
        contandoTempo = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void MainMenu()
    {
        isGameOver = false;
        contandoTempo = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
