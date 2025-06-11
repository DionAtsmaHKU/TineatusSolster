using FMOD.Studio;
using FMODUnity;
using System;
using UnityEngine;

public enum Area
{
    RICH = 0,
    MIDDLE = 1,
    POOR = 2
}

public class AreaMusic : MonoBehaviour
{
    [SerializeField] Transform playerTransform;
    [SerializeField] Transform paradeTransform;
    [SerializeField] float stopSoundDistance;
    private string poorPath = "event:/music/poor_district";
    private string middlePath = "event:/music/train_station";
    private string richPath = "event:/music/nauka_square";
    private EventInstance poorEv, middleEv, richEv;
    private Area currentArea;
    private bool musicOff = false;

    private void Awake()
    {
        currentArea = Area.MIDDLE;
        poorEv = RuntimeManager.CreateInstance(poorPath);
        middleEv = RuntimeManager.CreateInstance(middlePath);
        richEv = RuntimeManager.CreateInstance(richPath);
        PlayTrain();
    }

    private void Update()
    {
        if (Vector3.Distance(playerTransform.position, paradeTransform.position) < stopSoundDistance && !musicOff)
        {
            StopAllMusic();
        }
        else if (Vector3.Distance(playerTransform.position, paradeTransform.position) > stopSoundDistance && musicOff)
        {
            StartCurrentMusic();
        }
    }

    public void PoorMiddle()
    {
        if (currentArea == Area.MIDDLE)
        {
            poorEv.start();
            middleEv.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            currentArea = Area.POOR;
        }
        else
        {
            middleEv.start();
            poorEv.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            currentArea = Area.MIDDLE;
        }
    }

    public void RichMiddle()
    {
        if (currentArea == Area.MIDDLE)
        {
            richEv.start();
            middleEv.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            currentArea = Area.RICH;
        }
        else
        {
            middleEv.start();
            richEv.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            currentArea = Area.MIDDLE;
        }

    }

    private void PlayTrain()
    {
        currentArea = Area.MIDDLE;
        middleEv.start();
    }

    private void StopAllMusic()
    {
        musicOff = true;
        richEv.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        middleEv.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        poorEv.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }

    private void StartCurrentMusic()
    {
        musicOff = false;
        if (currentArea == Area.POOR)
            poorEv.start();

        if (currentArea == Area.MIDDLE)
            middleEv.start();

        if (currentArea == Area.RICH)
            richEv.start();
    }
}
