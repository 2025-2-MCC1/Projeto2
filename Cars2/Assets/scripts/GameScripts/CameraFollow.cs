using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;     // O carro
    public Vector3 offset;       // Distância da câmera em relação ao carro
    public float smoothSpeed = 5f;  // Suavidade do movimento

    void LateUpdate()
    {
        if (target == null) return;

        // Posição desejada da câmera
        Vector3 desiredPosition = target.position + offset;

        // Movimento suave
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

        transform.position = smoothedPosition;

        // Faz a câmera olhar pro carro (opcional)
        transform.LookAt(target);
    }
}
