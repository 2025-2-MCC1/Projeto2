using UnityEngine;

public class PistaInfinita : MonoBehaviour
{
    public float speed = 5f;
    public float comprimentoPista = 50f;

    void Update()
    {
        // Move para trás (igual ao seu código da grama)
        transform.Translate(Vector3.forward * -speed * Time.deltaTime);

        // Verifica se a pista já passou completamente
        if (transform.position.z <= -comprimentoPista)
        {
            // Reposiciona a pista
            ReposicionarPista();
        }
    }

    void ReposicionarPista()
    {
        // Move a pista de volta para a posição inicial
        Vector3 novaPosicao = transform.position;
        novaPosicao.z += comprimentoPista * 2f;
        transform.position = novaPosicao;
    }
}