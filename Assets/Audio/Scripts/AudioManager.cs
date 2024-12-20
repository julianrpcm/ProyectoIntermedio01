using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioSource audioSource; // Referencia al AudioSource

    public AudioClip defeatMusic; // M�sica de derrota
    public AudioClip backgroundMusic; // M�sica de fondo normal


    private void Awake()
    {
        audioSource = GetComponentInChildren<AudioSource>();
    }

    private void Start()
    {
        // Iniciar la m�sica de fondo (si es necesario al principio)
        if (audioSource != null)
        {
            audioSource.clip = backgroundMusic;
            audioSource.Play();
        }
    }

    public void PlayDefeatMusic()
    {
        Debug.Log("Cambio de musica");
        // Detener la m�sica actual
        if (audioSource != null)
        {
            Debug.Log("Stop");
            audioSource.Stop();
        }

        // Cambiar al clip de m�sica de derrota
        audioSource.clip = defeatMusic;

        // Reproducir la nueva m�sica
        audioSource.Play();
    }
}
