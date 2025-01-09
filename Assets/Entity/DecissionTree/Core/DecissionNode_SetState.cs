using UnityEngine;

public class DecissionNode_SetState : DecissionNode_Base
{
    [SerializeField] BaseState stateToSet;

    public override BaseState Run()
    {
        return stateToSet;
    }
}
