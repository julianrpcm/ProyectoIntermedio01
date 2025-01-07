using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [Header("Movements")]

    [SerializeField] float linearAcceleration = 50f;
   
    //[SerializeField][FormerlySerializedAs("speed")] float maxSpeed = 5f;
    [SerializeField] float maxWalkSpeed = 2f;
    [SerializeField] float maxRunSpeed = 5f;
    [SerializeField] float decelerationFactor = 10f;

    [Header("Jump")]
    [SerializeField] float jumpSpeed = 5f;

    public enum OrientationMode
    {
        MoevementDirection,
        CameraDirection,
        FaceToTarget
    };
    [Header("Orientation")]
    [SerializeField] OrientationMode orientationMode = OrientationMode.MoevementDirection;
    [SerializeField] float angularVelocity = 360f;
    [SerializeField] Transform target;

    [Header("Combat")]
    [SerializeField] Transform hitCollidersParent;


    [Header("Inputs Movements")]
    [SerializeField] InputActionReference move;
    [SerializeField] InputActionReference jump;
    [SerializeField] InputActionReference walk;

    Animator animator;

    CharacterController characterController;
    Camera mainCamera;

    private Transform originalTarget;
    private OrientationMode originalOrientationMode;

    [Header("UI")]
    [SerializeField] private GameObject pauseMenuUI;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        characterController = GetComponent<CharacterController>();
        mainCamera = Camera.main;

        originalTarget = target;
        originalOrientationMode = orientationMode;
    }

    private void OnEnable()
    {
        move.action.Enable();
        jump.action.Enable();
        walk.action.Enable();
        

        move.action.started += OnMove;
        move.action.performed += OnMove;
        move.action.canceled += OnMove;

        jump.action.performed += OnJump;

        walk.action.started += OnWalk;
        walk.action.canceled += OnWalk;

       

        foreach (AnimationEventForwarder aef in GetComponentsInChildren<AnimationEventForwarder>())
        {
            aef.onAnimationAttackEvent.AddListener(OnAnimatorEvent);
        }
    }

    void Start()
    {
        pauseMenuUI.SetActive(false);
    }

    Vector3 rawStickValue;
    void Update()
    {
        Vector3 compositeMovement = Vector3.zero;
        compositeMovement += UpdateMovementOnPlane();
        compositeMovement += UpdateVerticalMovement();

        characterController.Move(compositeMovement);

        UpdateOrientation();
        UpdateAnimation();

        if (Input.GetKeyDown(KeyCode.P))
            ActivateDeactivatePauseMenu();
    }

    Vector3 velocityOnPlane = Vector3.zero;
    private Vector3 UpdateMovementOnPlane()
    {
        // Deceleration
        //if (rawStickValue.magnitude <= 0.01f)
        {
            Vector3 decelerationOnPlane = -velocityOnPlane * decelerationFactor * Time.deltaTime;
            velocityOnPlane += decelerationOnPlane;
        }

        // Acceleration
        Vector3 acceleration = (mainCamera.transform.forward * rawStickValue.z) + (mainCamera.transform.right * rawStickValue.x);
        float accelerationLength = acceleration.magnitude;
        Vector3 projectedAcceleration = Vector3.ProjectOnPlane(acceleration, Vector3.up).normalized * accelerationLength;
        Vector3 deltaAccelerationOnPlane = projectedAcceleration * linearAcceleration * Time.deltaTime;

        // Account for max speed
        float maxSpeed = CalcMaxSpeed();

        float currentSpeed = velocityOnPlane.magnitude;
        float attainableVelocity = Mathf.Max(currentSpeed, maxSpeed);
        velocityOnPlane += deltaAccelerationOnPlane;
        velocityOnPlane = Vector3.ClampMagnitude(velocityOnPlane, attainableVelocity);

     
        return velocityOnPlane * Time.deltaTime;
    }

   



    const float gravity = -9.8f;
    float verticalVelocity = 0f;
    private Vector3 UpdateVerticalMovement()
    {

        if (characterController.isGrounded)
        { verticalVelocity = 0f; }

        if (mustJump)
        {
            mustJump = false;
            if (characterController.isGrounded)
            {
                verticalVelocity = jumpSpeed;
            }
        }


        verticalVelocity += gravity * Time.deltaTime;
        
        return Vector3.up * verticalVelocity * Time.deltaTime;

    }

    Vector3 lastMovementDirecction = Vector3.zero;
    private void UpdateOrientation()
    {
        Vector3 desireDirection = CalculateDesiredDirection(); ;
        // Codigo que gira para coincidir con esa direccion deseada
        RotateToDesiredDirection(desireDirection);

    }

    private Vector3 CalculateDesiredDirection()
    {
        Vector3 desireDirection = Vector3.zero;

        switch (orientationMode)
        {
            case OrientationMode.MoevementDirection:
                if (rawStickValue.magnitude < 0.01f)
                {
                    desireDirection = lastMovementDirecction;
                }
                else
                {
                    desireDirection = velocityOnPlane;
                    lastMovementDirecction = desireDirection;
                }
                break;
            case OrientationMode.CameraDirection:
                desireDirection = Vector3.ProjectOnPlane(mainCamera.transform.forward, Vector3.up);
                break;
            case OrientationMode.FaceToTarget:
                desireDirection = Vector3.ProjectOnPlane(target.position - transform.position, Vector3.up);
                break;
        }

        return desireDirection;
    }

    private void RotateToDesiredDirection(Vector3 desireDirection)
    {
        float angularDistance = Vector3.SignedAngle(transform.forward, desireDirection, Vector3.up);
        float angleToApply = angularVelocity * Time.deltaTime;
        angleToApply = Mathf.Min(angleToApply, Math.Abs(angularDistance));

        Quaternion rotationToApply = Quaternion.AngleAxis(angleToApply * Math.Sign(angularDistance), Vector3.up);
        transform.rotation = rotationToApply * transform.rotation;
    }
   

    private void UpdateAnimation()
    {
        Vector3 localVelocity = transform.InverseTransformDirection(velocityOnPlane);
    
        float maxSpeed = maxRunSpeed;
        animator.SetFloat("HorizontalVelocity", localVelocity.x / maxSpeed);
        animator.SetFloat("ForwardVelocity", localVelocity.z / maxSpeed);

        float jumpProgress = Mathf.InverseLerp(jumpSpeed, -jumpSpeed, verticalVelocity);

        animator.SetFloat("JumpProgress", characterController.isGrounded ? 1f : jumpProgress);
        animator.SetBool("IsGrounded", characterController.isGrounded);
    }

    private void OnDisable()
    {
        move.action.Disable();
        jump.action.Disable();
        walk.action.Disable();
        

        move.action.started -= OnMove;
        move.action.performed -= OnMove;
        move.action.canceled -= OnMove;

        jump.action.performed -= OnJump;

        walk.action.started -= OnWalk;
        walk.action.canceled -= OnWalk;

        

        foreach (AnimationEventForwarder aef in GetComponentsInChildren<AnimationEventForwarder>())
        {
            aef.onAnimationAttackEvent.RemoveListener(OnAnimatorEvent);
        }
    }

    #region Input Events
    private void OnMove(InputAction.CallbackContext ctx) 
    {
        Vector2 stickValue = ctx.ReadValue<Vector2>();

        rawStickValue = (Vector3.forward * stickValue.y) + (Vector3.right * stickValue.x);
             
    }

    bool mustJump = false;
    private void OnJump(InputAction.CallbackContext ctx)
    {

        mustJump = true;

    }
    bool isWalking = false;
    private void OnWalk(InputAction.CallbackContext ctx)
    {
        isWalking = ctx.ReadValueAsButton();
    }



    #endregion

    void OnAnimatorEvent(string hitColliderName)
    {
        hitCollidersParent.Find(hitColliderName)?.gameObject.SetActive(true);
    }

    private float CalcMaxSpeed()
    {
        return isWalking ? maxWalkSpeed : maxRunSpeed;
    }

    internal void SetExternalOrientationMode(OrientationMode orientationMode)
    {
        this.orientationMode = orientationMode;
    }

    internal void UnSetExternalOrientationMode()
    {
        orientationMode = originalOrientationMode;
    }

    internal void SetExternalTarget(Transform customTarget)
    {
        target = customTarget;
    }

    internal void UnSetExternalTarget()
    {
        target = originalTarget;
    }

    public void ActivateDeactivatePauseMenu()
    {
        if (!pauseMenuUI.activeInHierarchy)
        {
            pauseMenuUI.SetActive(true);
            Time.timeScale = 0.0f;
            //Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }
        else
        {
            pauseMenuUI.SetActive(false);
            Time.timeScale = 1.0f;
            //Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}