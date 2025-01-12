using UnityEngine;
using System.Collections.Generic;

public class DynamicCameraObstacleHandler : MonoBehaviour
{
    public Transform player; // Transform del jugador
    public GameObject cameraSystem; // El objeto que contiene las cámaras
    public LayerMask obstacleLayer; // Capa de obstáculos a detectar
    public float transparencyAlpha = 0.3f; // Nivel de transparencia al hacer los objetos transparentes
    public float fadeSpeed = 5f; // Velocidad de transición entre opaco y transparente

    private Camera activeCamera;
    private List<Renderer> currentObstacles = new List<Renderer>();
    private Dictionary<Renderer, Material[]> originalMaterials = new Dictionary<Renderer, Material[]>();

    void Update()
    {
        // Detectar la cámara activa
        activeCamera = GetActiveCamera();
        if (activeCamera == null || player == null) return;

        // Restaurar obstáculos previos
        RestoreObstacles();

        // Detectar nuevos obstáculos
        Vector3 direction = (player.position - activeCamera.transform.position).normalized;
        float distance = Vector3.Distance(player.position, activeCamera.transform.position);

        RaycastHit[] hits = Physics.RaycastAll(activeCamera.transform.position, direction, distance, obstacleLayer);

        foreach (var hit in hits)
        {
            Renderer renderer = hit.collider.GetComponent<Renderer>();
            if (renderer != null && !currentObstacles.Contains(renderer))
            {
                // Guardar materiales originales si no están registrados
                if (!originalMaterials.ContainsKey(renderer))
                {
                    originalMaterials[renderer] = renderer.materials;
                }

                // Aplicar transparencia al objeto
                ApplyTransparency(renderer);
                currentObstacles.Add(renderer);
            }
        }
    }

    private Camera GetActiveCamera()
    {
        // Iterar sobre todas las cámaras del CameraSystem y encontrar la activa
        Camera[] cameras = cameraSystem.GetComponentsInChildren<Camera>();
        foreach (Camera cam in cameras)
        {
            if (cam.gameObject.activeSelf)
            {
                return cam;
            }
        }
        return null;
    }

    private void ApplyTransparency(Renderer renderer)
    {
        foreach (Material material in renderer.materials)
        {
            if (material.HasProperty("_Color"))
            {
                Color color = material.color;
                color.a = Mathf.Lerp(color.a, transparencyAlpha, Time.deltaTime * fadeSpeed);
                material.color = color;
            }

            if (material.HasProperty("_Surface"))
            {
                // Cambiar el modo de renderizado para soportar transparencia (URP o HDRP)
                material.SetFloat("_Surface", 1f); // Transparente
                material.SetFloat("_Blend", 0.5f); // Alpha blending
            }
        }
    }

    private void RestoreObstacles()
    {
        foreach (Renderer renderer in currentObstacles)
        {
            if (renderer != null && originalMaterials.ContainsKey(renderer))
            {
                Material[] materials = renderer.materials;
                for (int i = 0; i < materials.Length; i++)
                {
                    if (materials[i].HasProperty("_Color"))
                    {
                        Color color = materials[i].color;
                        color.a = Mathf.Lerp(color.a, 1f, Time.deltaTime * fadeSpeed);
                        materials[i].color = color;
                    }

                    if (materials[i].HasProperty("_Surface"))
                    {
                        // Restaurar el modo de renderizado original
                        materials[i].SetFloat("_Surface", 0f); // Opaque
                        materials[i].SetFloat("_Blend", 0f);
                    }
                }
            }
        }

        // Limpiar lista de obstáculos
        currentObstacles.Clear();
    }
}

