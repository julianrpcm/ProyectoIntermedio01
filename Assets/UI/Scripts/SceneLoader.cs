using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu"); 
    }

    public void RestartScene()
    {
        SceneManager.LoadScene("OutdoorsScene");
    }
}

