using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour, IMovingAnimatable, IPerceptible
{
    //[Header("The Player")]
    //[SerializeField] Transform target;

    //[Header("Configurations")]
    //[SerializeField] float attackDistance = 2f;
    //[SerializeField] float timeBetweenAttacks = 2f;
    //[SerializeField] float attackDuration = 0.25f;

    //[Header("References")]
    //[SerializeField] Transform hitCollider;

    //float timeOfLastAttack;

    [SerializeField] IPerceptible.Faction faction = IPerceptible.Faction.Axis;
    Animator animator;
    EntityLife entityLife;
    NavMeshAgent agent;
    DecissionTree decissionTree;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        entityLife = GetComponent<EntityLife>();
        agent = GetComponent<NavMeshAgent>();
        decissionTree = GetComponent<DecissionTree>();

        //timeOfLastAttack = Time.time;
    }

    private void OnEnable()
    {
        entityLife.onDeath.AddListener(OnDeath);
    }

    //void Update()
    //{
    //    if (Vector3.Distance(target.position, transform.position) < attackDistance)
    //    {
    //        agent.isStopped = true;

    //        if ((Time.time - timeOfLastAttack) > timeBetweenAttacks)
    //        {
    //            animator.SetTrigger("Attack");
    //            hitCollider.gameObject.SetActive(true);
    //            DOVirtual.DelayedCall(attackDuration, () => hitCollider.gameObject.SetActive(false));
    //            timeOfLastAttack = Time.time;
    //        }
    //    }
    //    else
    //    {
    //        agent.isStopped = false;
    //        agent.destination = target.position;
    //    }
    //}

    private void OnDisable()
    {
        entityLife.onDeath.RemoveListener(OnDeath);
    }

    void OnDeath()
    {
        enabled = false;
        agent.enabled = false;
        GetComponent<Ragdollizer>()?.Ragdollize();
        animator.enabled = false;
        DOVirtual.DelayedCall(5f, () => Destroy(gameObject));
        decissionTree.Stop();

        //hitCollider.gameObject.SetActive(false);
    }

    bool IMovingAnimatable.GetIsGorunded()
    {
        return true;
    }

    float IMovingAnimatable.GetJumpProgress()
    {
        return 1f;
    }

    float IMovingAnimatable.GetNormalizedLocalForwardlVelocity()
    {

        return CalcNormalizedLocalVelocity().z;
    }

    float IMovingAnimatable.GetNormalizedLocalHorizontalVelocity()
    {
        return CalcNormalizedLocalVelocity().x;
    }

    private Vector3 CalcNormalizedLocalVelocity()
    {
        Vector3 localVelocity = transform.InverseTransformDirection(agent.velocity);
        Vector3 normalizedLocalVelocity = localVelocity / agent.speed;
        return normalizedLocalVelocity;
    }

    IPerceptible.Faction IPerceptible.GetFaction()
    {
        return faction;
    }

    Transform IPerceptible.GetTransform()
    {
        return transform;
    }

    Vector3 IPerceptible.GetOffSetForLineOfSightCheck()
    {
        return Vector3.up * 1.5f;
    }
}