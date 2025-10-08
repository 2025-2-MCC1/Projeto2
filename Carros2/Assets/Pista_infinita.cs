using UnityEngine;

public class PistaInfinita : MonoBehaviour
{
    public Transform[] segmentosPista; // Referência aos segmentos (2 ou mais)
    public float velocidade = 5f;
    public float comprimentoSegmento = 50f;

    void Update()
    {
        foreach (Transform segmento in segmentosPista)
        {
            // Move o segmento para trás
            segmento.Translate(Vector3.back * velocidade * Time.deltaTime);

            // Se o segmento saiu da visão, move ele para o final da fila
            if (segmento.position.z <= -comprimentoSegmento)
            {
                // Acha a maior posição Z atual entre os segmentos
                float maiorZ = ObterMaiorZ();
                segmento.position = new Vector3(segmento.position.x, segmento.position.y, maiorZ + comprimentoSegmento);
            }
        }
    }

    float ObterMaiorZ()
    {
        float maiorZ = float.MinValue;
        foreach (Transform segmento in segmentosPista)
        {
            if (segmento.position.z > maiorZ)
            {
                maiorZ = segmento.position.z;
            }
        }
        return maiorZ;
    }
}
