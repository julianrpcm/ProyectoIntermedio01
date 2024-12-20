using System.Linq;
using UnityEngine;

public class WeaponMelee_ByRaycast : WeaponBase, IHitter
{
    [SerializeField] float damage = 1f; 

    [SerializeField] Transform raycastStart;
    [SerializeField] Transform raycastEnd;

    [SerializeField] LayerMask layerMask = Physics.DefaultRaycastLayers;

    [SerializeField] float minPointDistance = 0.05f;
    [SerializeField] float attackDuration = 0.5f;

    [SerializeField] string[] affectedTags;

    float remainingAttackDuration = 0f;
    Vector3 oldRaycastStart;
    Vector3 oldRaycastEnd;

    private void Update()
    {
        remainingAttackDuration -= Time.deltaTime;
        if (remainingAttackDuration > 0)
        {
            float distanceBetweenStarts = (oldRaycastStart - raycastStart.position).magnitude;
            float distanceBetweenEnds = (oldRaycastEnd - raycastEnd.position).magnitude;
            int raysToUse = Mathf.CeilToInt(Mathf.Max(distanceBetweenStarts, distanceBetweenEnds) / minPointDistance);

            for (int i = 0; i < raysToUse; i++)
            {
                float t = (float)i / (float)raysToUse;
                Debug.DrawLine(
                    Vector3.Lerp(oldRaycastStart, raycastStart.position, t),
                    Vector3.Lerp(oldRaycastEnd, raycastEnd.position, t),
                    Color.red,
                    0.5f
                    );
                Vector3 startPoint = Vector3.Lerp(oldRaycastStart, raycastStart.position, t);
                Vector3 endPoint = Vector3.Lerp(oldRaycastEnd, raycastEnd.position, t);
                if (Physics.Linecast(
                    startPoint,
                    endPoint - startPoint,
                    out RaycastHit hit,
                    layerMask))
                {
                    if (affectedTags.Contains(hit.collider.tag))
                    {
                        hit.collider.GetComponent<HurtCollider>()?.NotifyHit(this);
                    }
                }

            }
            

            oldRaycastStart = raycastStart.position;
            oldRaycastEnd = raycastEnd.position;
        }
    }

    internal override void PerformAttack()
    {
        oldRaycastStart = raycastStart.position;
        oldRaycastEnd = raycastEnd.position;
        remainingAttackDuration = attackDuration;
    }

    public float GetDamage()
    {
        return damage;
    }
}
