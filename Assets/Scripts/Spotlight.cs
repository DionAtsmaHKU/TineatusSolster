using System.Collections;
using System.Collections.Generic;
using System.Security.Permissions;
using UnityEngine;

public class Spotlight : MonoBehaviour
{
    [SerializeField] string lightName;
    public Light stageLight;
    public bool active = true;

    private SpotlightManager spotlightManager;

    private void OnEnable()
    {
        spotlightManager = FindObjectOfType<SpotlightManager>();

        if (!spotlightManager.spotlights.ContainsKey(lightName)) 
            spotlightManager.spotlights.Add(lightName, this);

        if (spotlightManager.inactiveSpotlights.Contains(RemoveLastLetter(lightName)))
            active = false;
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, spotlightManager.playerTransform.position) < 15f)
        {
            ActivateSpotlight();
        }
        else
        {
            DeactivateSpotlight();
        }
    }

    private void ActivateSpotlight()
    {
        if (!active || stageLight.enabled == true)
            return;

        stageLight.enabled = true;
    }

    private void DeactivateSpotlight()
    {
        if (active || stageLight.enabled == false)
            return;

        stageLight.enabled = false;
    }

    private string RemoveLastLetter(string name)
    {
        return name.Substring(0, name.Length - 1);
    }
}
