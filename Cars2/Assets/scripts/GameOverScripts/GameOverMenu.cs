using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class GameOverMenu : MonoBehaviour
{
    [Header("Som da Parafusadeira")]
    public AudioClip somParafusadeira;
    private AudioSource audioSource;

    [Header("Textos do Game Over")]
    public TextMeshProUGUI tempoTexto;
    public TextMeshProUGUI gasolinaTexto;

    private void Start()
    {
        // 🔊 Cria automaticamente um AudioSource (não precisa adicionar manualmente)
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;

        // 🟢 Busca os dados salvos do GameManager
        GameOverData data = GameObject.Find("Data").GetComponent<GameOverData>();

        if (data != null)
        {
            tempoTexto.text = "Tempo: " + FormatTempo(data.tempoDeCorrida);
            gasolinaTexto.text = "Gasolinas: " + data.gasolinasColetadas;

            // 🔥 Destroi o objeto depois de usar (não precisa mais)
            //Destroy(data.gameObject);
        }
        else
        {
            tempoTexto.text = "Tempo: 00:00.00";
            gasolinaTexto.text = "Gasolinas: 0";
        }
    }

    private string FormatTempo(float t)
    {
        int minutos = Mathf.FloorToInt(t / 60f);
        int segundos = Mathf.FloorToInt(t % 60f);
        int centesimos = Mathf.FloorToInt((t * 100f) % 100f);
        return $"{minutos:00}:{segundos:00}.{centesimos:00}";
    }

    public void Retry()
    {
        if (somParafusadeira != null)
            audioSource.PlayOneShot(somParafusadeira);

        StartCoroutine(EsperarSomECarregarCena("Cars3"));
    }

    public void MainMenu()
    {
        if (somParafusadeira != null)
            audioSource.PlayOneShot(somParafusadeira);

        StartCoroutine(EsperarSomECarregarCena("menu"));
    }

    private IEnumerator EsperarSomECarregarCena(string cena)
    {
        float tempo = somParafusadeira != null ? somParafusadeira.length : 0.5f;
        yield return new WaitForSeconds(tempo);
        SceneManager.LoadScene(cena);
    }
}
