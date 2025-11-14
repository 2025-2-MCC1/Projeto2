using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;  // Necessário para fechar o Editor
#endif

public class NewMonoBehaviourScript : MonoBehaviour
{
    // Método para chamar no botão "Quit"
    public void QuitGame()
    {
        // Mostra mensagem no Console
        Debug.Log("Você saiu");

        // Fecha o jogo
#if UNITY_EDITOR
        Debug.Log("Fechando o jogo no Editor"); // Apenas para o Editor
        EditorApplication.isPlaying = false;  // Para o modo Play no Editor
#else
        Debug.Log("Fechando o jogo no EXE"); // Apenas para o build
        Application.Quit();  // Fecha o EXE buildado
#endif
    }
}
