using UnityEngine;
using UnityEngine.Playables; // Para controlar el Timeline
using UnityEngine.SceneManagement; // Para cargar la escena

public class TriggerTimelineAndLoadScene : MonoBehaviour
{
    public PlayableDirector timeline; // Arrastra tu Timeline aquí en el Inspector
    public string sceneToLoad = "WinnerScene"; // Nombre de la escena a cargar
    private bool hasTriggered = false; // Evitar múltiples activaciones
    public GameObject player;

    private void OnTriggerEnter(Collider other)
    {
        // Verifica si el objeto que entra es el jugador (tagged como "Player")
        if (!hasTriggered && other.CompareTag("Player"))
        {
            hasTriggered = true; // Asegura que solo se activa una vez

            if (timeline != null)
            {
                timeline.Play(); // Reproduce el Timeline
                timeline.stopped += OnTimelineStopped; // Ejecuta algo al finalizar
            }
        }
    }

    private void OnTimelineStopped(PlayableDirector director)
    {
        if (director == timeline) // Asegúrate de que sea el Timeline correcto
        {
            SceneManager.LoadScene(sceneToLoad); // Carga la escena
        }
    }
}

