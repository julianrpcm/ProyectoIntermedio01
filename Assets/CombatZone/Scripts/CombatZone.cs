using UnityEngine;

public class CombatZone : MonoBehaviour
{
    [SerializeField] Transform[] parentsToActivate;
    CombatZoneShape shape;

    private void Awake()
    {
        shape = GetComponentInChildren<CombatZoneShape>();
        SetActivationState(false);
    }

    private void OnEnable()
    {
        shape.onPlayerEnter.AddListener(OnPlayerEnter);
        shape.onPlayerExit.AddListener(OnPlayerExit);
    }


    private void OnDisable()
    {
        shape.onPlayerEnter.RemoveListener(OnPlayerEnter);
        shape.onPlayerExit.RemoveListener(OnPlayerExit);
    }

    private void OnPlayerEnter(CombatZoneShape shape)
    {
        SetActivationState(true);
    }

    private void OnPlayerExit(CombatZoneShape shape)
    {
        SetActivationState(false);
    }

    private void SetActivationState(bool desiredActivation)
    {
        foreach (Transform t in parentsToActivate)
        {
            t.gameObject.SetActive(desiredActivation);
        }
    }
}
