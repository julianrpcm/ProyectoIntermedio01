using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class BarrelByInstantiation : BarrelBase
{
    [SerializeField] GameObject projectile;

    [Header("References")]
    [SerializeField] private WeaponManager weaponManager;

    [Header("Variables")]
    [HideInInspector] public bool canShoot;

    [Header("Shotgun")]
    private int shotgunCartridgesShot = 8;
    [SerializeField] private float shotgunCartridgesAngle = 140f;

    [Header("Ammo")]
    [SerializeField] private float subMachineGunReloadingTime = 2f;
    [SerializeField] private float shotgunReloadingTime = 4f;
    [SerializeField] private float grenadeLauncherReloadingTime = 10f;

    [HideInInspector] public bool isReloading = false;

    private void Start()
    {
        canShoot = true;
        isReloading = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && !isReloading)
            ReloadCurrentWeapon();
    }

    private void ReloadCurrentWeapon()
    {
        if (projectile.gameObject.name == "SubMachineGunBullet")
            StartCoroutine(weaponManager.Reload(subMachineGunReloadingTime, 1, this));
        else if (projectile.gameObject.name == "Grenade")
            StartCoroutine(weaponManager.Reload(grenadeLauncherReloadingTime, 3, this));
        else if (projectile.gameObject.name == "ShotgunBullet")
            StartCoroutine(weaponManager.Reload(shotgunReloadingTime, 2, this));
    }

    public override void Shoot()
    {
        if (canShoot && !isReloading)
        {
            canShoot = false;
            if (projectile.gameObject.name == "SubMachineGunBullet" && weaponManager.subMachineGunAmmo != 0 && weaponManager.subMachineGunHasAmmo)
            {
                weaponManager.contadorMachineGunShootDelay = 0f;
                weaponManager.subMachineGunAmmo--;
                //Debug.Log("subMachineGunAmmo: " + weaponManager.subMachineGunAmmo);
                Instantiate(projectile, transform.position, transform.rotation);
                if (weaponManager.subMachineGunAmmo == 0)
                {
                    weaponManager.subMachineGunHasAmmo = false;
                    //StartCoroutine(weaponManager.Reload(subMachineGunReloadingTime, 1, this));
                }
            }
            else if (projectile.gameObject.name == "Grenade" && weaponManager.grenadeLauncherAmmo != 0 && weaponManager.grenadeLauncherHasAmmo)
            {
                weaponManager.contadorGrenadeLauncherShootDelay = 0f;
                weaponManager.grenadeLauncherAmmo--;
                //Debug.Log("grenadeLauncherAmmo: " + weaponManager.grenadeLauncherAmmo);
                Instantiate(projectile, transform.position, transform.rotation);
                if (weaponManager.grenadeLauncherAmmo == 0)
                {
                    weaponManager.grenadeLauncherHasAmmo = false;
                    //StartCoroutine(weaponManager.Reload(grenadeLauncherReloadingTime, 3, this));
                }
            }
            else if (projectile.gameObject.name == "ShotgunBullet" && weaponManager.shotgunAmmo != 0 && weaponManager.shotgunHasAmmo)
            {
                weaponManager.contadorShotgunShootDelay = 0f;
                weaponManager.shotgunAmmo--;
                //Debug.Log("shotgunAmmo: " + weaponManager.shotgunAmmo);

                float distanceBetweenBullets = 0f;
                List<GameObject> instantiatedProjectiles = new List<GameObject>();

                for (int i = 0; i < shotgunCartridgesShot; i++)
                {
                    //float randomInstantiationAngle = Random.Range((-shotgunCartridgesAngle / 2), (shotgunCartridgesAngle / 2));
                    float instantiationAngle = (-shotgunCartridgesAngle / 2) + distanceBetweenBullets;
                    Quaternion spreadRotation = Quaternion.Euler(0f, instantiationAngle, 0f);

                    GameObject newProjectile = Instantiate(projectile, transform.position, transform.rotation * spreadRotation);
                    //audioSource.Play();

                    distanceBetweenBullets += 20f;

                    foreach (GameObject otherProjectile in instantiatedProjectiles)
                    {
                        if (newProjectile.TryGetComponent<Collider>(out Collider newCollider) &&
                            otherProjectile.TryGetComponent<Collider>(out Collider otherCollider))
                        {
                            Physics.IgnoreCollision(newCollider, otherCollider);
                        }
                    }

                    instantiatedProjectiles.Add(newProjectile);
                    //ChooseShootFlash(2);
                }

                if (weaponManager.shotgunAmmo == 0)
                {
                    weaponManager.shotgunHasAmmo = false;
                    //StartCoroutine(weaponManager.Reload(shotgunReloadingTime, 2, this));
                }
            }
        }
    }

    /*
    public override void Shoot()
    {
        if (canShoot)
        {
            if (projectile.gameObject.name == "SubMachineGunBullet")
                weaponManager.contadorMachineGunShootDelay = 0f;
            if (projectile.gameObject.name == "ShotgunBullet")
                weaponManager.contadorShotgunShootDelay = 0f;
            if (projectile.gameObject.name == "Grenade")
                weaponManager.contadorGrenadeLauncherShootDelay = 0f;

            canShoot = false;

            Instantiate(projectile, transform.position, transform.rotation);

            if (projectile.gameObject.name == "ShotgunBullet")
            {
                {
                    //int shotgunCartridgesShot = Random.Range(minShotgunCartridges, maxShotgunCartridges+1);
                    //Debug.Log("postas disparadas = " + shotgunCartridgesShot);

                    float distanceBetweenBullets = 0f;
                    List<GameObject> instantiatedProjectiles = new List<GameObject>();

                    for (int i = 0; i < shotgunCartridgesShot; i++)
                    {
                        //float randomInstantiationAngle = Random.Range((-shotgunCartridgesAngle / 2), (shotgunCartridgesAngle / 2));
                        float instantiationAngle = (-shotgunCartridgesAngle / 2) + distanceBetweenBullets;
                        Quaternion spreadRotation = Quaternion.Euler(0f, instantiationAngle, 0f);

                        GameObject newProjectile = Instantiate(projectile, transform.position, transform.rotation * spreadRotation);
                        //audioSource.Play();

                        distanceBetweenBullets += 20f;

                        foreach (GameObject otherProjectile in instantiatedProjectiles)
                        {
                            if (newProjectile.TryGetComponent<Collider>(out Collider newCollider) &&
                                otherProjectile.TryGetComponent<Collider>(out Collider otherCollider))
                            {
                                Physics.IgnoreCollision(newCollider, otherCollider);
                            }
                        }

                        instantiatedProjectiles.Add(newProjectile);
                        //ChooseShootFlash(2);
                    }
                }
            }
        }
    }
    */
}