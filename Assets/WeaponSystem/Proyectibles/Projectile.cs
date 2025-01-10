using DG.Tweening;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    [SerializeField] float startSpeed = 10f;
    [SerializeField] float lifeTime = 10f;
    [SerializeField] GameObject explosionPrefab;
    [SerializeField] float timeToDieAfterCollision = 0f;

    HitCollider hitCollider;
    private void Awake()
    {
        hitCollider = GetComponent<HitCollider>();
    }

    private void OnEnable()
    {
        hitCollider?.onHit.AddListener(PerformDestruction);
    }

    private void Start()
    {
        GetComponent<Rigidbody>().linearVelocity = transform.forward * startSpeed;
        DOVirtual.DelayedCall(lifeTime, () => Destroy(gameObject));
    }

    Tween deathTween = null;
    private void OnCollisionEnter(Collision collision)
    {
        if (deathTween == null)
        {
            deathTween = DOVirtual.DelayedCall(
                timeToDieAfterCollision,
                () =>
                {
                    Instantiate(explosionPrefab, transform.position, Quaternion.identity);
                    Destroy(gameObject);
                }
                );
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Projectile")
            Destroy(gameObject);
    }

    private void OnDisable()
    {
        hitCollider?.onHit.RemoveListener(PerformDestruction);
    }
    public void PerformDestruction()
    {
        Destroy(gameObject);
    }
}
