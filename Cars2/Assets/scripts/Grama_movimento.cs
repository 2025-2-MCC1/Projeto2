using UnityEngine;

public class Grama_movimento : MonoBehaviour
{
    public float speed = 5f; // Use valor positivo aqui
    public float comprimentoGrama = 10f; // Comprimento do objeto de grama em unidades

    void Update()
    {
        // Move para frente usando velocidade negativa para n�o ocorrer bugs
        transform.Translate(Vector3.forward * -speed * Time.deltaTime);

        // Verifica se a grama j� passou completamente
        if (transform.position.z <= -comprimentoGrama)
        {
            // Reposiciona a grama no in�cio
            ReposicionarGrama();
        }
    }

    void ReposicionarGrama()
    {
        // Move a grama de volta para a posi��o inicial.
        Vector3 novaPosicao = transform.position;
        novaPosicao.z += comprimentoGrama * 2f; // Reposiciona para frente (dobro do comprimento)
        transform.position = novaPosicao;
    }
}