using UnityEngine;

public class Horizontal : MonoBehaviour
{
    private float SideSpeed = 3.5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetAxisRaw("Horizontal") > 0) && transform.position.x == (11.46f))
        {
            transform.Translate(SideSpeed * Time.deltaTime, 0, 0);
        }
        else if ((Input.GetAxisRaw("Horizontal") < 0) && transform.position.x == (-1.35f))
        {
            transform.Translate(-SideSpeed * Time.deltaTime, 0, 0);
        }


    }
}

