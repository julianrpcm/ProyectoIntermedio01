using UnityEngine;

public class StateFireWeapon : BaseState
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
        entity.weaponManager.PerformShoot();

        LookAtTarget();
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        entity.agent.isStopped = false;
        entity.weaponManager.PerformAim(false);
    }
}