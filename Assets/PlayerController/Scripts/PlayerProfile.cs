using UnityEngine;
using Unity.Cinemachine;

public class PlayerProfile : MonoBehaviour
{
    public string nombre;
    public CinemachineCamera cinemachineCamera;
    public PlayerController.OrientationMode orientationMode;
    public enum TargetForOrientationMode
    {
        None,
        UseCustomTarget,
    }
    public TargetForOrientationMode targetForOrientationMode;
    public Transform customTarget;
    public GameObject[] objectsToActivate;

    //[Header("Debug")]
    //[SerializeField] private bool debugActivate;
    //[SerializeField] private bool debugDeactivate;

    private PlayerController playerController;

    //private void OnValidate()
    //{
    //    if (debugActivate)
    //    {
    //        debugActivate = false;
    //        Activate();
    //    }

    //    if (debugDeactivate)
    //    {
    //        debugDeactivate = false;
    //        Deactivate();
    //    }
    //}

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
        enabled = false;
    }

    public void Activate()
    {
        cinemachineCamera.gameObject.SetActive(true);
        playerController.SetExternalOrientationMode(orientationMode);
        enabled = true;

        foreach (GameObject go in objectsToActivate)
            go.SetActive(true);
    }

    private void Update()
    {
        switch (targetForOrientationMode)
        {
            case TargetForOrientationMode.None:
                break;
            case TargetForOrientationMode.UseCustomTarget:
                playerController.SetExternalTarget(customTarget);
                break;
        }
    }

    public void Deactivate()
    {
        cinemachineCamera.gameObject.SetActive(false);
        playerController.UnSetExternalOrientationMode();
        if (targetForOrientationMode == TargetForOrientationMode.UseCustomTarget)
            playerController.UnSetExternalTarget();
        enabled = false;

        foreach (GameObject go in objectsToActivate)
            go.SetActive(false);
    }
}