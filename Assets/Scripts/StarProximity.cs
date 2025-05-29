using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class StarProximity : MonoBehaviour
{
    [SerializeField] Transform playerTransform;
    private StudioEventEmitter emitter;
    private EventInstance theaterMusic;
    private float starProximity;

    private void Start()
    {
        emitter = GetComponent<StudioEventEmitter>();
        theaterMusic = emitter.EventInstance;
    }

    private void Update()
    {
        float distance = Vector3.Distance(transform.position, playerTransform.position);
        starProximity = 1f - 0.01f * distance;
        theaterMusic.setParameterByName("star_proximity", starProximity);
    }
}
