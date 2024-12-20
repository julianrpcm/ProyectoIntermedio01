using UnityEngine;
using UnityEngine.SceneManagement;

public class UISystem : MonoBehaviour
{

    // Cargar una escena de juego.
    public void PlayGame()
    {
        SceneManager.LoadScene("OutdoorsScene"); // Cambia "GameScene" por el nombre de tu escena de juego.
    }

    // Salir del juego.
    public void QuitGame()
    {
        Debug.Log("Salir del juego"); // Esto solo se muestra en el editor.
        Application.Quit();
    }
}


