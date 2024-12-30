using Unity.VisualScripting;
using UnityEngine;

public class BarrelByRaycast : BarrelBase, IHitter
{

    [SerializeField] float damage = 1f;
    [SerializeField] float range = 50f;
    [SerializeField] LayerMask layerMask = Physics.DefaultRaycastLayers;
    [SerializeField] GameObject bulletTrailPrefab;
    [SerializeField] float horizontalDispersion = 5f;
    [SerializeField] float verticalDispersion = 3f;

    public override void Shoot()
    {
        Debug.Log("Disparo desde Shoot()");

        Vector3 shootDirection = CalculateForwardWithDispersion();
        Vector3 bulletStartPosition = transform.position;
        Vector3 bulletEndPosition = transform.position + (shootDirection * range);

        if (Physics.Raycast(transform.position, shootDirection, out RaycastHit hitInfo, range, layerMask))
        {
            hitInfo.collider.GetComponent<HurtCollider>()?.NotifyHit(this);
            bulletEndPosition = hitInfo.point;
        }
        Instantiate(bulletTrailPrefab)?.GetComponent<BulletTrail>()?.InitBullet(bulletStartPosition, bulletEndPosition);
    }

    private Vector3 CalculateForwardWithDispersion()
    {
        Vector3 shootDirection = transform.forward;
        float horizontalAngleToApply = Random.Range(-horizontalDispersion, horizontalDispersion);
        float verticalAngleToApply = Random.Range(-verticalDispersion, verticalDispersion);

        Quaternion horizontalRotationToAplly = Quaternion.AngleAxis(horizontalAngleToApply, transform.up);
        Quaternion verticalRotationToApply = Quaternion.AngleAxis(verticalAngleToApply, transform.up);
        shootDirection = verticalRotationToApply * (horizontalRotationToAplly * shootDirection);
        return shootDirection;
    }

    public float GetDamage()
    {
        return damage;
    }
}