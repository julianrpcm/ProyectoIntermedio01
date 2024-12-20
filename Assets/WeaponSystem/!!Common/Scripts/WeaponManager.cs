using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Animations.Rigging;
using DG.Tweening;
using UnityEngine.Events;


[DefaultExecutionOrder(-10)]
public class WeaponManager : MonoBehaviour
{
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

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        originalAnimatorController = animator.runtimeAnimatorController;
        weapons = weaponsParent.GetComponentsInChildren<WeaponBase>(true);
        foreach (WeaponBase wb in weapons)
        {
            wb.Init();
        }

    }

    private void Start()
    {
        SelectWeapon(startingWeaponIndex);
    }

    private void OnEnable()
    {

        foreach (AnimationEventForwarder aef in GetComponentsInChildren<AnimationEventForwarder>())
        {
            aef.onMeleeAttackEvent.AddListener(OnMeleeAttackEvent);
        }
    }


    private void Update()
    {
        if (mustAttack)
        {
            mustAttack = false;
            animator.SetTrigger("Attack");
        }
    }
    private void OnDisable()
    {
        foreach (AnimationEventForwarder aef in GetComponentsInChildren<AnimationEventForwarder>())
        {
            aef.onMeleeAttackEvent.RemoveListener(OnMeleeAttackEvent);
        }
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
            {
                weaponToSet = -1;
            }
        }
        else
        {
            weaponToSet--;
            if (weaponToSet < -1)
            {
                weaponToSet = weapons.Length - 1;
            }
        }

        if (weaponToSet != currentWeapon)
        {
            SelectWeapon(weaponToSet);
        }

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
            {
                ((Weapon_FireWeapon)weapons[currentWeapon]).StartShooting();
            }
            else
            {
                ((Weapon_FireWeapon)weapons[currentWeapon]).StopShooting();
            }
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
        bool wasAiming = isAiming;
        isAiming = false;

        if (currentWeapon != -1)
        {

            animator.SetBool("IsAiming", mustAim);
            isAiming = mustAim && weapons[currentWeapon] is Weapon_FireWeapon;
        }

        AnimateArmRigsWeight();
        AnimateAimRigsWeight();

        if (!wasAiming && isAiming)
        {
            onStartAiming.Invoke();
        }
        else if (wasAiming && !isAiming)
        {
            onStopAiming.Invoke();
        }


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
            {
                animator.SetBool("IsAiming", false);
            }
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

}
