using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class menucontroler : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public GameObject menuopcoes, rawImage;
    private Animator animatorRawImage;
    public GameObject titulo;
    public GameObject video;

    [Header("Som de Parafusadeira")]
    public AudioSource audioSource; // ← som dos botões
    public AudioClip somParafusadeira;

    [Header("Música de Fundo")]
    public AudioSource musicaFundo; // ← música ambiente do menu

    [Header("Som de Aceleração (primeiro clique)")]
    public AudioSource somMotorSource; // ← outro AudioSource
    public AudioClip somMotor; // ← som de acelerar carro

    private bool jaIniciou = false; // garante que só acontece uma vez

    void Start()
    {
        rawImage.SetActive(false);
        animatorRawImage = rawImage.GetComponent<Animator>();

        // Garante que nada toque antes da hora
        if (musicaFundo != null)
            musicaFundo.Stop();

        if (somMotorSource != null)
            somMotorSource.Stop();
    }

    void Update()
    {
        // Só reage ao primeiro clique/tecla
        if (!jaIniciou && Input.anyKeyDown)
        {
            jaIniciou = true;

            rawImage.SetActive(true);
            animatorRawImage.SetTrigger("FadeIn");
            videoPlayer.Play();
            menuopcoes.SetActive(true);
            titulo.SetActive(false);
            video.SetActive(true);

            // 🎵 Toca a música de fundo
            if (musicaFundo != null)
                musicaFundo.Play();

            // 🚗 Toca o som de aceleração
            if (somMotorSource != null && somMotor != null)
                somMotorSource.PlayOneShot(somMotor);
        }
    }

    // 🟢 Chamado pelo botão "Start Game"
    public void StartGame()
    {
        // 🔧 Toca o som da parafusadeira
        if (audioSource && somParafusadeira)
            audioSource.PlayOneShot(somParafusadeira);

        // Carrega a cena com atraso
        Invoke(nameof(CarregarCena), 0.5f);
    }

    void CarregarCena()
    {
        SceneManager.LoadScene("Cars3");
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("O jogo foi encerrado!");
    }
}
