using UnityEngine;

public class FloatingAndRotatingPowerUps : MonoBehaviour
{
    [Header("Rotación")]
    [SerializeField] private Vector3 rotationSpeed = new Vector3(0, 50, 0);

    [Header("Movimiento Vertical")]
    [SerializeField] private float floatAmplitude = 0.1f; 
    [SerializeField] private float floatSpeed = 1.5f; 

    private Vector3 startPosition;

    private void Start()
    {
        // Guarda la posición inicial para el movimiento vertical
        startPosition = transform.position;
    }

    private void Update()
    {
        // Aplica la rotación
        transform.Rotate(rotationSpeed * Time.deltaTime);

        // Aplica el movimiento vertical
        float newY = startPosition.y + Mathf.Sin(Time.time * floatSpeed) * floatAmplitude;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}
