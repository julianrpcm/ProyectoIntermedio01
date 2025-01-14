using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class CheckEntitiesEmpty : MonoBehaviour
{
    [SerializeField] private GameObject entities; // El objeto que contiene a los enemigos
    [SerializeField] private GameObject timelineObject; // Objeto con el PlayableDirector
    [SerializeField] private string victorySceneName = "WinnerScene"; // Nombre de la escena de victoria

    private PlayableDirector timeline; // Referencia al PlayableDirector
    private bool isTimelinePlayed = false; // Para evitar reproducir el Timeline varias veces

    private void Start()
    {
        if (timelineObject != null)
        {
            // Asegurarse de que el Timeline esté desactivado al inicio
            timelineObject.SetActive(false);
            timeline = timelineObject.GetComponent<PlayableDirector>();

            if (timeline != null)
            {
                // Suscribir el evento para detectar cuando el Timeline termina
                timeline.stopped += OnTimelineFinished;
            }
        }
        else
        {
            Debug.LogWarning("No se asignó el objeto del Timeline.");
        }
    }

    void Update()
    {
        // Verifica si el objeto entities no tiene hijos
        if (entities.transform.childCount == 0 && !isTimelinePlayed)
        {
            PlayTimeline();
        }
    }

    private void PlayTimeline()
    {
        isTimelinePlayed = true; // Marca como reproducido
        if (timelineObject != null && timeline != null)
        {
            timelineObject.SetActive(true); // Activa el objeto del Timeline
            timeline.Play(); // Reproduce el Timeline
            Debug.Log("Timeline activado y reproducido.");
        }
        else
        {
            Debug.LogWarning("Timeline no asignado o no encontrado.");
        }
    }

    private void OnTimelineFinished(PlayableDirector director)
    {
        // Verifica si es el Timeline esperado el que terminó
        if (director == timeline)
        {
            Debug.Log("Timeline terminado. Cargando escena de victoria...");
            SceneManager.LoadScene(victorySceneName);
        }
    }

    private void OnDestroy()
    {
        // Desuscribir el evento para evitar errores si el objeto es destruido
        if (timeline != null)
        {
            timeline.stopped -= OnTimelineFinished;
        }
    }
}

