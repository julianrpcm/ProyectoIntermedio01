using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
public class CombatZoneShape : MonoBehaviour
{
    [SerializeField] string playerTag = "Player";
    int triggersContainingThePlayer = 0;

    [SerializeField] public UnityEvent<CombatZoneShape> onPlayerEnter;
    [SerializeField] public UnityEvent<CombatZoneShape> onPlayerExit;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            triggersContainingThePlayer++;
            if (triggersContainingThePlayer == 1)
            {
                onPlayerEnter.Invoke(this);
            }
        }
    }

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.CompareTag(playerTag))
    //    {
    //        triggersContainingThePlayer--;
    //        if (triggersContainingThePlayer == 0)
    //        {
    //            onPlayerExit.Invoke(this);
    //        }
    //    }
    //}
}
