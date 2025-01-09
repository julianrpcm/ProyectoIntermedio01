using UnityEngine;

public class StatePatrol : BaseState
{
    [SerializeField] Transform patrolPointsParent;
    [SerializeField] int startingPatrolPointIndex;
    [SerializeField] float arrivalDistance = 2f;

    int currentPatrolPointIndex;

    protected override void Start()
    {
        base.Start();
        currentPatrolPointIndex = startingPatrolPointIndex;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        entity.agent.isStopped = false;
    }

    protected override void Update()
    {
        base.Update();

        Vector3 currentDestination = patrolPointsParent.GetChild(currentPatrolPointIndex).position;
        entity.agent.SetDestination(currentDestination);
        if(Vector3.Distance(transform.position, currentDestination) < arrivalDistance)
        {
            currentPatrolPointIndex++;
            if (currentPatrolPointIndex >= patrolPointsParent.childCount)
            {
                currentPatrolPointIndex = 0;
            }
        }
    }

    protected override void OnDisable()
    {
        base.OnDisable();
    }
}
