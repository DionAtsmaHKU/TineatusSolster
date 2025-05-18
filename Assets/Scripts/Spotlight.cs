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

    private void Start()
    {
        spotlightManager = FindObjectOfType<SpotlightManager>();
        spotlightManager.spotlights.Add(lightName, this);
    }

    private void OnBecameVisible()
    {
        if (!active)
            return;

        stageLight.enabled = true;
    }

    private void OnBecameInvisible()
    {
        stageLight.enabled = false;
    }
}
