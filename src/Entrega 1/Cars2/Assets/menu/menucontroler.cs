using UnityEngine;
using UnityEngine.Video;

public class menucontroler : MonoBehaviour 
{
    public VideoPlayer videoPlayer;
    public GameObject menuopcoes, rawImage;
    private Animator animatorRawImage;
    public GameObject titulo;
    public GameObject video; 

    void Start()
    {
        rawImage.SetActive(false);
        animatorRawImage = rawImage.GetComponent<Animator>();
    }

    
    void Update()
    {
        if (Input.anyKeyDown)
        {
            rawImage.SetActive(true);
            animatorRawImage.SetTrigger("FadeIn"); 
            videoPlayer.Play();
            menuopcoes.SetActive(true);
            titulo.SetActive(false);
            video.SetActive(true);
        }
    }
}
