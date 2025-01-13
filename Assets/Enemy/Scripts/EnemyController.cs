using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour, IMovingAnimatable, IPerceptible
{
    [SerializeField] IPerceptible.Faction faction = IPerceptible.Faction.Axis;
    Animator animator;
    EntityLife entityLife;
    NavMeshAgent agent;
    DecissionTree decissionTree;
    [SerializeField] private GameObject deathSmoke;
    [SerializeField] private Transform hips;

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
        //DOVirtual.DelayedCall(4.6f, () => deathSmoke.SetActive(true));
        DOVirtual.DelayedCall(4.9f, () => Instantiate(deathSmoke, hips.position, Quaternion.identity));
        DOVirtual.DelayedCall(5f, () => EnemyDeath());
        decissionTree.Stop();

        //hitCollider.gameObject.SetActive(false);
    }

    private void EnemyDeath()
    {
        gameObject.transform.parent.GetComponent<DestroyEnemyParent>().StartCoroutine("BeDestroyed");
        Destroy(gameObject);
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