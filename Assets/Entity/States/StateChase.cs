using UnityEngine;

public class StateChase : BaseState
{

    protected override void Update()
    {
        if (entity.GetTarget() != null)
        {
            entity.agent.destination = entity.GetTarget().GetTransform().position;
        }

    }
}
