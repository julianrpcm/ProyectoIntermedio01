using UnityEngine;
using DG.Tweening;

public class HitColliderSelfDesactivation : MonoBehaviour
{
    [SerializeField] float duration = 0.25f;

    private void Awake()
    {
        gameObject.SetActive(false);
    }

    Tween selfDesactivation = null;
    private void OnEnable()
    {
        selfDesactivation = DOVirtual.DelayedCall(duration, () => gameObject.SetActive(false));
        
    }

    private void OnDisable()
    {
        if (selfDesactivation != null)
        {
            selfDesactivation.Kill();
            selfDesactivation = null;
        }
    }
}
