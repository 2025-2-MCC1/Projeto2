using UnityEngine;

public class WheelSpin : MonoBehaviour
{
    public Transform[] wheels;   // arraste aqui as rodas no Inspector
    public float spinSpeed = 500f; // velocidade de rotação (ajuste no Inspector)
    public bool isMoving = true;  // controla se deve girar ou não

    void Update()
    {
        if (!isMoving) return;

        foreach (Transform wheel in wheels)
        {
            // gira as rodas no eixo local — ajuste o eixo se girar errado
            wheel.Rotate(Vector3.right, spinSpeed * Time.deltaTime, Space.Self);
        }
    }
}
