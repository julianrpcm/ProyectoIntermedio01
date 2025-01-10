using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class BarrelByRaycast : BarrelBase, IHitter
{

    [Header("References")]
    [SerializeField] private WeaponManager weaponManager;
    [SerializeField] private BarrelByRaycast originalBBR;

    [Header("Parameters")]
    [SerializeField] float damage = 1f;
    [SerializeField] float range = 50f;
    [SerializeField] LayerMask layerMask = Physics.DefaultRaycastLayers;
    [SerializeField] GameObject bulletTrailPrefab;
    [SerializeField] float horizontalDispersion = 5f;
    [SerializeField] float verticalDispersion = 3f;

    [Header("Variables")]
    [HideInInspector] public bool canShoot;
    [SerializeField] private bool isOriginalShotgun;
    [SerializeField] private bool isShotgun;
    [SerializeField] private bool isDesertEagle;
    [SerializeField] private bool isPlayer = false;

    [Header("Ammo")]
    [SerializeField] private float shotgunReloadingTime = 4f;
    [HideInInspector] public bool shotgunIsReloading = false;
    [SerializeField] private float desertEagleReloadingTime = 1.5f;
    [HideInInspector] public bool desertEagleIsReloading = false;

    [Header("VFX")]
    [SerializeField] private GameObject shootLight;
    [SerializeField] private float timeToMakeShootLightDisappear_ForDesertEagle = 0.1f;
    [SerializeField] private float timeToMakeShootLightDisappear_ForShotgun = 0.1f;
    private float timeToMakeShootLightDisappear;


    private void Start()
    {
        shotgunIsReloading = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (!shotgunIsReloading && isOriginalShotgun && isShotgun)
                ReloadShotgun();
            else if (!desertEagleIsReloading && isDesertEagle)
                ReloadDesertEagle();
        }
    }

    private void ReloadShotgun()
    {
        //Debug.Log("ReloadShotgun()");
        StartCoroutine(weaponManager.Reload(shotgunReloadingTime, 2, this.gameObject));
    }

    private void ReloadDesertEagle()
    {
        //Debug.Log("ReloadDesertEagle()");
        StartCoroutine(weaponManager.Reload(desertEagleReloadingTime, 4, this.gameObject));
    }

    public override void Shoot()
    {
        if (isPlayer)
        {
            if (isShotgun)
            {
                //Debug.Log("originalBBR.canShoot = " + originalBBR.canShoot);
                //Debug.Log("weaponManager.shotgunHasAmmo = " + weaponManager.shotgunHasAmmo);

                if (originalBBR.canShoot && !originalBBR.shotgunIsReloading && weaponManager.shotgunAmmo != 0 && weaponManager.shotgunHasAmmo)
                {
                    //Debug.Log("Disparo desde Shoot()");
                    MakeTheShot();
                    if (shootLight != null)
                        ChooseShootFlash(2);

                    if (isOriginalShotgun)
                    {
                        //Debug.Log("Entro al if de la escopeta");
                        weaponManager.contadorShotgunShootDelay = 0f;
                        weaponManager.shotgunAmmo--;
                        //Debug.Log("shotgunAmmo: " + weaponManager.shotgunAmmo);

                        if (weaponManager.shotgunAmmo == 0)
                        {
                            //Debug.Log("Estoy sin municion en la escopeta");

                            weaponManager.shotgunHasAmmo = false;
                            //StartCoroutine(weaponManager.Reload(shotgunReloadingTime, 2, this));
                        }
                    }
                }
            }
            else if (isDesertEagle)
            {
                if (canShoot && !desertEagleIsReloading && weaponManager.desertEagleAmmo != 0 && weaponManager.desertEagleHasAmmo)
                {
                    MakeTheShot();
                    ChooseShootFlash(1);

                    weaponManager.contadorDesertEagleShootDelay = 0f;
                    weaponManager.desertEagleAmmo--;

                    if (weaponManager.desertEagleAmmo == 0)
                        weaponManager.desertEagleHasAmmo = false;
                }
            }
        }
        else if (!isPlayer)
            MakeTheShot();
    }

    private void MakeTheShot()
    {
        //Debug.Log("MakeTheShot()");

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

    private void ChooseShootFlash(int weapon)   // 1 = DesertEagle | 2 = Shotgun
    {
        switch (weapon)
        {
            case 1:
                timeToMakeShootLightDisappear = timeToMakeShootLightDisappear_ForDesertEagle;
                StartCoroutine("ShootFlash");
                break;
            case 2:
                timeToMakeShootLightDisappear = timeToMakeShootLightDisappear_ForShotgun;
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