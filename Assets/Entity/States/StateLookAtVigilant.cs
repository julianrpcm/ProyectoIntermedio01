using UnityEngine;

public class StateLookAtVigilant : BaseState
{

    protected override void OnEnable()
    {
        base.OnEnable();
        entity.agent.isStopped = true;
        entity.weaponManager.PerformAim(true);
    }

    protected override void Update()
    {
        base.Update();  
        LookAtTarget();
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        entity.agent.isStopped = false;
        entity.weaponManager.PerformAim(false);
    }

}
