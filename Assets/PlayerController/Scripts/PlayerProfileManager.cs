using UnityEngine;

public class PlayerProfileManager : MonoBehaviour
{
    [SerializeField] private PlayerProfile profileMovement;
    [SerializeField] private PlayerProfile profileAiming;
    [SerializeField] private PlayerProfile profileAimingWithTarget;

    [SerializeField] private TargetLock targetLock;

    private WeaponManager weaponManager;
    private bool isAiming;
    //IPerceptible currentTarget;

    PlayerProfile currentProfile;

    private void Awake()
    {
        weaponManager = GetComponent<WeaponManager>();
    }

    private void OnEnable()
    {
        weaponManager.onStartAiming.AddListener(OnStartAiming);
        weaponManager.onStopAiming.AddListener(OnStopAiming);

        //targetLock.onTargetAcquired.AddListener(OnTargetAcquired);
        //targetLock.onTargetLost.AddListener(OnTargetLost);
    }

    private void Update()
    {
        PlayerProfile desiredProfile = profileMovement;

        if (isAiming)
        {
            //if (currentTarget != null)
            //    desiredProfile = profileAimingWithTarget;
            //else
            //    desiredProfile = profileAiming;
            desiredProfile = profileAiming; // Esta linea se tiene que quitar cuando se active lo de arriba
        }
        // If Has Target entonses

        if (currentProfile != desiredProfile)
        {
            currentProfile?.Deactivate();
            currentProfile = desiredProfile;
            currentProfile?.Activate();
        }
    }

    private void OnDisable()
    {
        weaponManager.onStartAiming.RemoveListener(OnStartAiming);
        weaponManager.onStopAiming.RemoveListener(OnStopAiming);

        //targetLock.onTargetAcquired.RemoveListener(OnTargetAcquired);
        //targetLock.onTargetLost.RemoveListener(OnTargetLost);
    }

    private void OnStartAiming()
    {
        isAiming = true;
    }

    private void OnStopAiming()
    {
        isAiming = false;
    }

    //private void OnTargetAcquired(IPerceptible targetAcquired)
    //{
    //    currentTarget = TargetAcquired;
    //    profileAimingWithTarget.customTarget = targetAcquired.GetTransform();
    //}

    //private void OnTargetLost(IPerceptible targetLost)
    //{
    //    currentTarget = null;
    //    profileAimingWithTarget.customTarget = null;
    //}
}