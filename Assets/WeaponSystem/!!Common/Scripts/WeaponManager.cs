using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Animations.Rigging;
using DG.Tweening;
using UnityEngine.Events;
using System.Collections;
using TMPro;


[DefaultExecutionOrder(-10)]
public class WeaponManager : MonoBehaviour
{
    /* Deactivated
    //[Header("configurations")]
    //[SerializeField] Transform weaponsParent;

    //[Header("IK")]
    //[SerializeField] Rig armsRig;
    //[SerializeField] Rig aimRig;

    //[Header("Inputs Combat")]
    //[SerializeField] InputActionReference attack;
    //[SerializeField] InputActionReference nextPrevWeapon;

    //[Header("Inputs Combat")]
    //[SerializeField] InputActionReference shoot;
    //[SerializeField] InputActionReference continuousShoot;
    //[SerializeField] InputActionReference aim;

    //Animator animator;
    //RuntimeAnimatorController originalAnimatorController;

    //WeaponBase[] weapons;
    //int currentWeapon = -1;

    //private void Awake()
    //{
    //    animator = GetComponentInChildren<Animator>();
    //    originalAnimatorController = animator.runtimeAnimatorController;
    //    weapons = weaponsParent.GetComponentsInChildren<WeaponBase>(true);
    //    foreach(WeaponBase wb in weapons)
    //    {
    //        wb.Init();
    //    }
    //}

    //private void OnEnable()
    //{
    //    attack.action.Enable();
    //    nextPrevWeapon.action.Enable();
    //    shoot.action.Enable();
    //    continuousShoot.action.Enable();
    //    aim.action.Enable();

    //    attack.action.performed += OnAttack;

    //    nextPrevWeapon.action.performed += OnNextPrevWeapon;

    //    shoot.action.performed += OnShoot;
    //    continuousShoot.action.started += OnContinuousShoot;
    //    continuousShoot.action.canceled += OnContinuousShoot;
    //    aim.action.started += OnAim;
    //    aim.action.canceled += OnAim;


    //    foreach (AnimationEventForwarder aef in GetComponentsInChildren<AnimationEventForwarder>())
    //    {
    //        aef.onMeleeAttackEvent.AddListener(OnMeleeAttackEvent);
    //    }

    //}


    //private void Update()
    //{
    //    if (mustAttack)
    //    {
    //        mustAttack = false;
    //        animator.SetTrigger("Attack");
    //    }
    //}

    //private void OnDisable()
    //{
    //    attack.action.Disable();
    //    nextPrevWeapon.action.Disable();
    //    shoot.action.Disable();
    //    continuousShoot.action.Disable();
    //    aim.action.Disable();

    //    attack.action.performed -= OnAttack;

    //    nextPrevWeapon.action.performed -= OnNextPrevWeapon;

    //    shoot.action.performed -= OnShoot;
    //    continuousShoot.action.started -= OnContinuousShoot;
    //    continuousShoot.action.canceled -= OnContinuousShoot;
    //    aim.action.started -= OnAim;
    //    aim.action.canceled -= OnAim;

    //    foreach (AnimationEventForwarder aef in GetComponentsInChildren<AnimationEventForwarder>())
    //    {
    //        aef.onMeleeAttackEvent.RemoveListener(OnMeleeAttackEvent);
    //    }
    //}

    //bool mustAttack = false;
    //private void OnAttack(InputAction.CallbackContext ctx)
    //{
    //    mustAttack = true;
    //}

    //void OnNextPrevWeapon(InputAction.CallbackContext ctx)
    //{
    //    Vector2 readValue = ctx.ReadValue<Vector2>();
    //    int weaponToSet = currentWeapon;
    //    if (readValue.y > 0f) 
    //    {
    //        weaponToSet++;
    //        if(weaponToSet >= weapons.Length)
    //        {   
    //            weaponToSet = -1;   
    //        }
    //    }
    //    else
    //    {
    //        weaponToSet--;
    //        if(weaponToSet < -1)
    //        {
    //            weaponToSet = weapons.Length - 1;
    //        }
    //    }

    //    if(weaponToSet != currentWeapon)
    //    {
    //        SelectWeapon(weaponToSet);
    //    }
    //}

    //void OnShoot(InputAction.CallbackContext ctx)
    //{
    //    if (currentWeapon != -1) 
    //    {
    //        ((Weapon_FireWeapon)weapons[currentWeapon])?.Shoot();

    //    }
    //}

    //void OnContinuousShoot(InputAction.CallbackContext ctx)
    //{
    //    if ((currentWeapon != -1) && (weapons[currentWeapon] is Weapon_FireWeapon))
    //    {
    //        float value = ctx.ReadValue<float>();
    //        if (value > 0f)
    //        {
    //            ((Weapon_FireWeapon)weapons[currentWeapon]).StartShooting();
    //        }
    //        else
    //        {
    //            ((Weapon_FireWeapon)weapons[currentWeapon]).StopShooting();
    //        }
    //    }
    //}

    //bool isAiming = false;
    //void OnAim(InputAction.CallbackContext ctx)
    //{
    //    isAiming = false;
    //    if (currentWeapon != -1)
    //    {
    //        float value = ctx.ReadValue<float>();
    //        animator.SetBool("IsAiming", value > 0f);
    //        isAiming = (value > 0f) && weapons[currentWeapon] is Weapon_FireWeapon;
    //    }

    //    AnimateArmRigsWeight();
    //    AnimateAimRigsWeight();
    //}

    //void OnMeleeAttackEvent()
    //{
    //    weapons[currentWeapon].PerformAttack();
    //}

    //void SelectWeapon(int weaponToSet)
    //{
    //    //Desactivamos arma
    //    if(currentWeapon != -1)
    //    {
    //        weapons[currentWeapon].Deselect(animator);
    //        if (weapons[currentWeapon] is Weapon_FireWeapon)
    //        {
    //            animator.SetBool("IsAiming", false);
    //        }
    //    }

    //    //Asignamos
    //    currentWeapon = weaponToSet;
    //    //Activamos arma...
    //    if (currentWeapon != -1) 
    //    {
    //        weapons[currentWeapon].Select(animator);
    //        animator.SetBool("IsHoldingFireWeapon", weapons[currentWeapon] is Weapon_FireWeapon);


    //    }
    //    else //... o volvemos al animator original
    //    {
    //        animator.runtimeAnimatorController = originalAnimatorController;
    //    }

    //    AnimateArmRigsWeight();
    //    AnimateAimRigsWeight();

    //}

    //private void AnimateArmRigsWeight()
    //{
    //    //DOTween.To(
    //    //       () => armsRig.weight,
    //    //       (x) => armsRig.weight = x,
    //    //       (currentWeapon != -1) && (weapons[currentWeapon] is Weapon_FireWeapon) && isAiming? 1f : 0f,
    //    //       0.25f
    //    //       );
    //}

    //void AnimateAimRigsWeight() 
    //{
    //    DOTween.To(
    //           () => aimRig.weight,
    //           (x) => aimRig.weight = x,
    //           (currentWeapon != -1) && (weapons[currentWeapon] is Weapon_FireWeapon) && isAiming? 1f : 0f,
    //           0.25f
    //           );
    //}
    */

