using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class EntityLife : MonoBehaviour
{
    [SerializeField] public float startingLife = 1f;

    public UnityEvent <float> onLifeChanged;
    public UnityEvent onDeath;

    float currentLife;

    #region Debug
    //[SerializeField] float debugLifeToAdd = 0f;
    [SerializeField] float debugLifeToSubtract = 0f;
    [SerializeField] bool debugApplyLifeChange;

    private void OnValidate()
    {
        if (debugApplyLifeChange)
        {
            debugApplyLifeChange = false;
            OnHitWithDamage(debugLifeToSubtract);
        }
    }
    #endregion

    HurtCollider hurtCollider;
    private void Awake()
    {
        hurtCollider = GetComponent<HurtCollider>();
        currentLife = startingLife;
        
    }

    private void OnEnable()
    {
        hurtCollider.onHitWithDamage.AddListener(OnHitWithDamage);
    }

  
    void OnHitWithDamage(float damage)
    {
        currentLife -= damage;
        onLifeChanged.Invoke(currentLife);
        if(currentLife <= 0f)
        {
            onDeath.Invoke();
            if (CompareTag("Player"))
            {
                StartCoroutine(WaitAndLoadScene());
            }
        }
    }

    private void OnDisable()
    {
        hurtCollider.onHitWithDamage.RemoveListener(OnHitWithDamage);
    }

    private IEnumerator WaitAndLoadScene()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("GameOverScene");
    }

}
