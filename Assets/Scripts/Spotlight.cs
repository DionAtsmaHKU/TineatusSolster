using System.Collections;
using System.Collections.Generic;
using System.Security.Permissions;
using UnityEngine;

public class Spotlight : MonoBehaviour
{
    public Light stageLight;
    public bool active = true;

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
