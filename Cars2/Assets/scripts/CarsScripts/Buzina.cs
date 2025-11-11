using UnityEngine;
using UnityEngine.UI;

public class Buzina : MonoBehaviour
{
    [Header("Som da Buzina")]
    public AudioSource buzinaSom;

    [Header("UI da Buzina")]
    public Image buzinaIcone;
    public Color corAtiva = Color.white;
    public Color corDesativada = new Color(1, 1, 1, 0.4f); // meio transparente

    [Header("Tecla de Buzina")]
    public KeyCode teclaBuzina = KeyCode.E; // tecla E

    private bool buzinando = false;

    void Update()
    {
        // Quando pressionar E
        if (Input.GetKeyDown(teclaBuzina))
        {
            if (!buzinaSom.isPlaying)
                buzinaSom.Play();

            buzinando = true;
            AtualizarUI(true);
        }

        // Quando soltar E
        if (Input.GetKeyUp(teclaBuzina))
        {
            buzinaSom.Stop();
            buzinando = false;
            AtualizarUI(false);
        }
    }

    void AtualizarUI(bool ativo)
    {
        if (buzinaIcone != null)
        {
            buzinaIcone.color = ativo ? corAtiva : corDesativada;
        }
    }
}
