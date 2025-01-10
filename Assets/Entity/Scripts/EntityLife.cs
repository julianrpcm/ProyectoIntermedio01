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

    [Header("Drops on death")]
    private WeaponManager weaponManager;
    [SerializeField] private GameObject subMachineGunAmmoPrefab;
    [SerializeField] private GameObject desertEagleAmmoPrefab;
    [SerializeField] private GameObject shotgunAmmoPrefab;

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
        weaponManager = GetComponent<WeaponManager>();
        currentLife = startingLife;

        contador = 0f;
    }

    private void OnEnable()
    {
        hurtCollider.onHitWithDamage.AddListener(OnHitWithDamage);
    }

    private void Update()
    {
        if (gameObject.tag == "Player")
        {
            contador += Time.deltaTime;
            if (contador >= timeToStartRegenerating && currentLife <= startingLife)
            {
                currentLife += (healthRegenPerSecond * Time.deltaTime);
                RefreshLifeCanvas();
            }
        }
    }

    void OnHitWithDamage(float damage)
    {
        //Debug.Log("OnHitWithDamage()");

        if (gameObject.tag == "Player")
        {
            contador = 0f;
        }

        currentLife -= damage;
        onLifeChanged.Invoke(currentLife);
        if (currentLife <= 0f)
        {
            //Debug.Log("onDeath.Invoke()");
            onDeath.Invoke();
            if (gameObject.tag == "Player")
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

    public void DropAmmoOnDeath()
    {
        if (gameObject.tag == "Enemy")
        {
            if (weaponManager.startingWeaponIndex == 3)     // Lleva la Desert Eagle
                Instantiate(desertEagleAmmoPrefab, transform.position, Quaternion.identity);
            else if (weaponManager.startingWeaponIndex == 4)     // Lleva la SubMachineGun
                Instantiate(subMachineGunAmmoPrefab, transform.position, Quaternion.identity);
            else if (weaponManager.startingWeaponIndex == 5)     // Lleva la Escopeta
                Instantiate(shotgunAmmoPrefab, transform.position, Quaternion.identity);
        }
    }
}