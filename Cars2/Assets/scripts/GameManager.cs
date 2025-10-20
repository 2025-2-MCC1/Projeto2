using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro; // ADICIONADO para compatibilidade com TextMeshPro

public class GameManager : MonoBehaviour
{
    private float score;
    public bool isGameOver = false;

    // ----- REFER�NCIAS DA UI (ARRASTE NO INSPECTOR) -----
    public TextMeshProUGUI scoreText; // ALTERADO para aceitar TextMeshPro
    public GameObject gameOverPanel;

    // Refer�ncia ao script do jogador para desativ�-lo no fim do jogo.
    public MonoBehaviour playerControllerScript;

    void Start()
    {
        // Garante que o jogo est� rodando e o painel de game over est� escondido
        Time.timeScale = 1;
        gameOverPanel.SetActive(false);
        isGameOver = false;
        score = 0f;
    }

    void Update()
    {
        // Se o jogo n�o acabou, aumenta a pontua��o com base no tempo
        if (!isGameOver)
        {
            score += Time.deltaTime * 10;
            scoreText.text = "PONTOS: " + Mathf.FloorToInt(score).ToString();
        }
    }

    // Fun��o chamada quando o jogador colide com um obst�culo
    public void GameOver()
    {
        if (isGameOver) return; // Evita que a fun��o seja chamada v�rias vezes

        isGameOver = true;
        gameOverPanel.SetActive(true); // Mostra a tela de "Fim de Jogo"

        // Desativa o controle do jogador para que ele n�o possa mais se mover
        if (playerControllerScript != null)
        {
            playerControllerScript.enabled = false;
        }
    }

    // Fun��o para ser usada pelo bot�o de "Jogar Novamente"
    public void RestartGame()
    {
        // Recarrega a cena atual, reiniciando o jogo
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

