using UnityEngine;

public class GameOverData : MonoBehaviour
{
    public float tempoDeCorrida;
    public int gasolinasColetadas;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        Debug.Log(gasolinasColetadas);
    }
}
