using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerUI;
    public float timer = 0;
    public float totalTimer = 0;
    private int timeInMinutes;
    private int hours = 9;
    public float timeMultiplier = 2f;

    // Update is called once per frame
    void Update()
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
}
