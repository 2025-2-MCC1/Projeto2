using UnityEngine;

public class CarForward : MonoBehaviour
{
    [Header("Movimento Frontal")]
    public float baseSpeed = 10f;       // Velocidade mínima
    public float maxSpeed = 25f;        // Velocidade máxima
    public float acceleration = 5f;     // Aceleração
    public float brakingForce = 10f;    // Força do freio

    [Header("Controle do Jogador")]
    public bool canControl = true;      // 🚦 Se falso, bloqueia os inputs (mas mantém baseSpeed)

    private float currentSpeed;
    private bool slowingDown = false;

    // 🚀 Nitro controlado externamente (GameManager)
    private float speedMultiplier = 1f;

    void Start()
    {
        currentSpeed = baseSpeed;
    }

    void Update()
    {
        // Movimento frontal contínuo (o carro sempre anda pra frente)
        transform.Translate(Vector3.forward * currentSpeed * speedMultiplier * Time.deltaTime);

        // 🚫 Se o jogador ainda não pode controlar, mantém apenas a velocidade base
        if (!canControl)
        {
            // Garante que o carro fique exatamente na velocidade mínima
            currentSpeed = Mathf.MoveTowards(currentSpeed, baseSpeed, brakingForce * Time.deltaTime);
            return;
        }

        // 🎮 Movimento padrão (aceleração e desaceleração)
        if (!slowingDown)
        {
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                currentSpeed += acceleration * Time.deltaTime;
            }
            else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            {
                currentSpeed -= brakingForce * Time.deltaTime;
            }
            else
            {
                // Retorna gradualmente à velocidade base
                if (currentSpeed > baseSpeed)
                    currentSpeed -= acceleration * Time.deltaTime * 0.5f;
                else if (currentSpeed < baseSpeed)
                    currentSpeed += acceleration * Time.deltaTime * 0.5f;
            }
        }
        else
        {
            currentSpeed = Mathf.MoveTowards(currentSpeed, baseSpeed, brakingForce * Time.deltaTime);
        }

        // Limita a velocidade máxima conforme o multiplicador
        currentSpeed = Mathf.Clamp(currentSpeed, baseSpeed, maxSpeed * speedMultiplier);
    }

    public float GetCurrentSpeed()
    {
        return currentSpeed;
    }

    public void ReduceSpeedOverTime(float rate)
    {
        currentSpeed = Mathf.MoveTowards(currentSpeed, baseSpeed, rate * Time.deltaTime);
    }

    public void SlowDownToBaseSpeed()
    {
        slowingDown = true;
        Invoke(nameof(StopSlowing), 2f);
    }

    private void StopSlowing()
    {
        slowingDown = false;
    }

    public void SetSpeedMultiplier(float multiplier)
    {
        speedMultiplier = Mathf.Clamp(multiplier, 1f, 2f); // máximo 2x
    }
}

