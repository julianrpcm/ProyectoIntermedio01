using UnityEngine;

using UnityEngine.Events;

public class HurtCollider : MonoBehaviour
{
    /*
    public UnityEvent onHit;
    public UnityEvent <float> onHitWithDamage;

    private DamageFeedback damageFeedback; // Referencia al componente de feedback visual.

    private void Start()
    {
        // Intenta obtener automáticamente el componente DamageFeedback.
        damageFeedback = GetComponent<DamageFeedback>();
        if (damageFeedback == null)
        {
            Debug.LogWarning("DamageFeedback no encontrado en el objeto.");
        }
    }

    public void NotifyHit(IHitter hitter)
    {
        onHit.Invoke();
        onHitWithDamage.Invoke(hitter.GetDamage());
    }
    */

    public UnityEvent onHit; // Evento general al ser golpeado.
    public UnityEvent<float> onHitWithDamage; // Evento con la cantidad de daño.

    public Color damageColor = Color.red; // Color al recibir daño.
    public float feedbackDuration = 0.2f; // Duración del efecto.

    private Renderer objectRenderer; // Para objetos 3D.
    private SpriteRenderer spriteRenderer; // Para sprites 2D.
    private Color originalColor; // Para restaurar el color original.
    private bool isSpriteRenderer; // Determina si usa SpriteRenderer.

    public AudioSource damageAudioSource;
    public AudioClip damageSound;

    private void Start()
    {

        // Aseguramos que hay un AudioSource asignado.
        if (damageAudioSource == null)
        {
            damageAudioSource = GetComponent<AudioSource>();
        }

        // Intenta buscar automáticamente los renderizadores.
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            originalColor = spriteRenderer.color;
            isSpriteRenderer = true;
        }
        else
        {
            objectRenderer = GetComponentInChildren<Renderer>();
            if (objectRenderer != null)
            {
                originalColor = objectRenderer.material.color;
                isSpriteRenderer = false;
            }
        }

        // Aviso si no se encuentra un renderizador.
        if (spriteRenderer == null && objectRenderer == null)
        {
            Debug.LogWarning("No se encontró SpriteRenderer ni Renderer en el objeto.");
        }
    }

    public void NotifyHit(IHitter hitter)
    {
        // Invoca los eventos correspondientes.
        onHit.Invoke();
        onHitWithDamage.Invoke(hitter.GetDamage());

        // Activa el feedback visual.
        if (spriteRenderer != null || objectRenderer != null)
        {
            StartCoroutine(DamageEffect());
        }

        // Reproducir el sonido de daño si el AudioSource está asignado.
        if (damageAudioSource != null && damageSound != null)
        {
            damageAudioSource.PlayOneShot(damageSound); // Reproducir el sonido de daño
        }

    }

    private System.Collections.IEnumerator DamageEffect()
    {
        // Cambia al color de daño.
        if (isSpriteRenderer)
        {
            spriteRenderer.color = damageColor;
        }
        else if (objectRenderer != null)
        {
            objectRenderer.material.color = damageColor;
        }

        // Espera la duración del feedback.
        yield return new WaitForSeconds(feedbackDuration);

        // Restaura el color original.
        if (isSpriteRenderer)
        {
            spriteRenderer.color = originalColor;
        }
        else if (objectRenderer != null)
        {
            objectRenderer.material.color = originalColor;
        }
    }
}

