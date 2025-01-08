using UnityEngine;
using UnityEngine.UI;

public class LifeCanvas : MonoBehaviour
{
    [SerializeField] Image lifeBar;
    [SerializeField] EntityLife entityLife;

    [SerializeField] private bool isPlayer;

    private void OnEnable()
    {
        entityLife.onLifeChanged.AddListener(OnLifeChanged);
    }

    private void OnDisable()
    {
        entityLife.onLifeChanged.RemoveListener(OnLifeChanged);
    }

    public void OnLifeChanged(float newLifeValue)
    {
        if (isPlayer)
            lifeBar.fillAmount = newLifeValue / entityLife.startingLife;
    }
}