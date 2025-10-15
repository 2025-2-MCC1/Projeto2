using UnityEngine;

public class ObstacleMovement : MonoBehaviour
{
    public float speed = 5f;
    private float zBound = -15f;

    void Update()
    {
        transform.Translate(Vector3.back * speed * Time.deltaTime);

        if (transform.position.z < zBound)
        {
            Destroy(gameObject);
        }
    }
}
