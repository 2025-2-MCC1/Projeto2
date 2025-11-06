using UnityEngine;

public class WheelController : MonoBehaviour
{
    [Header("Rodas")]
    public Transform[] wheelsToSpin;      // Todas as rodas que giram (ex: as 4)
    public Transform[] frontWheelsToTurn; // Rodas da frente que viram

    [Header("Configurações")]
    public float spinSpeed = 1000f;      // Velocidade de giro (rotação no eixo X)
    public float maxTurnAngle = 30f;     // Ângulo máximo para virar as rodas
    public float input;                  // Input horizontal (-1 a 1)

    void Update()
    {
        // Simule ou pegue o input real
        input = Input.GetAxis("Horizontal");

        // Girar as rodas (giro ao redor do eixo X)
        foreach (Transform wheel in wheelsToSpin)
        {
            wheel.Rotate(spinSpeed * Time.deltaTime, 0f, 0f);
        }

        // Virar as rodas da frente no eixo Y
        float steerAngle = maxTurnAngle * input;
        foreach (Transform frontWheel in frontWheelsToTurn)
        {
            // Apenas roda em torno do eixo Y local (gira para esquerda/direita)
            frontWheel.localRotation = Quaternion.Euler(0f, steerAngle, 0f);
        }
    }
}

