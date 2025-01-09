using System;
using UnityEngine;

public class DecissionTree : MonoBehaviour
{
    [SerializeField] DecissionNode_Base startingDecissionNode;

    BaseState[] states;

    BaseState currenteState;

    Entity entity;

    private void Awake()
    {
        states = GetComponents<BaseState>();
        foreach (BaseState bs in states) 
        {
            bs.enabled = false;
        }

        entity = GetComponent<Entity>();    

        foreach(DecissionNode_ConditionBase dncb in GetComponentsInChildren<DecissionNode_ConditionBase>())
        {
            dncb.SetEntity(entity);
        }

    }

    private void Update()
    {

        BaseState desiredState = startingDecissionNode.Run();

        if (currenteState != desiredState)
        {
            SetCurrentState(desiredState);
        }

    }

    private void SetCurrentState(BaseState newState)
    {
        if(currenteState != null)
        {
            currenteState.enabled = false;
        }

        currenteState = newState;

        if(currenteState != null)
        {
            currenteState.enabled = true;
        }
    }

    internal void Stop()
    {
        enabled = false;
        //currenteState.enabled = false;
        foreach (BaseState bs in states)
        {
            bs.enabled = false;
        }
    }

}
