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
                spotlights[k].active = false;
                spotlights[k].stageLight.enabled = false;
            }
        }
    }

    private void ReactivateSpotlight(string key)
    {
        spotlights[key].active = true;
    }
}