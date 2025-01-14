using UnityEngine;

public class MouseUsable : MonoBehaviour
{
    private void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }
}