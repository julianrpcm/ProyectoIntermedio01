using UnityEngine;

public class DamageFeedback : MonoBehaviour
{
    public Renderer objectRenderer; // Para objetos 3D.
    public SpriteRenderer spriteRenderer; // Para sprites 2D.
    public Color damageColor = Color.red; // Color al recibir daño.
    public float feedbackDuration = 0.2f; // Duración del efecto.

    private Color originalColor; // Para restaurar el color original.
    private bool isSpriteRenderer; // Para determinar el tipo de objeto.

    void Start()
    {
        // Detecta si el objeto usa SpriteRenderer o Renderer.
        if (spriteRenderer != null)
        {
            originalColor = spriteRenderer.color;
            isSpriteRenderer = true;
        }
        else if (objectRenderer != null)
        {
            originalColor = objectRenderer.material.color;
            isSpriteRenderer = false;
        }
        else
        {
            Debug.LogWarning("No Renderer o SpriteRenderer asignado.");
        }
    }

    public void TakeDamage()
    {
        // Llama al feedback visual y maneja la lógica de daño aquí.
        StartCoroutine(DamageEffect());
    }

    private System.Collections.IEnumerator DamageEffect()
    {
        // Cambia al color de daño.
        if (isSpriteRenderer)
        {
            spriteRenderer.color = damageColor;
        }
        else
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
        else
        {
            objectRenderer.material.color = originalColor;
        }
    }
}
