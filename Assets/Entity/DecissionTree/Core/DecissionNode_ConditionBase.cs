using UnityEngine;

public abstract class DecissionNode_ConditionBase : DecissionNode_Base
{
    DecissionNode_Base onConditionTrue;
    DecissionNode_Base onConditionFalse;

    protected Entity entity;

    private void Awake()
    {
        onConditionTrue = transform.GetChild(0).GetComponent<DecissionNode_Base>();
        onConditionFalse = transform.GetChild(1).GetComponent<DecissionNode_Base>();
    }
    public override BaseState Run()
    {
        if (Condition())
        {
            return onConditionTrue.Run();
        }
        else
        {
            return onConditionFalse.Run();
        }
    }

    public abstract bool Condition();

    public void SetEntity(Entity entity)
    {
        this.entity = entity;
    }
}
