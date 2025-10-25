using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 8f;
    public float destroyZ = -10f; // depois de passar do jogador, destrói

    void Update()
    {
        // mover para frente local (ou world -Z dependiendo do setup)
        transform.Translate(Vector3.forward * -speed * Time.deltaTime, Space.World);

        if (transform.position.z < destroyZ)
            Destroy(gameObject);
    }
}
