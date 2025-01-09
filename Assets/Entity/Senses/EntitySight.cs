using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class EntitySight : MonoBehaviour
{
    [SerializeField] float range = 50f;
    [SerializeField] float horizontalRange = 20f;
    [SerializeField] float verticalRange = 10f;

    [SerializeField] IPerceptible.Faction[] interestingFactions;

    [SerializeField] LayerMask perceptibleLayers = Physics.DefaultRaycastLayers;
    [SerializeField] LayerMask occludingLayers = Physics.DefaultRaycastLayers;

    public List<IPerceptible> interestingVisibles = new();

    [SerializeField] private bool isPlayer = false;

    Vector3 sighRangeHalved;
    private void Awake()
    {
        sighRangeHalved = new Vector3(horizontalRange / 2f, verticalRange / 2f, range / 2f);
    }

    void Update()
    {
        interestingVisibles.Clear();

        Collider[] colliders =  Physics.OverlapBox(transform.position + (transform.forward * range / 2f), sighRangeHalved, transform.rotation, perceptibleLayers);
        foreach(Collider c in colliders)
        {
            IPerceptible visible = c.GetComponent<IPerceptible>();

            //if(visible != null)
            //{
            //    Debug.Log(visible.GetFaction(), visible.GetTransform());
            //}

            if((visible != null) &&
                interestingFactions.Contains(visible.GetFaction())
                ) 
            {
                //Debug.Log("Checking for hit");
                bool hasHit = Physics.Linecast(transform.position, c.transform.position + visible.GetOffSetForLineOfSightCheck(), out RaycastHit hit, occludingLayers);

                //Debug.Log("hasHit " + hasHit);
                //Debug.Log("Collider " + hit.collider);
                if (!hasHit || (hit.collider == c)) 
                {

                    interestingVisibles.Add(visible);
                }
            }
            //Debug.Log(interestingVisibles.Count);
        }
    }

    internal IPerceptible GetClosestVisible()
    {
        IPerceptible closestVisible = null;
        float closestDistance = -1f;
        foreach(IPerceptible v in interestingVisibles)
        {
            float distance = Vector3.Distance(transform.position, v.GetTransform().position);
            if((closestDistance <0f) || (distance < closestDistance))
            {
                closestDistance = distance;
                closestVisible = v;
            }
        }

        if (isPlayer)
            Debug.Log("closestVisible: " + closestVisible);

        return closestVisible;
    }
}
