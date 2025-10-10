using UnityEngine;

public class PistaInfinita : MonoBehaviour
{
    public Transform[] segmentosPista; // Segmentos da pista (m�nimo 2)
    public float velocidade = 5f;
    public float comprimentoSegmento = 50f;

    void Update()
    {
        for (int i = 0; i < segmentosPista.Length; i++)
        {
            Transform segmento = segmentosPista[i];

            // Move o segmento para tr�s
            segmento.Translate(Vector3.back * velocidade * Time.deltaTime);

            // Se o segmento saiu da tela (atr�s do jogador)
            if (segmento.position.z <= -comprimentoSegmento)
            {
                // Encontrar o segmento mais � frente (maior valor de Z)
                Transform segmentoMaisADireita = ObterSegmentoMaisAFrente();

                // Reposicionar o segmento atr�s do mais � frente
                float novaPosZ = segmentoMaisADireita.position.z + comprimentoSegmento;
                segmento.position = new Vector3(segmento.position.x, segmento.position.y, novaPosZ);
            }
        }
    }

    Transform ObterSegmentoMaisAFrente()
    {
        Transform maisAFrente = segmentosPista[0];

        foreach (Transform seg in segmentosPista)
        {
            if (seg.position.z > maisAFrente.position.z)
            {
                maisAFrente = seg;
            }
        }

        return maisAFrente;
    }
}
