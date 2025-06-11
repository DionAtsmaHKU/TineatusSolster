using UnityEngine;
using FMOD.Studio;
using FMODUnity;

public class RadioQuest : MonoBehaviour
{
    [SerializeField] Knowledge knowledge;
    [SerializeField] Transform playerTransform;
    [SerializeField] Transform radioSource;
    [Range(0.01f, 0.05f)]
    [SerializeField] float multiplier;

    private bool questStarted = false;
    private string radioPath = "event:/character/sasha/radio_sasha";
    private EventInstance radioEv;
    private float islandSignal;

    private void Start()
    {
        radioEv = RuntimeManager.CreateInstance(radioPath);
    }

    private void Update()
    {
        CheckRadio();
        CheckDistance();
    }

    // Checks whether the radio should be playing or not
    private void CheckRadio()
    {
        if (knowledge.DoesHeKnow("RadioQuestStarted") && !questStarted)
        {
            questStarted = true;
            radioEv.start();
        }
        if (knowledge.DoesHeKnow("RadioQuestComplete") && questStarted)
        {
            radioEv.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        }
    }

    private void CheckDistance()
    {
        float distance = Vector3.Distance(radioSource.position, playerTransform.position);
        islandSignal = 1f - multiplier * distance;
        radioEv.setParameterByName("island_signal", islandSignal);
    }
}
