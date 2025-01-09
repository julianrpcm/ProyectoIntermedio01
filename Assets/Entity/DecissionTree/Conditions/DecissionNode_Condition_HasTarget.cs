using UnityEngine;

public class DecissionNode_Condition_HasTarget : DecissionNode_ConditionBase
{
    public override bool Condition()
    {
        return entity.GetTarget() != null;
    }
}
