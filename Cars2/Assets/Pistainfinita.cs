using UnityEngine;

public class PistaInfinita : MonoBehaviour
{
    public Transform[] segmentosPista; // Segmentos da pista (mínimo 2)
    public float velocidade = 5f;
    public float comprimentoSegmento = 50f;

    void Update()
    {
        for (int i = 0; i < segmentosPista.Length; i++)
        {
            Transform segmento = segmentosPista[i];

            // Move o segmento para trás
            segmento.Translate(Vector3.back * velocidade * Time.deltaTime);

            // Se o segmento saiu da tela (atrás do jogador)
            if (segmento.position.z <= -comprimentoSegmento)
            {
                // Encontrar o segmento mais à frente (maior valor de Z)
                Transform segmentoMaisADireita = ObterSegmentoMaisAFrente();

                // Reposicionar o segmento atrás do mais à frente
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
