using UnityEngine;
using TMPro;
using System.Collections;

public class TimerScript : MonoBehaviour
{
    [Header("UI")]
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI startCountdownText;

    [Header("Som da Corneta")]
    public AudioSource cornetaSom;

    private float elapsedTime = 0f;
    private bool jogoIniciado = false;

    void Start()
    {
        StartCoroutine(ContagemInicial());
    }

    IEnumerator ContagemInicial()
    {
        int contador = 3;
        while (contador > 0)
        {
            startCountdownText.text = contador.ToString();
            yield return new WaitForSeconds(1f);
            contador--;
        }

        // "RACE!" aparece e toca a corneta
        startCountdownText.text = "RACE!";
        if (cornetaSom != null)
            cornetaSom.Play();

        // Espera 1 segundo e começa o timer
        yield return new WaitForSeconds(1f);
        startCountdownText.gameObject.SetActive(false);
        jogoIniciado = true;
    }

    void Update()
    {
        if (!jogoIniciado)
            return;

        elapsedTime += Time.deltaTime;

        int minutes = Mathf.FloorToInt(elapsedTime / 60f);
        int seconds = Mathf.FloorToInt(elapsedTime % 60f);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
