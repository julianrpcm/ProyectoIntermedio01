using UnityEngine;
using TMPro;  // Necesario para trabajar con TextMeshPro

public class TextManager : MonoBehaviour
{
    public TextMeshPro tmpText;  // Referencia al componente TextMeshPro

    void Start()
    {
        tmpText.gameObject.SetActive(false);  // Asegúrate de que el texto esté oculto al inicio
    }

    // Método para mostrar el texto
    public void ShowText(string message)
    {
        tmpText.text = message;
        tmpText.gameObject.SetActive(true);  // Hacer visible el texto
    }

    // Método para ocultar el texto
    public void HideText()
    {
        tmpText.gameObject.SetActive(false);  // Hacer invisible el texto
    }
}

