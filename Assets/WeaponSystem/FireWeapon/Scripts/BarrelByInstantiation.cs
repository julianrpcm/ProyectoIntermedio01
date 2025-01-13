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
    [SerializeField] private bool isPlayer;

    [Header("Shotgun")]
    private int shotgunCartridgesShot = 8;
    [SerializeField] private float shotgunCartridgesAngle = 140f;

    [Header("Ammo")]
    [SerializeField] private float subMachineGunReloadingTime = 2f;
    [SerializeField] private float grenadeLauncherReloadingTime = 10f;

    [HideInInspector] public bool isReloading = false;

    [Header("VFX")]
    [SerializeField] private GameObject shootLight;
    [SerializeField] private float timeToMakeShootLightDisappear_ForMachineGun = 0.05f;
    [SerializeField] private float timeToMakeShootLightDisappear_ForGrenadeLauncher = 0.5f;
    private float timeToMakeShootLightDisappear;

    [Header("SFX")]
    [SerializeField] private AudioSource audioSource;

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
            StartCoroutine(weaponManager.Reload(subMachineGunReloadingTime, 1, this.gameObject));
        else if (projectile.gameObject.name == "Grenade")
            StartCoroutine(weaponManager.Reload(grenadeLauncherReloadingTime, 3, this.gameObject));
        //else if (projectile.gameObject.name == "ShotgunBullet")
        //    StartCoroutine(weaponManager.Reload(shotgunReloadingTime, 2, this.gameObject));
    }

    public override void Shoot()
    {
        //Debug.Log("Shoot()");

        if (isPlayer)
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
                    audioSource.Play();
                    ChooseShootFlash(1);
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
                    audioSource.Play();
                    ChooseShootFlash(2);
                    if (weaponManager.grenadeLauncherAmmo == 0)
                    {
                        weaponManager.grenadeLauncherHasAmmo = false;
                        //StartCoroutine(weaponManager.Reload(grenadeLauncherReloadingTime, 3, this));
                    }
                }
                /*
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
                }*/
            }
        }
        else
            Instantiate(projectile, transform.position, transform.rotation);
    }

    private void ChooseShootFlash(int weapon)   // 1 = SubMachineGun | 2 = GrenadeLauncher
    {
        switch (weapon)
        {
            case 1:
                timeToMakeShootLightDisappear = timeToMakeShootLightDisappear_ForMachineGun;
                StartCoroutine("ShootFlash");
                break;
            case 2:
                timeToMakeShootLightDisappear = timeToMakeShootLightDisappear_ForGrenadeLauncher;
                StartCoroutine("ShootFlash");
                break;
        }
    }

    private IEnumerator ShootFlash()
    {
        shootLight.SetActive(true);
        yield return new WaitForSeconds(timeToMakeShootLightDisappear);
        shootLight.SetActive(false);
    }
}