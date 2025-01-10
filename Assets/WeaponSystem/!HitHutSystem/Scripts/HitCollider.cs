using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class HitCollider : MonoBehaviour, IHitter
{
    [Header("Configuration")]
    public float damage;
    public string[] affectedTags;

    [Header("Events")]
    public UnityEvent onHit;

    private void OnCollisionEnter(Collision collision)
    {
        CheckCollider(collision.collider);
        //Debug.Log("OnCollisionEnter with: " + collision.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        CheckCollider(other);
        //Debug.Log("OnTriggerEnter with: " + other.gameObject);
    }

    private void CheckCollider(Collider otherCollider)
    {
        //Debug.Log("CheckCollider()");

        if (affectedTags.Contains(otherCollider.tag) &&  
            otherCollider.TryGetComponent<HurtCollider>(out HurtCollider hurtCollider))
        {
            //Debug.Log("Collides");
            hurtCollider.NotifyHit(this);
            onHit.Invoke();
        }
    }

    public float GetDamage()
    {
        return damage;
    }
}