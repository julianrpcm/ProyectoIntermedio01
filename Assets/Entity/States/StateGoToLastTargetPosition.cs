using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class StateGoToLastTargetPosition : BaseState
{

    [SerializeField] float visitThreshold = 1.5f;

    protected override void OnEnable()
    {
        base.OnEnable();
        entity.agent.isStopped = false;
    }

    protected override void Update()
    {
        base.Update();
        entity.agent.SetDestination(entity.lastTargetPosition);

        if(Vector3.Distance(transform.position, entity.lastTargetPosition) < visitThreshold)
        {
            entity.lastTargetPositionIsNotVisible = false;
        }
    }
}
