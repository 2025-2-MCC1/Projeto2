using UnityEngine;

[RequireComponent(typeof(Collider))]
public class WallSlowDown : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            CarForward car = collision.gameObject.GetComponent<CarForward>();
            if (car != null)
            {
                car.SlowDownToBaseSpeed();
            }
        }
    }

    // opcional: também funciona se quiser que a parede seja "trigger"
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CarForward car = other.GetComponent<CarForward>();
            if (car != null)
            {
                car.SlowDownToBaseSpeed();
            }
        }
    }
}
