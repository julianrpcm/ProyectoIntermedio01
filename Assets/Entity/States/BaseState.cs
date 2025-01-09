using UnityEngine;

public class BaseState : MonoBehaviour
{

  protected Entity entity;


    protected virtual void Awake()
    {
        entity = GetComponent<Entity>();
    }

    protected virtual void Start()
    {

    }

    protected virtual void OnEnable()
    {
        
    }


    protected virtual void Update()
    {
        
    }

    protected virtual void OnDisable()
    {
        
    }

    protected void LookAtTarget()
    {
        Debug.DrawLine(transform.position + Vector3.up * 2f, entity.GetTarget().GetTransform().position + Vector3.up * 2f, Color.red, 0.3f);
        float angularDistance = Vector3.SignedAngle(transform.forward, entity.GetTarget().GetTransform().position - transform.position, Vector3.up);
        Quaternion rotationToApply = Quaternion.AngleAxis(angularDistance, Vector3.up);
        transform.rotation = rotationToApply * transform.rotation;
    }


}
