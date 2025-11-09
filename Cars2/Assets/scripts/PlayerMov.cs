using UnityEngine;

public class CarForward : MonoBehaviour
{
    [Header("Movimento Frontal")]
    public float baseSpeed = 10f;       // Velocidade mínima (nunca para)
    public float maxSpeed = 25f;        // Velocidade máxima
    public float acceleration = 5f;     // Aceleração (↑)
    public float brakingForce = 10f;    // Força do freio (↓)

    private float currentSpeed;
    private bool slowingDown = false;   // Controle da desaceleração forçada

    void Start()
    {
        currentSpeed = baseSpeed;
    }

    void Update()
    {
        // Movimento constante pra frente
        transform.Translate(Vector3.forward * currentSpeed * Time.deltaTime);

        if (!slowingDown)
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                currentSpeed += acceleration * Time.deltaTime;
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                currentSpeed -= brakingForce * Time.deltaTime;
            }
            else
            {
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

        currentSpeed = Mathf.Clamp(currentSpeed, baseSpeed, maxSpeed);
    }

    // ✅ Método público para pegar a velocidade atual
    public float GetCurrentSpeed()
    {
        return currentSpeed;
    }

    // ✅ Reduz gradualmente a velocidade (usado pela barreira)
    public void ReduceSpeedOverTime(float rate)
    {
        currentSpeed = Mathf.MoveTowards(currentSpeed, baseSpeed, rate * Time.deltaTime);
    }

    // ✅ Chamada pela barreira para forçar desaceleração
    public void SlowDownToBaseSpeed()
    {
        slowingDown = true;
        Invoke(nameof(StopSlowing), 2f);
    }

    private void StopSlowing()
    {
        slowingDown = false;
    }
}
