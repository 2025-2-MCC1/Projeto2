using UnityEngine;

public class Horizontal : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            transform.Translate(0.04f, 0, 0);
        }
        else if(Input.GetAxisRaw("Horizontal")< 0)
        {
            transform.Translate(-0.04f, 0, 0);
        }
            

    }
}
