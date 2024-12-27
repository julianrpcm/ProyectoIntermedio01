using UnityEngine;
using UnityEngine.Events;

public class TargetLock : MonoBehaviour
{
    //public UnityEvent <IPerceptible> onTargetAcquired;
    //public UnityEvent <IPerceptible> onTargetLost;

    [SerializeField] private WeaponManager weaponManager;
    //private EntitySight sight;
    private bool isAiming;

    //IPerceptible currentTarget;

    private void Awake()
    {
        //sight = GetComponent<EntitySight>();
    }

    private void OnEnable()
    {
        weaponManager.onStartAiming.AddListener(OnStartAiming);
        weaponManager.onStopAiming.AddListener(OnStopAiming);
    }

    private void Update()
    {
        //IPerceptible desiredPerceptible = isAiming? sight.GetClosestVisible() : null;

        //if (desiredPerceptible != currentTarget)
        //{
        //    if (currentTarget != null)
        //        onTargetLost.Invoke(currentTarget);

        //    currentTarget = desiredPerceptible;

        //    if (currentTarget != null)
        //        onTargetAcquired.Invoke(currentTarget);
        //}
    }

    private void OnDisable()
    {
        weaponManager.onStartAiming.RemoveListener(OnStartAiming);
        weaponManager.onStopAiming.RemoveListener(OnStopAiming);
    }

    private void OnStartAiming()
    {
        isAiming = true;
    }

    private void OnStopAiming()
    {
        isAiming = false;
    }
}