    [Header("configurations")]
    [SerializeField] Transform weaponsParent;
    [SerializeField] int startingWeaponIndex = -1;

    [Header("IK")]
    [SerializeField] Rig armsRig;
    [SerializeField] Rig aimRig;

    [Header("Events")]
    public UnityEvent onStartAiming;
    public UnityEvent onStopAiming;

    Animator animator;
    RuntimeAnimatorController originalAnimatorController;

    WeaponBase[] weapons;
    int currentWeapon = -1;

    [Header("FireWeapons")]
    [SerializeField] private BarrelByInstantiation machineGunBBI;
    [SerializeField] private BarrelByInstantiation shotgunBBI;
    [SerializeField] private BarrelByInstantiation grenadeLauncherBBI;
    [SerializeField] private float machineGunShootDelay;
    [SerializeField] private float shotgunShootDelay;
    [SerializeField] private float grenadeLauncherShootDelay;
    [HideInInspector] public float contadorMachineGunShootDelay;
    [HideInInspector] public float contadorShotgunShootDelay;
    [HideInInspector] public float contadorGrenadeLauncherShootDelay;

    [Header("Ammo")]
    public bool subMachineGunHasAmmo = true;
    public bool shotgunHasAmmo = true;
    public bool grenadeLauncherHasAmmo = true;

