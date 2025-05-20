using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Yarn.Unity;
using static UnityEngine.Rendering.DebugUI;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerUI;
    [SerializeField] Transform playerTransform;
    public float timeMultiplier = 2f;
    public List<TimedEvent> events = new List<TimedEvent>();
    
    private List<string> DontActivate = new List<string>();
    private float range = 20f;
    private float timer = 0;
    private float totalTimer = 0;
    private int timeInMinutes;
    private int hours = 9;

    private void Start()
    {
        foreach (TimedEvent ev in events)
        {
            ev.ToMinutes();
        }
        totalTimer = hours * 60;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTime();
        CheckTimedEvents();
    }

    void UpdateTime()
    {
        totalTimer += Time.deltaTime * timeMultiplier;
        timer += Time.deltaTime * timeMultiplier;

        if (timeInMinutes >= 60)
        {
            timer -= 60;
            hours++;
        }
        timeInMinutes = (int)timer;

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
            if (totalTimer > ev.timeInMinutes && !ev.triggered)
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
}

[Serializable]
public class TimedEvent
{
    public List<GameObject> objToDeactivate;
    public List<GameObject> objToActivate;
    public int hours;
    public int minutes;

    [HideInInspector]
    public float timeInMinutes;

    [HideInInspector]
    public bool triggered;

    public void ToMinutes()
    {
        timeInMinutes = hours * 60 + minutes;
    }
}
