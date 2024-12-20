using UnityEngine;
using TMPro;  // Necesario para trabajar con TextMeshPro

public class TextManager : MonoBehaviour
{
    public TextMeshPro tmpText;  // Referencia al componente TextMeshPro

    void Start()
    {
        tmpText.gameObject.SetActive(false);  // Aseg�rate de que el texto est� oculto al inicio
    }

    // M�todo para mostrar el texto
    public void ShowText(string message)
    {
        tmpText.text = message;
        tmpText.gameObject.SetActive(true);  // Hacer visible el texto
    }

    // M�todo para ocultar el texto
    public void HideText()
    {
        tmpText.gameObject.SetActive(false);  // Hacer invisible el texto
    }
}

