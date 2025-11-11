using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameOverMenu : MonoBehaviour
{
    [Header("Som da Parafusadeira")]
    public AudioClip somParafusadeira; // ← este é o campo que vai aparecer no Inspector
    private AudioSource audioSource;

    private void Start()
    {
        // Cria automaticamente um AudioSource (não precisa adicionar manualmente)
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
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
