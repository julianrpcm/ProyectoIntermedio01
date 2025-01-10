using UnityEngine;

public class StateChase : BaseState
{

    protected override void Update()
    {
        if (GetTarget() != null)
            entity.agent.destination = GetTarget().GetTransform().position;
    }
}