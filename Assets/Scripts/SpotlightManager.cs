using System;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class SpotlightManager : MonoBehaviour
{
    [SerializeField] DialogueRunner dialogueRunner;
    public Dictionary<string, Spotlight> spotlights = new Dictionary<string, Spotlight>();

    private void Start()
    {
        dialogueRunner.AddCommandHandler<string>("LightOff", DeactivateSpotlight);
        dialogueRunner.AddCommandHandler<string>("LightOn", ReactivateSpotlight);
    }

    private void DeactivateSpotlight(string key)
    {
        spotlights[key].active = false;
        spotlights[key].stageLight.enabled = false;
    }

    private void ReactivateSpotlight(string key)
    {
        spotlights[key].active = true;
    }
}