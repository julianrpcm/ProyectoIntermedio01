using System.Collections;
using UnityEngine;

public class EmptyPrefabScript : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine("DestroyMyself");
    }

    private IEnumerator DestroyMyself()
    {
        if (gameObject != null && gameObject.activeInHierarchy)
        {
            yield return new WaitForSeconds(3f);
            Destroy(gameObject);
        }
    }
}