using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioSource audioSource; // Referencia al AudioSource

    public AudioClip defeatMusic; // Música de derrota
    public AudioClip backgroundMusic; // Música de fondo normal


    private void Awake()
    {
        audioSource = GetComponentInChildren<AudioSource>();
    }

    private void Start()
    {
        // Iniciar la música de fondo (si es necesario al principio)
        if (audioSource != null)
        {
            audioSource.clip = backgroundMusic;
            audioSource.Play();
        }
    }

    public void PlayDefeatMusic()
    {
        Debug.Log("Cambio de musica");
        // Detener la música actual
        if (audioSource != null)
        {
            Debug.Log("Stop");
            audioSource.Stop();
        }

        // Cambiar al clip de música de derrota
        audioSource.clip = defeatMusic;

        // Reproducir la nueva música
        audioSource.Play();
    }
}
