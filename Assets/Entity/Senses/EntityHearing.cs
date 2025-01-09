using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class EntityHearing : MonoBehaviour
{
    [SerializeField] float heardPerceptiblesLifeTime = 5f;
    [SerializeField] float trimsPerSecond = 5f;

    [Serializable]
    class HeardPreceptible
    {
        public IPerceptible perceptible;
        public float lastHeardTime;
    }

    [SerializeField] List<HeardPreceptible> heardPerceptibles = new();
    float lasTrimTime;

    private void Awake()
    {
        lasTrimTime = Time.time;
    }

    internal void NotifyHeardSoundEmitter(EntitySoundEmiter entitySoundEmiter)
    {

        IPerceptible perceptible = entitySoundEmiter.GetComponent<IPerceptible>();
        if (perceptible != null)
        {
            HeardPreceptible heardPreceptible = heardPerceptibles.Find((x) => x.perceptible == perceptible);

            if (heardPreceptible == null)
            {
                heardPreceptible = new();
                heardPreceptible.perceptible = perceptible;
                heardPerceptibles.Add(heardPreceptible);
            }

            heardPreceptible.lastHeardTime = Time.time;
        }
    }

    private void Update()
    {
        if ((Time.time - lasTrimTime) > (1f / trimsPerSecond))
        {
            heardPerceptibles.RemoveAll((x) => (Time.time - x.lastHeardTime) > heardPerceptiblesLifeTime);
            lasTrimTime = Time.time;
        }
    }

    internal IPerceptible GetClosestPerceptible()
    {
        //Codigo duplicado
        IPerceptible closestPerceptible = null;
        float closestDistance = -1f;
        foreach (HeardPreceptible hp in heardPerceptibles)
        {
            float distance = Vector3.Distance(transform.position, hp.perceptible.GetTransform().position);
            if ((closestDistance < 0f) || (distance < closestDistance))
            {
                closestDistance = distance;
                closestPerceptible = hp.perceptible;
            }
        }

        return closestPerceptible;
    }

}
