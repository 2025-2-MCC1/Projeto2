using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro; // ADICIONADO para compatibilidade com TextMeshPro

public class GameManager : MonoBehaviour
{
    private float score;
    public bool isGameOver = false;

    // ----- REFERÊNCIAS DA UI (ARRASTE NO INSPECTOR) -----
    public TextMeshProUGUI scoreText; // ALTERADO para aceitar TextMeshPro
    public GameObject gameOverPanel;

    // Referência ao script do jogador para desativá-lo no fim do jogo.
    public MonoBehaviour playerControllerScript;

    void Start()
    {
        // Garante que o jogo está rodando e o painel de game over está escondido
        Time.timeScale = 1;
        gameOverPanel.SetActive(false);
        isGameOver = false;
        score = 0f;
    }

    void Update()
    {
        // Se o jogo não acabou, aumenta a pontuação com base no tempo
        if (!isGameOver)
        {
            score += Time.deltaTime * 10;
            scoreText.text = "PONTOS: " + Mathf.FloorToInt(score).ToString();
        }
    }

    // Função chamada quando o jogador colide com um obstáculo
    public void GameOver()
    {
        if (isGameOver) return; // Evita que a função seja chamada várias vezes

        isGameOver = true;
        gameOverPanel.SetActive(true); // Mostra a tela de "Fim de Jogo"

        // Desativa o controle do jogador para que ele não possa mais se mover
        if (playerControllerScript != null)
        {
            playerControllerScript.enabled = false;
        }
    }

    // Função para ser usada pelo botão de "Jogar Novamente"
    public void RestartGame()
    {
        // Recarrega a cena atual, reiniciando o jogo
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

