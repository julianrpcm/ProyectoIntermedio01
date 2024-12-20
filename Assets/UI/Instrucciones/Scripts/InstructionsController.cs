using UnityEngine;
using UnityEngine.Playables;

public class InstructionsController : MonoBehaviour
{
    public PlayableDirector timeline;  // Referencia al PlayableDirector
    public GameObject missionInstruction;  // Referencia al objeto de la instrucción "mission"
    public GameObject[] otherInstructions;  // Array de otras instrucciones

    void Start()
    {
        // Asegurarnos de que todas las instrucciones estén desactivadas excepto la de 'mission' al inicio
        DeactivateOtherInstructions();

        // Suscribirse al evento que detecta el final del Timeline
        if (timeline != null)
            timeline.stopped += OnTimelineStopped;
    }

    void OnTimelineStopped(PlayableDirector director)
    {
        // Mostrar todas las instrucciones cuando el Timeline termine
        if (director == timeline)
        {
            ActivateOtherInstructions();  // Activamos todas las demás instrucciones
            // Desactivar la instrucción 'mission' después de terminar el Timeline
            if (missionInstruction != null)
                missionInstruction.SetActive(false);
        }
    }

    // Método para desactivar todas las instrucciones excepto la de 'mission'
    void DeactivateOtherInstructions()
    {
        // Desactivamos todas las instrucciones que no sean 'mission'
        if (otherInstructions != null)
        {
            foreach (var instruction in otherInstructions)
            {
                instruction.SetActive(false);  // Desactivar otras instrucciones
            }
        }
    }

    // Método para activar todas las instrucciones
    void ActivateOtherInstructions()
    {
        if (otherInstructions != null)
        {
            foreach (var instruction in otherInstructions)
            {
                instruction.SetActive(true);  // Activar otras instrucciones
            }
        }
    }

    void OnDestroy()
    {
        // Limpiar la suscripción para evitar errores
        if (timeline != null)
            timeline.stopped -= OnTimelineStopped;
    }
}
