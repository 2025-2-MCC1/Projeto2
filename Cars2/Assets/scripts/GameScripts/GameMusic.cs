using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class GameMusic : MonoBehaviour
{
    [Header("Música de Fundo")]
    public AudioClip backgroundMusic; // 🎵 Arraste sua música aqui
    public float musicVolume = 0.5f;

    private AudioSource audioSource;

    void Start()
    {
        // 🎧 Configura o áudio
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = backgroundMusic;
        audioSource.volume = musicVolume;
        audioSource.loop = true;
        audioSource.playOnAwake = false;

        // Toca a música assim que a cena iniciar
        audioSource.Play();
    }
}
