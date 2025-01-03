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

    private void Start()
    {
        canShoot = true;
    }

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
}