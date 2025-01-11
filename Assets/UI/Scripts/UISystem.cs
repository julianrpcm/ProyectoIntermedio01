using UnityEngine;
using UnityEngine.SceneManagement;

public class UISystem : MonoBehaviour
{
    [SerializeField] private GameObject controlsMenu;

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

    public void ControlPanels(GameObject panel)
    {
        panel.SetActive(!panel.activeInHierarchy);
    }
}