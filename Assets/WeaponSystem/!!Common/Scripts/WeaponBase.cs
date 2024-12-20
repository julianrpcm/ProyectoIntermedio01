using System;
using UnityEngine;

public abstract class WeaponBase : MonoBehaviour
{
    [SerializeField] float range = 2f;
    [SerializeField] int comboLength = 3;
    [SerializeField] AnimatorOverrideController overrideController;
    [SerializeField] bool activateFighting = false;


    internal virtual void Init()
    {
        gameObject.SetActive(false);
    }
    internal virtual void Select(Animator animator)
    {
        gameObject.SetActive(true);
        animator.runtimeAnimatorController = overrideController;
        animator.SetBool("IsFighting", activateFighting);
        animator.SetInteger("ComboLength", comboLength);
       
    }

    internal virtual void Deselect(Animator animator)
    {
        gameObject.SetActive(false);
        animator.runtimeAnimatorController = null;
        
    }
    internal abstract void PerformAttack();

    internal virtual float GetRange()
    {
        return range;
    }
}
