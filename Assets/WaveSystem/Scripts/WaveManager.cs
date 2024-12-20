using UnityEngine;
using UnityEngine.Events;

public class WaveManager : MonoBehaviour
{
    [SerializeField] Transform objectsToActiveOnStartParent;
    [SerializeField] Transform objectsToDesactiveOnFinishParent;
    [SerializeField] Transform objectsToActiveOnFinishParent;
    public UnityEvent onWavesFinisher;

    Wave[] waves;
    WaveStartTrigger waveStartTrigger;


    bool alreadyStarted;
    int currentWaveIndex = 0;


    private void Awake()
    {
        waves = GetComponentsInChildren<Wave>(true);
        waveStartTrigger = GetComponentInChildren<WaveStartTrigger>();
    }

    private void OnEnable()
    {
        foreach(Wave w in waves)
        {
            w.onWaveFinished.AddListener(OnWaveFinisher);
        }
        waveStartTrigger.onTriggered.AddListener(OnStartTriggerTriggered);
    }

    private void OnDisable()
    {
        foreach (Wave w in waves)
        {
            w.onWaveFinished.RemoveListener(OnWaveFinisher);
        }
        waveStartTrigger.onTriggered.RemoveListener(OnStartTriggerTriggered);
    }

    void OnStartTriggerTriggered()
    {
        if (!alreadyStarted && waveStartTrigger.CompareTag("Level2"))
        {
            Rigidbody rb = GetComponentInChildren<Rigidbody>();
            rb.isKinematic = false;
            alreadyStarted = true;
            //gameObject.GetComponentInChildren;
            waves[currentWaveIndex].gameObject.SetActive(true);
        }
        else if (!alreadyStarted)
        {
            alreadyStarted = true;
            objectsToActiveOnStartParent.gameObject.SetActive(true);
            waves[currentWaveIndex].gameObject.SetActive(true);
        }
    }

    void OnWaveFinisher()
    {
        currentWaveIndex++;
        if (currentWaveIndex < waves.Length)
        {
            waves[currentWaveIndex].gameObject.SetActive(true);
        }
        else
        {
            objectsToDesactiveOnFinishParent.gameObject.SetActive(false);
            objectsToActiveOnFinishParent.gameObject.SetActive(true);
            onWavesFinisher.Invoke();
        }
    }

}
