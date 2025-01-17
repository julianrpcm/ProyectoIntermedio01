using UnityEngine;

public class Medikit : MonoBehaviour
{
    [SerializeField] private float lifeAddedWhenPicked = 5f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<EntityLife>().AddHealthByMedikit(lifeAddedWhenPicked);
            other.transform.GetChild(6).GetComponent<AudioSource>().Play();
            Destroy(gameObject);
        }
    }
}