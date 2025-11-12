using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class menucontroler : MonoBehaviour
{
    [Header("Referências Visuais")]
    public VideoPlayer videoPlayer;
    public GameObject rawImage;
    private Animator animatorRawImage;

    [Header("Painéis")]
    public GameObject menuopcoes;   // Painel principal (Start, Options, Quit)
    public GameObject options;      // Painel de opções (volume, voltar)
    public GameObject titulo;
    public GameObject video;

    [Header("Sons")]
    public AudioSource audioSource;
    public AudioClip somParafusadeira;
    public AudioSource musicaFundo;
    public AudioSource somMotorSource;
    public AudioClip somMotor;

    private bool jaIniciou = false;

    void Start()
    {
        rawImage.SetActive(false);
        animatorRawImage = rawImage.GetComponent<Animator>();

        // Início: mostra apenas o título
        menuopcoes.SetActive(false);
        options.SetActive(false);
        titulo.SetActive(true);
        video.SetActive(false);

        if (musicaFundo != null)
            musicaFundo.Stop();
    }

    void Update()
    {
        if (!jaIniciou && Input.anyKeyDown)
        {
            jaIniciou = true;
            rawImage.SetActive(true);
            animatorRawImage.SetTrigger("FadeIn");
            videoPlayer.Play();

            // Mostra menu principal
            menuopcoes.SetActive(true);
            titulo.SetActive(false);
            video.SetActive(true);

            if (musicaFundo != null)
                musicaFundo.Play();

            if (somMotorSource != null && somMotor != null)
                somMotorSource.PlayOneShot(somMotor);
        }
    }

    public void StartGame()
    {
        if (audioSource && somParafusadeira)
            audioSource.PlayOneShot(somParafusadeira);

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

    // 🔹 Ao clicar em "Options" (botão do menu principal)
    public void AbrirOpcoes()
    {
        if (audioSource && somParafusadeira)
            audioSource.PlayOneShot(somParafusadeira);

        // Esconde o menu principal e o vídeo
        menuopcoes.SetActive(false);
        video.SetActive(false);

        // Mostra o painel de opções
        options.SetActive(true);
    }

    // 🔹 Ao clicar em "Voltar" (botão dentro do painel Options)
    public void VoltarMenuPrincipal()
    {
        if (audioSource && somParafusadeira)
            audioSource.PlayOneShot(somParafusadeira);

        // Esconde o painel de opções
        options.SetActive(false);

        // Mostra novamente o menu principal e o vídeo
        menuopcoes.SetActive(true);
        video.SetActive(true);
    }
}
