using UnityEngine;

public class EntitySoundEmiter : MonoBehaviour
{
    [SerializeField] float radius = 10f;
    [SerializeField] bool emitOnlyOnMovement = false;
    [SerializeField] float emissionsPerSecond = 5f;
    [SerializeField] float movementDetectionThreshold = 0.01f;
    [SerializeField] LayerMask hearingObjectsLayerMask = Physics.DefaultRaycastLayers;

    float lastEmissionTime;
    Vector3 lastEmissionPosition;
    private void Awake()
    {
        lastEmissionTime = Time.time - (1f / emissionsPerSecond);
        lastEmissionPosition = transform.position;
    }

    private void Update()
    {
        if (!emitOnlyOnMovement || 
            (Vector3.Distance(transform.position, lastEmissionPosition) > movementDetectionThreshold))
        {
            if (Time.time - lastEmissionTime > (1f / emissionsPerSecond))
            {
                Emit();
            }
        }
    }

    void Emit()
    {
        lastEmissionTime += Time.time;
        lastEmissionPosition = transform.position;

        Collider[] colliders = Physics.OverlapSphere(transform.position, radius, hearingObjectsLayerMask);

        foreach (Collider c in colliders) 
        {
            c.GetComponent<EntityHearing>()?.NotifyHeardSoundEmitter(this);
        }
    }
}
