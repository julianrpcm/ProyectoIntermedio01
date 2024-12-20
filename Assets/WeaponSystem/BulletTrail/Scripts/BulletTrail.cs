using UnityEngine;
using DG.Tweening;

public class BulletTrail : MonoBehaviour
{
    [SerializeField] float duration = 0.25f;
    LineRenderer lineRenderer;

    const int numberOfPosition = 10;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();    
    }

    public void InitBullet(Vector3 startPosition, Vector3 endPosition)
    {
        Vector3[] positions = new Vector3[numberOfPosition];
        for(int i = 0; i < numberOfPosition; i++)
        {
            float t = (float)i / (float)numberOfPosition;
            positions[i] = Vector3.Lerp(startPosition, endPosition, t);
        }
        lineRenderer.SetPositions(positions);

        DOTween.To(
            () => lineRenderer.widthMultiplier,
            (x) => lineRenderer.widthMultiplier = x,
            0f,
            duration
            ).OnComplete(() => Destroy(gameObject));
        
    }
}
