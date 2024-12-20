using UnityEngine;

public class AudioController : MonoBehaviour
{
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.Play(); // Inicia el audio al entrar a la escena
    }

    public void StopAudio()
    {
        audioSource.Stop(); // Detiene el audio cuando lo necesites
    }
}
