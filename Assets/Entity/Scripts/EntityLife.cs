using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class EntityLife : MonoBehaviour
{
    [SerializeField] public float startingLife = 1f;

    public UnityEvent <float> onLifeChanged;
    public UnityEvent onDeath;

    [SerializeField] private float currentLife;

    [Header("Health generation")]
    [SerializeField] private float timeToStartRegenerating = 10f;
    [SerializeField] private float healthRegenPerSecond = 1f;
    private float contador;

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

        contador = 0f;
    }

    private void OnEnable()
    {
        hurtCollider.onHitWithDamage.AddListener(OnHitWithDamage);
    }

    private void Update()
    {
        contador += Time.deltaTime;
        if (contador >= timeToStartRegenerating && currentLife <= startingLife)
        {
            currentLife += (healthRegenPerSecond * Time.deltaTime);
            RefreshLifeCanvas();
        }
    }

    void OnHitWithDamage(float damage)
    {
        contador = 0f;

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

    public void AddHealthByMedikit(float lifeAdded)
    {
        currentLife += lifeAdded;
        RefreshLifeCanvas();
    }

    private void RefreshLifeCanvas()
    {
        gameObject.GetComponentInChildren<LifeCanvas>().OnLifeChanged(currentLife);
        if (currentLife >= startingLife)
            currentLife = startingLife;
    }
}