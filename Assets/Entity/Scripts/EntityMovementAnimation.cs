using UnityEngine;

public class EntityMovementAnimation : MonoBehaviour
{
    Animator animator;
    IMovingAnimatable animatable;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        animatable = GetComponent<IMovingAnimatable>();
    }


    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("HorizontalVelocity", animatable.GetNormalizedLocalHorizontalVelocity());
        animator.SetFloat("ForwardVelocity", animatable.GetNormalizedLocalForwardlVelocity());

        animator.SetFloat("JumpProgress", animatable.GetJumpProgress());
        animator.SetBool("IsGrounded", animatable.GetIsGorunded());
    }
}
