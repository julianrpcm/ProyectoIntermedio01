using System.Security.Claims;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(WeaponManager))]
public class WeaponManagerController : MonoBehaviour
{
    [Header("Inputs Combat")]
    [SerializeField] InputActionReference attack;
    [SerializeField] InputActionReference nextPrevWeapon;

    [Header("Inputs Combat")]
    [SerializeField] InputActionReference shoot;
    [SerializeField] InputActionReference continuousShoot;
    [SerializeField] InputActionReference aim;

    WeaponManager weaponManager;

    private void Awake()
    {
        weaponManager = GetComponent<WeaponManager>();
    }


    private void OnEnable()
    {
        attack.action.Enable();
        nextPrevWeapon.action.Enable();
        shoot.action.Enable();
        continuousShoot.action.Enable();
        aim.action.Enable();

        attack.action.performed += OnAttack;

        nextPrevWeapon.action.performed += OnNextPrevWeapon;

        shoot.action.performed += OnShoot;
        continuousShoot.action.started += OnContinuousShoot;
        continuousShoot.action.canceled += OnContinuousShoot;
        aim.action.started += OnAim;
        aim.action.canceled += OnAim;

    }


    private void OnDisable()
    {
        attack.action.Disable();
        nextPrevWeapon.action.Disable();
        shoot.action.Disable();
        continuousShoot.action.Disable();
        aim.action.Disable();

        attack.action.performed -= OnAttack;

        nextPrevWeapon.action.performed -= OnNextPrevWeapon;

        shoot.action.performed -= OnShoot;
        continuousShoot.action.started -= OnContinuousShoot;
        continuousShoot.action.canceled -= OnContinuousShoot;
        aim.action.started -= OnAim;
        aim.action.canceled -= OnAim;

    }

    void OnNextPrevWeapon(InputAction.CallbackContext ctx)
    {
        Vector2 readValue = ctx.ReadValue<Vector2>();


        bool mustSelectNextWeapon = readValue.y > 0;
        weaponManager.PerformChangeToNextOrPreWeapon(mustSelectNextWeapon);
    }

    private void OnAttack(InputAction.CallbackContext ctx)
    {
        weaponManager.PerformAttack();
    }

    void OnShoot(InputAction.CallbackContext ctx)
    {
        weaponManager.PerformShoot();
    }

    void OnContinuousShoot(InputAction.CallbackContext ctx)
    {
        float value = ctx.ReadValue<float>();
        bool mustStarShooting = value > 0f;

        weaponManager.PerformStartOrStopShooting(mustStarShooting);
    }

    void OnAim(InputAction.CallbackContext ctx)
    {
        float value = ctx.ReadValue<float>();
        bool mustAim = value > 0f;
        weaponManager.PerformAim(mustAim);
    }
}
