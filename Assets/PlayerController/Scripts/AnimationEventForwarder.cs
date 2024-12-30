using UnityEngine;
using UnityEngine.Events;

public class AnimationEventForwarder : MonoBehaviour
{
    [HideInInspector] public UnityEvent <string> onAnimationAttackEvent;
    [HideInInspector] public UnityEvent onMeleeAttackEvent;
    public void OnAnimationAttack(string hitColliderName)
    {
        //Debug.Log($"OnAnimationAttack - {hitColliderName}");
        onAnimationAttackEvent.Invoke(hitColliderName);
    }

    public void MeleeWeaponAttack()
    {
        //Debug.Log($"MeleeWeaponAttack");
        onMeleeAttackEvent.Invoke();
    }
}
