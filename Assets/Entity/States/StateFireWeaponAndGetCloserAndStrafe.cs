using UnityEngine;

public class StateFireWeaponAndGetCloserAndStrafe : StateFireWeapon
{
    [SerializeField] float distanceForStrafint = 5f;
        
    protected override void OnEnable()
    {
        base.OnEnable();
        entity.agent.isStopped = false;
    }

    protected override void Update()
    {
        base.Update();  


        float distance = Vector3.Distance(transform.position, GetTarget().GetTransform().position);
        if (distance > distanceForStrafint)
        {
            entity.agent.destination = GetTarget().GetTransform().position;
        }
        else
        {
            entity.agent.destination = transform.position + transform.right;
        }
    }

}
