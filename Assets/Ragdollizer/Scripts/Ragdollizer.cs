using UnityEngine;

public class Ragdollizer : MonoBehaviour
{
    Collider[] colliders;
    Rigidbody[] rigidbodies;

    #region Debug

    [SerializeField] bool debugReagdolize;

    private void OnValidate()
    {
        if (debugReagdolize)
        {
            debugReagdolize = false;
            Ragdollize();
        }
    }
    #endregion

    private void Awake()
    {
        colliders = GetComponentsInChildren<Collider>();
        rigidbodies = GetComponentsInChildren<Rigidbody>();

        UnRagdollize();
    }

    void UnRagdollize()
    {
        foreach (Collider c in colliders) { c.enabled = false; }
        foreach (Rigidbody rb in rigidbodies) { rb.isKinematic = true; }
    }

   public void Ragdollize()
    {
        foreach (Collider c in colliders) { c.enabled = true; }
        foreach (Rigidbody rb in rigidbodies) { rb.isKinematic = false; }
    }
}
