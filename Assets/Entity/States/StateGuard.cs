using UnityEngine;

public class StateGuard : BaseState
{
    [SerializeField] float positionThreshold = 1.5f;
    Vector3 originalPosition;
    Vector3 originalLookDirection;

    protected override void Start()
    {
        base.Start();
        originalPosition = transform.position;
        originalLookDirection =  Vector3.ProjectOnPlane(transform.forward , Vector3.up).normalized;
    }

    protected override void Update()
    {
        base.Update();
        if(Vector3.Distance(transform.position, originalPosition) > positionThreshold)
        {
            entity.agent.isStopped = false;
            entity.agent.destination = originalPosition;
        }
        else
        {
            entity.agent.isStopped = true;

            float angularDistance = Vector3.SignedAngle(transform.forward, originalLookDirection, Vector3.up);
            float angularSpeed = entity.agent.angularSpeed;
            float angleToApply = angularSpeed * Time.deltaTime;
            angleToApply = Mathf.Sign(angularDistance) * Mathf.Min(Mathf.Abs(angularDistance), angleToApply);

            Quaternion rotationToApply = Quaternion.AngleAxis(angleToApply, Vector3.up);
            transform.rotation = rotationToApply * transform.rotation;

        }
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        entity.agent.isStopped = false;
    }
}
