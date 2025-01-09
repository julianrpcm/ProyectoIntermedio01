using UnityEngine;

public class DecissionNode_Condition_CurrentWeaponIsMelee : DecissionNode_ConditionBase
{
    public override bool Condition()
    {
        WeaponBase currentWeapon = entity.weaponManager.GetCurrentWeapon();
        return currentWeapon is WeaponMelee_Base;
    }
}
