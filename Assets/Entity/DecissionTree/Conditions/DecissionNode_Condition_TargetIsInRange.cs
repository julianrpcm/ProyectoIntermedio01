using UnityEngine;

public class DecissionNode_Condition_TargetIsInRange : DecissionNode_ConditionBase
{
    public override bool Condition()
    {
        WeaponBase currentWeapon = entity.weaponManager.GetCurrentWeapon();
        if (currentWeapon)
        {
            IPerceptible target = entity.GetTarget();
            float distance = Vector3.Distance(transform.position, target.GetTransform().position);

            return distance < currentWeapon.GetRange();
        }
        return false;
    }
}
