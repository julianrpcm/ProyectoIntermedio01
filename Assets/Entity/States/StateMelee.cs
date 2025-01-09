using UnityEngine;

public class StateMelee : BaseState
{
    protected override void Update()
    {
        base.Update();

        LookAtTarget();
        entity.weaponManager.PerformAttack();

    }
}
