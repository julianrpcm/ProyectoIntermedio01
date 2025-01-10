using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.EventSystems.EventTrigger;

[DefaultExecutionOrder(-1)]
public class Entity : MonoBehaviour
{
    internal NavMeshAgent agent;
    internal EntitySight sight;
    internal EntityHearing hearing;
    internal WeaponManager weaponManager;

    public Vector3 lastTargetPosition = Vector3.zero;
    public bool lastTargetPositionIsNotVisible = false;

    [SerializeField] private bool isAmbusher = false;
    
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        sight = GetComponentInChildren<EntitySight>();
        weaponManager = GetComponent<WeaponManager>();
        hearing = GetComponent<EntityHearing>();
    }

    public IPerceptible GetTarget()
    {
        IPerceptible target = null;
        IPerceptible closesAudible = null;
        if (!isAmbusher)
            closesAudible = hearing.GetClosestPerceptible();
        IPerceptible closesVisible = sight.GetClosestVisible();

        if ((closesAudible != null) && (closesVisible == null))
        {
            lastTargetPosition = closesAudible.GetTransform().position;
            lastTargetPositionIsNotVisible = true;
        }
        else
        {
            target = closesVisible;
        }
        
        //if ((closesAudible != null) && closesVisible != null)
        //{
        //    float distanceToVisible = Vector3.Distance(transform.position, closesVisible.GetTransform().position);
        //    float distanceToAudible = Vector3.Distance(transform.position, closesAudible.GetTransform().position);
        //    target = distanceToVisible < distanceToAudible ? closesVisible : closesAudible;

        //}
        //else
        //{
        //    target =
        //        (closesAudible == null) && (closesVisible == null) ? null :
        //        (closesAudible == null) ? closesVisible : closesAudible;
        //}

        if (target != null) 
        {
            lastTargetPosition = target.GetTransform().position;
            lastTargetPositionIsNotVisible = true;
        }

        return target;
    }
}
