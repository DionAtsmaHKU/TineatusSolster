using UnityEngine;
using FMOD.Studio;
using FMODUnity;
using System.Collections.Generic;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public class RadioQuest : MonoBehaviour
{
    [SerializeField] Knowledge knowledge;
    [SerializeField] Transform playerTransform;
    [SerializeField] List<Transform> radioSources = new List<Transform>();
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
        float currentDistance = 100000;
        foreach(Transform t in radioSources)
        {
            float distance = Vector3.Distance(t.position, playerTransform.position);
            if (distance < currentDistance)
                currentDistance = distance;
        }

        islandSignal = 1f - multiplier * currentDistance;
        radioEv.setParameterByName("island_signal", islandSignal);
    }
}
