using System.Collections;
using UnityEngine;

public class DestroyEnemyParent : MonoBehaviour
{
    public IEnumerator BeDestroyed()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}