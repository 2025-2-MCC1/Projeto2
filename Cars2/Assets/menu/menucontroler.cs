using UnityEngine;
using UnityEngine.Video;

public class menucontroler : MonoBehaviour 
{
    public VideoPlayer videoPlayer;
    public GameObject menuopcoes;
    public GameObject titulo;
    public GameObject video; 

    void Start()
    {
        
    }

    
    void Update()
    {
        if (Input.anyKeyDown)
        {
            videoPlayer.Play();
            menuopcoes.SetActive(true);
            titulo.SetActive(false);
            video.SetActive(true);
        }
    }
}
