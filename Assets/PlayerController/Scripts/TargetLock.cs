using UnityEngine;
using UnityEngine.Events;

public class TargetLock : MonoBehaviour
{
    [SerializeField] private WeaponManager weaponManager;

    public UnityEvent<IPerceptible> onTargetAcquired;
    public UnityEvent<IPerceptible> onTargetLost;

    private EntitySight sight;
    private bool isAiming;

    IPerceptible currentTarget;

    private void Awake()
    {
        sight = GetComponent<EntitySight>();
    }

    private void OnEnable()
    {
        weaponManager.onStartAiming.AddListener(OnStartAiming);
        weaponManager.onStopAiming.AddListener(OnStopAiming);
    }

    private void Update()
    {
        //Debug.Log("isAiming = " + isAiming);
        IPerceptible desiredPerceptible = isAiming ? sight.GetClosestVisible() : null;

        //Debug.Log("desiredPerceptible = " + desiredPerceptible);

        if (desiredPerceptible != currentTarget)
        {
            //Debug.Log("desiredPerceptible != currentTarget" );

            if (currentTarget != null)
                onTargetLost.Invoke(currentTarget);

            currentTarget = desiredPerceptible;

            if (currentTarget != null)
                onTargetAcquired.Invoke(currentTarget);
            //Debug.Log("currentTarget = " + currentTarget);
        }
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