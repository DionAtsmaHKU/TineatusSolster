using System;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class SpotlightManager : MonoBehaviour
{
    [SerializeField] DialogueRunner dialogueRunner;
    public Transform playerTransform;
    public Dictionary<string, Spotlight> spotlights = new Dictionary<string, Spotlight>();
    public List<string> inactiveSpotlights = new List<string>();

    private void Awake()
    {
        VariableManager.onLoop += ResetSpotlights;
    }

    private void OnDestroy()
    {
        VariableManager.onLoop -= ResetSpotlights;
    }

    private void Start()
    {
        dialogueRunner.AddCommandHandler<string>("LightOff", DeactivateSpotlight);
        dialogueRunner.AddCommandHandler<string>("LightOn", ReactivateSpotlight);
    }

    private void DeactivateSpotlight(string key)
    {
        inactiveSpotlights.Add(key);

        foreach (string k in spotlights.Keys)
        {
            if (k.Contains(key))
            {
                spotlights[k].stageLight.enabled = false;
                spotlights[k].active = false;
            }
        }
    }

    private void ReactivateSpotlight(string key)
    {
        inactiveSpotlights.Remove(key);

        foreach (string k in spotlights.Keys)
        {
            if (k.Contains(key))
            {
                spotlights[k].active = true;
            }
        }
    }

    public void ResetSpotlights()
    {
        foreach (Spotlight spotlight in spotlights.Values)
        {
            spotlight.active = true;
        }
        inactiveSpotlights.Clear();
    }
}
