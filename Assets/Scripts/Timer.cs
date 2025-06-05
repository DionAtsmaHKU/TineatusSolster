using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using Yarn.Unity;
using static UnityEngine.Rendering.DebugUI;

public class Timer : MonoBehaviour
{
	[SerializeField] DialogueRunner dialogueRunner;
    [SerializeField] TextMeshProUGUI timerUI;
    [SerializeField] Transform playerTransform;
    public float timeMultiplier = 2f;
	public float timeToAdd = 0;
    public TimeStamp startTime;
    public TimeStamp endTime;
    public List<TimedEvent> events = new List<TimedEvent>();
    public List<GameObject> originalObjects = new List<GameObject>();
    
    private List<string> DontActivate = new List<string>();
    private float range = 20f;
    private float timer = 0;
    private float totalTimer = 0;
    private int timeInMinutes;
    private int hours;
	private bool paused = false;

    private void Awake()
    {
        VariableManager.onLoop += Reset;
    }

    private void OnDestroy()
    {
        VariableManager.onLoop -= Reset;
    }

    private void Start()
    {
        foreach (TimedEvent ev in events)
        {
            ev.time.ToMinutes();
        }
        endTime.ToMinutes();
        startTime.ToMinutes();
        SetTime();
        SetText();
		dialogueRunner.onDialogueStart.AddListener(Pause);
		dialogueRunner.onDialogueComplete.AddListener(UnPause);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTime();
        CheckTimedEvents();
    }

    void UpdateTime()
    {
		if (paused)
			return;
		
        totalTimer += Time.deltaTime * timeMultiplier;
        timer += Time.deltaTime * timeMultiplier;

        if (timeInMinutes >= 60)
        {
            timer -= 60;
            hours++;
        }
        timeInMinutes = (int)timer;

        SetText();

        if (totalTimer > endTime.timeInMinutes) 
        {
            LoopCutscene();
        }
    }

    void SetTime()
    {
        hours = startTime.hours;
        timer = startTime.minutes;
        totalTimer = startTime.hours * 60 + startTime.minutes;
        timeInMinutes = (int)timer;
    }

    void SetText()
    {
        if (timeInMinutes < 10)
        {
            timerUI.text = "Time: " + hours.ToString() + ":0" + timeInMinutes.ToString();
        }
        else
        {
            timerUI.text = "Time: " + hours.ToString() + ":" + timeInMinutes.ToString();
        }
    }

    void CheckTimedEvents()
    {
        foreach(TimedEvent ev in events)
        {
            if (totalTimer > ev.time.timeInMinutes && !ev.triggered)
            {
                ActivateEvent(ev);
            }
        }
    }

    void ActivateEvent(TimedEvent ev)
    {
        ev.triggered = true;
        foreach (GameObject obj in ev.objToDeactivate)
        {
            float distanceToPlayer = Vector3.Distance(obj.transform.position, playerTransform.position);
            if (distanceToPlayer > range)
            {
                obj.SetActive(false);
            } 
            else
            {
                DontActivate.Add(RemoveLastLetter(obj.name));
            }
        }
        foreach (GameObject obj in ev.objToActivate)
        {
            float distanceToPlayer = Vector3.Distance(obj.transform.position, playerTransform.position);
            if (distanceToPlayer > range && !DontActivate.Contains(RemoveLastLetter(obj.name)))
            {
                obj.SetActive(true);
            }
        }
    }

    private string RemoveLastLetter(string name)
    {
        return name.Substring(0, name.Length - 1);
    }
	
	public void Pause() 
	{
		paused = true;
	}

	public void UnPause() 
	{
		totalTimer += timeToAdd;
		timer += timeToAdd;
		paused = false;
	}

    public void Reset()
    {
        SetTime();
        SetText();

        foreach (TimedEvent e in events)
        {
            foreach (GameObject obj in e.objToActivate)
            {
                obj.SetActive(false);
            }
            foreach (GameObject obj in e.objToDeactivate)
            {
                obj.SetActive(false);
            }
            e.triggered = false;
        }

        DontActivate.Clear();
        foreach (GameObject obj in originalObjects)
        {
            obj.SetActive(true);
        }
    }

    public void LoopCutscene()
    {
        StartCoroutine(LoopCutsceneRoutine());
    }

    IEnumerator LoopCutsceneRoutine()
    {
        paused = true; // Pause timer
        // Fade to black here
        yield return new WaitForSeconds(2f);
        // Cutscene here 
        yield return new WaitForSeconds(5f);
        VariableManager.Instance.Loop(); // Reset PlayerPos, NPC's, Timer, Events
        dialogueRunner.StartDialogue("Start");
    }

    public bool isPaused()
    {
        if (paused)
            return true;

        return false;
    }
}

[Serializable]
public class TimeStamp
{
    public int hours;
    public int minutes;

    [HideInInspector]
    public float timeInMinutes;

    public void ToMinutes()
    {
        timeInMinutes = hours * 60 + minutes;
    }
}

[Serializable]
public class TimedEvent
{
    public List<GameObject> objToDeactivate;
    public List<GameObject> objToActivate;
    public TimeStamp time;

    [HideInInspector]
    public bool triggered;
}
