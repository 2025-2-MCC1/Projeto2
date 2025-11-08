using UnityEngine;

public class CarForward : MonoBehaviour
{
    [Header("Movimento Frontal")]
    public float baseSpeed = 10f;       // Velocidade mínima (nunca para)
    public float maxSpeed = 25f;        // Velocidade máxima
    public float acceleration = 5f;     // Aceleração (↑)
    public float brakingForce = 10f;    // Força do freio (↓)

    private float currentSpeed;

    void Start()
    {
        currentSpeed = baseSpeed;
    }

    void Update()
    {
        // Movimento constante pra frente
        transform.Translate(Vector3.forward * currentSpeed * Time.deltaTime);

        // --- Aceleração e frenagem ---
        if (Input.GetKey(KeyCode.UpArrow))
        {
            // Acelera até o limite máximo
            currentSpeed += acceleration * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            // Diminui a velocidade mais rapidamente
            currentSpeed -= brakingForce * Time.deltaTime;
        }
        else
        {
            // Retorna suavemente à velocidade base
            if (currentSpeed > baseSpeed)
                currentSpeed -= acceleration * Time.deltaTime * 0.5f;
            else if (currentSpeed < baseSpeed)
                currentSpeed += acceleration * Time.deltaTime * 0.5f;
        }

        // Garante que o carro nunca pare nem ultrapasse o máximo
        currentSpeed = Mathf.Clamp(currentSpeed, baseSpeed, maxSpeed);
    }

    // ✅ Método público para o GameManager acessar a velocidade atual
    public float GetCurrentSpeed()
    {
        return currentSpeed;
    }
}
