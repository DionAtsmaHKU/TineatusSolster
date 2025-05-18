using System;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class SpotlightManager : MonoBehaviour
{
    [SerializeField] CustomSpotlightDictionary spotlightDict;
    [SerializeField] DialogueRunner dialogueRunner;
    private Dictionary<string, Spotlight> spotlights = new Dictionary<string, Spotlight>();

    private void Start()
    {
        spotlights = spotlightDict.ToDictionary();
        dialogueRunner.AddCommandHandler<string>("LightOff", DeactivateSpotlight);
    }

    private void DeactivateSpotlight(string key)
    {
        spotlights[key].active = false;
        spotlights[key].stageLight.enabled = false;
    }
}

[Serializable]
public class CustomSpotlightDictionary
{
    [SerializeField] List<CustomDictionarySpotlight> items;

    public Dictionary<string, Spotlight> ToDictionary()
    {
        Dictionary<string, Spotlight> newDict = new Dictionary<string, Spotlight>();

        foreach (CustomDictionarySpotlight item in items)
        {
            newDict.Add(item.name, item.spotlight);
        }
        return newDict;
    }
}

[Serializable]
public class CustomDictionarySpotlight
{
    [SerializeField] public string name;
    [SerializeField] public Spotlight spotlight;
}