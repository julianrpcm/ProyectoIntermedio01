using UnityEngine;
using DG.Tweening;

public class DOTweenExample : MonoBehaviour
{
    private void Start()
    {
        transform.DOMove(Vector3.up * 15, 10f);
    }
}
