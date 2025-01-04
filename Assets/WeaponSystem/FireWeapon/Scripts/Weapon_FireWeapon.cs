/*
//using UnityEngine;

//public class Weapon_FireWeapon : WeaponBase
//{
//    BarrelBase[] barrels;

//    #region Debug
//    [Header("Debug")]
//    [SerializeField] bool debugShoot;

//    [SerializeField] bool debugStartShooting;
//    [SerializeField] bool debugStopShooting;

//    [SerializeField] bool debugShootBurst;
//    [SerializeField] bool debugCancelBurst;

//    private void OnValidate()
//    {
//        if (debugShoot)
//        {
//            debugShoot = false;
//            Shoot();
//        }
//    }
//    #endregion

//    internal override void Init()
//    {
//        base.Init();
//        barrels = GetComponentsInChildren<BarrelBase>();
//    }

//    public void Shoot()
//    {
//        foreach(BarrelBase bb in barrels)
//        {
//            bb.Shoot();
//        }
//    }

//    public void StartShooting()
//    {

//    }

//    public void StopShooting()
//    {

//    }

//    internal override void PerformAttack()
//    {
//        //throw new System.NotImplementedException();
//    }

//}
*/

using System;
using UnityEngine;

public class Weapon_FireWeapon : WeaponBase
{
    public enum FireWeaponMode
    {
        ShotByShot,
        ContinuousShot,
    }

    [SerializeField] FireWeaponMode fireWeaponMode = FireWeaponMode.ShotByShot;
    BarrelBase[] barrels;

    #region Debug
    [Header("Debug")]
    [SerializeField] bool debugInit;

    [SerializeField] bool debugShoot = true;

    [SerializeField] bool debugStartShooting;
    [SerializeField] bool debugStopShooting;

    [SerializeField] bool debugShootBurst;
    [SerializeField] bool debugCancelBurst;

    [SerializeField] float shootsPerSecond = 3f;

    float lastShootTime;
    bool isShooting;

    private void OnValidate()
    {
        if (debugInit)
        {
            debugInit = false;
            Init();
        }

        if (debugShoot)
        {
            debugShoot = false;
            Shoot();
        }

        if (debugStartShooting)
        {
            debugStartShooting = false;
            StartShooting();
        }

        if (debugStopShooting)
        {
            debugStopShooting = false;
            StopShooting();
        }
    }
    #endregion

    internal override void Init()
    {
        base.Init();
        barrels = GetComponentsInChildren<BarrelBase>();
        lastShootTime = Time.time - (1f / shootsPerSecond);
    }

    private void Update()
    {
        if (isShooting)
        {
            PerformShoot();
        }
    }

    public void Shoot()
    {
        //Debug.Log("Disparo desde Shoot()");

        if ((fireWeaponMode == FireWeaponMode.ShotByShot))
        {
            PerformShoot();
        }
        else
        {
            throw new Exception("Tried to Shoot on a FireWerapon that is not a set as ShotByShot mode");
        }
    }

    public void StartShooting()
    {
        //Debug.Log("Disparo desde StartShooting()");

        if ((fireWeaponMode == FireWeaponMode.ContinuousShot))
        {
            isShooting = true;
        }
        else
        {
            throw new Exception("Tried to Shoot on a FireWerapon that is not a set as ContinuousShot mode");
        }
    }

    public void StopShooting()
    {
        if ((fireWeaponMode == FireWeaponMode.ContinuousShot))
        {
            isShooting = false;
        }
        else
        {
            throw new Exception("Tried to Shoot on a FireWerapon that is not a set as ContinuousShot mode");
        }
    }

    internal override void PerformAttack()
    {
        //throw new System.NotImplementedException();
    }

    internal override void Deselect(Animator animator)
    {
        base.Deselect(animator);

        isShooting = false;
    }

    private void PerformShoot()
    {
        //Debug.Log("Disparo desde PerformShoot()");

        if ((Time.time - lastShootTime) > (1f / shootsPerSecond))
        {
            lastShootTime = Time.time;
            foreach (BarrelBase bb in barrels)
            {
                bb.Shoot();
            }
        }
    }

    internal bool CanShootByShoot()
    {
        return fireWeaponMode == FireWeaponMode.ShotByShot;
    }

    internal bool CanContinuousShoot()
    {
        return fireWeaponMode == FireWeaponMode.ContinuousShot;
    }
}

