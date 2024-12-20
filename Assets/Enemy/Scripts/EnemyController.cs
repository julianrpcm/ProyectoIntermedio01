using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [Header("The Player")]
    [SerializeField] Transform target;
    
    [Header("Configurations")]
    [SerializeField] float attackDistance = 2f;
    [SerializeField] float timeBetweenAttacks = 2f;
    [SerializeField] float attackDuration = 0.25f;

    [Header("References")]
    [SerializeField] Transform hitCollider;

    NavMeshAgent agent;
    Animator animator;

    float timeOfLastAttack;

    EntityLife entityLife;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        entityLife = GetComponent<EntityLife>();

        timeOfLastAttack = Time.time;
    }

    private void OnEnable()
    {
        entityLife.onDeath.AddListener(OnDeath);
    }

    void Update()
    {
        if (Vector3.Distance(target.position, transform.position) < attackDistance)
        {
            agent.isStopped = true;

            if ((Time.time - timeOfLastAttack) > timeBetweenAttacks)
            {
                animator.SetTrigger("Attack");
                hitCollider.gameObject.SetActive(true);
                DOVirtual.DelayedCall(attackDuration, () => hitCollider.gameObject.SetActive(false));
                timeOfLastAttack = Time.time;
            }
        }
        else
        {
            agent.isStopped = false;
            agent.destination = target.position;
        }
    }

    private void OnDisable()
    {
        entityLife.onDeath.RemoveListener(OnDeath);
    }

    void OnDeath()
    {
        enabled = false;
        GetComponent<Ragdollizer>()?.Ragdollize();
        agent.enabled = false;
        animator.enabled = false;
        hitCollider.gameObject.SetActive(false);

        DOVirtual.DelayedCall(5f, () => Destroy(gameObject));
    }
}
