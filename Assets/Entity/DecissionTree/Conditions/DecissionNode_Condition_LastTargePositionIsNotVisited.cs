using UnityEngine;

public class DecissionNode_Condition_LastTargePositionIsNotVisited : DecissionNode_ConditionBase
{
    public override bool Condition()
    {
        return entity.lastTargetPositionIsNotVisible;
    }
}