    public float subMachineGunAmmo = 30f;
    public float shotgunAmmo = 4f;
    public float grenadeLauncherAmmo = 1f;

    public float subMachineGunTotalAmmo = 300f;
    public float shotgunTotalAmmo = 40f;
    public float grenadeLauncherTotalAmmo = 10f;

    public float subMachineGunCharger = 30f;
    public float shotgunCharger = 4f;
    public float grenadeLauncherCharger = 1f;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI subMGAmmoUI;
    [SerializeField] private TextMeshProUGUI shotGAmmoUI;
    [SerializeField] private TextMeshProUGUI gLaunchAmmoUI;
    [SerializeField] private TextMeshProUGUI subMGReloadingText;
    [SerializeField] private TextMeshProUGUI shotGReloadingText;
    [SerializeField] private TextMeshProUGUI gLaunchReloadingText;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        originalAnimatorController = animator.runtimeAnimatorController;
        weapons = weaponsParent.GetComponentsInChildren<WeaponBase>(true);
        foreach (WeaponBase wb in weapons)
            wb.Init();
    }

    private void Start()
    {
        SelectWeapon(startingWeaponIndex);

        contadorMachineGunShootDelay = 0f;
        contadorShotgunShootDelay = 0f;
        contadorGrenadeLauncherShootDelay = 0f;

        subMachineGunHasAmmo = true;
        shotgunHasAmmo = true;
        grenadeLauncherHasAmmo = true;

        subMachineGunCharger = subMachineGunAmmo;
        shotgunCharger = shotgunAmmo;
        grenadeLauncherCharger = grenadeLauncherAmmo;
    }

    private void OnEnable()
    {
        foreach (AnimationEventForwarder aef in GetComponentsInChildren<AnimationEventForwarder>())
            aef.onMeleeAttackEvent.AddListener(OnMeleeAttackEvent);
    }

    private void Update()
    {
        if (mustAttack)
        {
            mustAttack = false;
            animator.SetTrigger("Attack");
        }

        if (!machineGunBBI.gameObject.activeInHierarchy)
        {
            machineGunBBI.isReloading = false;
            subMGReloadingText.gameObject.SetActive(false);
        }
        if (!shotgunBBI.gameObject.activeInHierarchy)
        {
            shotgunBBI.isReloading = false;
            shotGReloadingText.gameObject.SetActive(false);
        }
        if (!grenadeLauncherBBI.gameObject.activeInHierarchy)
        {
            grenadeLauncherBBI.isReloading = false;
            gLaunchReloadingText.gameObject.SetActive(false);
        }

        subMGAmmoUI.text = "" + subMachineGunAmmo + " / " + subMachineGunTotalAmmo;
        shotGAmmoUI.text = "" + shotgunAmmo + " / " + shotgunTotalAmmo;
        gLaunchAmmoUI.text = "" + grenadeLauncherAmmo + " / " + grenadeLauncherTotalAmmo;

        #region OneByOne
        contadorMachineGunShootDelay += Time.deltaTime;
        if (contadorMachineGunShootDelay >= machineGunShootDelay)
            machineGunBBI.canShoot = true;

        contadorShotgunShootDelay += Time.deltaTime;
        if (contadorShotgunShootDelay >= shotgunShootDelay)
            shotgunBBI.canShoot = true;

        contadorGrenadeLauncherShootDelay += Time.deltaTime;
        if (contadorGrenadeLauncherShootDelay >= grenadeLauncherShootDelay)
            grenadeLauncherBBI.canShoot = true;

        //baseballBatUses_image.fillAmount = baseballBatUses / 3f;
        #endregion
    }
    private void OnDisable()
    {
        foreach (AnimationEventForwarder aef in GetComponentsInChildren<AnimationEventForwarder>())
            aef.onMeleeAttackEvent.RemoveListener(OnMeleeAttackEvent);
    }

    bool mustAttack = false;
    public void OnAttack(InputAction.CallbackContext ctx)
    {
        PerformAttack();
    }

    public void PerformAttack()
    {
        mustAttack = true;
    }

    void OnNextPrevWeapon(InputAction.CallbackContext ctx)
    {
        Vector2 readValue = ctx.ReadValue<Vector2>();

        bool mustSelectNextWeapon = readValue.y > 0;
        PerformChangeToNextOrPreWeapon(mustSelectNextWeapon);
    }

    public int PerformChangeToNextOrPreWeapon(bool mustSelectNextWeapon)
    {
        int weaponToSet = currentWeapon;

        if (mustSelectNextWeapon)
        {
            weaponToSet++;
            if (weaponToSet >= weapons.Length)
                weaponToSet = -1;
        }
        else
        {
            weaponToSet--;
            if (weaponToSet < -1)
                weaponToSet = weapons.Length - 1;
        }

        if (weaponToSet != currentWeapon)
            SelectWeapon(weaponToSet);

        return weaponToSet;
    }

    //void OnShoot(InputAction.CallbackContext ctx)
    //{
    //    PerformShoot();
    //}

    public void PerformShoot()
    {
        if (
            (currentWeapon != -1) &&
            (weapons[currentWeapon] is Weapon_FireWeapon) &&
            ((Weapon_FireWeapon)weapons[currentWeapon]).CanShootByShoot()
           )
        {
            ((Weapon_FireWeapon)weapons[currentWeapon]).Shoot();
        }
    }

    void OnContinuousShoot(InputAction.CallbackContext ctx)
    {
        float value = ctx.ReadValue<float>();
        bool mustStarShooting = value > 0f;

        PerformStartOrStopShooting(mustStarShooting);
    }

    public void PerformStartOrStopShooting(bool startShooting)
    {
        if (
            (currentWeapon != -1) &&
            (weapons[currentWeapon] is Weapon_FireWeapon) &&
            ((Weapon_FireWeapon)weapons[currentWeapon]).CanContinuousShoot()
           )
        {
            if (startShooting)
                ((Weapon_FireWeapon)weapons[currentWeapon]).StartShooting();
            else
                ((Weapon_FireWeapon)weapons[currentWeapon]).StopShooting();
        }
    }


    //void OnAim(InputAction.CallbackContext ctx)
    //{

    //    float value = ctx.ReadValue<float>();
    //    bool mustAim = value > 0f;
    //    PerformAim(mustAim);
    //}

    bool isAiming = false;
    public void PerformAim(bool mustAim)
    {
        if (!isAiming && mustAim)
            onStartAiming.Invoke();
        else if (isAiming && !mustAim)
            onStopAiming.Invoke();

        bool wasAiming = isAiming;
        isAiming = false;

        if (currentWeapon != -1)
        {
            animator.SetBool("IsAiming", mustAim);
            isAiming = mustAim && weapons[currentWeapon] is Weapon_FireWeapon;
        }

        AnimateArmRigsWeight();
        AnimateAimRigsWeight();
    }

    void OnMeleeAttackEvent()
    {
        weapons[currentWeapon].PerformAttack();
    }

    void SelectWeapon(int weaponToSet)
    {
        //Desactivamos arma
        if (currentWeapon != -1)
        {
            weapons[currentWeapon].Deselect(animator);
            if (weapons[currentWeapon] is Weapon_FireWeapon)
                animator.SetBool("IsAiming", false);
        }

        //Asignamos
        currentWeapon = weaponToSet;
        //Activamos arma...
        if (currentWeapon != -1)
        {
            weapons[currentWeapon].Select(animator);
            animator.SetBool("IsHoldingFireWeapon", weapons[currentWeapon] is Weapon_FireWeapon);
        }
        else //... o volvemos al animator original
        {
            animator.runtimeAnimatorController = originalAnimatorController;
        }

        AnimateArmRigsWeight();
        AnimateAimRigsWeight();
    }

    private void AnimateArmRigsWeight()
    {
        //DOTween.To(
        //       () => armsRig.weight,
        //       (x) => armsRig.weight = x,
        //       (currentWeapon != -1) && (weapons[currentWeapon] is Weapon_FireWeapon) && isAiming? 1f : 0f,
        //       0.25f
        //       );
    }

    void AnimateAimRigsWeight()
    {
        DOTween.To(
               () => aimRig.weight,
               (x) => aimRig.weight = x,
               (currentWeapon != -1) && (weapons[currentWeapon] is Weapon_FireWeapon) && isAiming ? 1f : 0f,
               0.25f
               );
    }

    internal WeaponBase GetCurrentWeapon()
    {
        return currentWeapon != -1 ? weapons[currentWeapon] : null;
    }

    public IEnumerator Reload(float timeForReloading, int weapon, BarrelByInstantiation thisGameObject)  // 1 -> SMG     2 -> Shotgun     3 -> GrenadeLauncher
    {
        //Debug.Log("Entro en Reload()");

        thisGameObject.isReloading = true;


        if (weapon == 1 && subMachineGunTotalAmmo > 0f && subMachineGunAmmo < subMachineGunCharger)
        {
            subMGReloadingText.gameObject.SetActive(true);
            yield return new WaitForSeconds(timeForReloading);

            float subMachineGunTotalAmmoSave = subMachineGunTotalAmmo;

            subMachineGunTotalAmmo -= (subMachineGunCharger - subMachineGunAmmo);

            if (subMachineGunTotalAmmo < 0f)
                subMachineGunTotalAmmo = 0f;

            if (subMachineGunTotalAmmoSave >= subMachineGunCharger)
                subMachineGunAmmo = subMachineGunCharger;
            else if (subMachineGunTotalAmmoSave < subMachineGunCharger && subMachineGunTotalAmmoSave > 0f)
                if (subMachineGunAmmo + subMachineGunTotalAmmoSave <= 30)
                    subMachineGunAmmo += subMachineGunTotalAmmoSave;
                else
                    subMachineGunAmmo = subMachineGunCharger;

            subMachineGunHasAmmo = true;

            subMGReloadingText.gameObject.SetActive(false);
        }
        else if (weapon == 2 && shotgunTotalAmmo > 0f && shotgunAmmo < shotgunCharger)
        {
            shotGReloadingText.gameObject.SetActive(true);
            yield return new WaitForSeconds(timeForReloading);

            float shotgunTotalAmmoSave = shotgunTotalAmmo;

            shotgunTotalAmmo -= (shotgunCharger - shotgunAmmo);

            if (shotgunTotalAmmo < 0f)
                shotgunTotalAmmo = 0f;

            if (shotgunTotalAmmoSave >= shotgunCharger)
                shotgunAmmo = shotgunCharger;
            else if (shotgunTotalAmmoSave < shotgunCharger && shotgunTotalAmmoSave > 0f)
                if (shotgunAmmo + shotgunTotalAmmoSave <= 30)
                    shotgunAmmo += shotgunTotalAmmoSave;
                else
                    shotgunAmmo = shotgunCharger;

            shotgunHasAmmo = true;

            shotGReloadingText.gameObject.SetActive(false);
        }
        else if (weapon == 3 && grenadeLauncherTotalAmmo > 0f && grenadeLauncherAmmo < grenadeLauncherCharger)
        {
            gLaunchReloadingText.gameObject.SetActive(true);
            yield return new WaitForSeconds(timeForReloading);

            float grenadeLauncherTotalAmmoSave = grenadeLauncherTotalAmmo;

            grenadeLauncherTotalAmmo -= (grenadeLauncherCharger - grenadeLauncherAmmo);

            if (grenadeLauncherTotalAmmo < 0f)
                grenadeLauncherTotalAmmo = 0f;

            if (grenadeLauncherTotalAmmoSave >= grenadeLauncherCharger)
                grenadeLauncherAmmo = grenadeLauncherCharger;
            else if (grenadeLauncherTotalAmmoSave < grenadeLauncherCharger && grenadeLauncherTotalAmmoSave > 0f)
                if (grenadeLauncherAmmo + grenadeLauncherTotalAmmoSave <= 30)
                    grenadeLauncherAmmo += grenadeLauncherTotalAmmoSave;
                else
                    grenadeLauncherAmmo = grenadeLauncherCharger;

            grenadeLauncherHasAmmo = true;

            gLaunchReloadingText.gameObject.SetActive(false);
        }

        thisGameObject.isReloading = false;
        //Debug.Log("Ya puedo disparar");
    }
}