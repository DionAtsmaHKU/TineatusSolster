using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class StarProximity : MonoBehaviour
{
    [SerializeField] Transform playerTransform;
    [SerializeField] float multiplier;
    private StudioEventEmitter emitter;
    private EventInstance theaterMusic;
    private float starProximity;

    private void Start()
    {
        theaterMusic = emitter.EventInstance;
    }

    private void Update()
    {
        starProximity = Vector3.Distance(transform.position, playerTransform.position) / multiplier;
        theaterMusic.setParameterByName("star_proximity", starProximity);
    }
}
