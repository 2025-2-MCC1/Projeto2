using UnityEngine;
using TMPro;

public class TimerScript : MonoBehaviour
{
    [Header("Tempo total contado (segundos)")]
    public float elapsedTime = 0f;

    [Header("Referência ao texto do TMP")]
    public TextMeshProUGUI timerText;

    void Update()
    {
        // Verifica se há um texto TMP antes de atualizar
        if (timerText == null)
        {
            Debug.LogWarning("TimerScript: Nenhum TextMeshProUGUI atribuído!");
            return;
        }

        // Incrementa o tempo conforme o deltaTime
        elapsedTime += Time.deltaTime;

        // Calcula minutos e segundos
        int minutes = Mathf.FloorToInt(elapsedTime / 60f);
        int seconds = Mathf.FloorToInt(elapsedTime % 60f);

        // Atualiza o texto formatado (mm:ss)
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
