using UnityEngine;

public abstract class BarrelBase : MonoBehaviour
{

    [Header("Debug")]
    [SerializeField] bool debugShoot;

    private void OnValidate()
    {
        if (debugShoot)
        {
            debugShoot = false;
            Shoot();
        }
    }
    public abstract void Shoot();

}
