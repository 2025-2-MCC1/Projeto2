using UnityEngine;

public class Collidplayer : MonoBehaviour
{
    private Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true; //para evitar o player girar por causa da física
    }

    // Update is called once per frame
    private void OnCollisionEnter(Collision collision )
    {
        if (collision.gameObject.CompareTag("Parede"))
        {
            Debug.Log("na parede ");
        }
    }
}
