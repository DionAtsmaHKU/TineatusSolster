using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class NotebookManager : MonoBehaviour
{
    [SerializeField] GameObject notebook;
    [SerializeField] GameObject timerUI;
    [SerializeField] List<GameObject> pages = new List<GameObject>();
    [SerializeField] DialogueRunner runner;
    private Timer timer;
    private int currentIndex = 0;
    private bool isActive = false;
    private bool firstOpen = true;
    private bool uiActive = true;

    private void Awake()
    {
        runner.onDialogueStart.AddListener(ToggleUI);
        runner.onDialogueComplete.AddListener(ToggleUI);
    }

    private void Start()
    {
        timer = FindAnyObjectByType<Timer>();
        Canvas canvas = GetComponent<Canvas>();
        canvas.enabled = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.N) && !timer.isPaused())
        {
            ToggleNotebook();
            return;
        }
            
        if (timer.isPaused() && isActive)
            ToggleNotebook();
    }

    public void ToggleNotebook()
    {
        if (firstOpen)
        {
            FlipHere(0);
            firstOpen = false;
        }

        isActive = !isActive;
        notebook.SetActive(isActive);
    }

    public void FlipNext(int pageChange)
    {
        if (currentIndex + pageChange < 0 || currentIndex + pageChange > 9)
            return;

        pages[currentIndex].SetActive(false);
        currentIndex += pageChange;
        pages[currentIndex].SetActive(true);
    }

    public void FlipHere(int page)
    {
        pages[currentIndex].SetActive(false);
        currentIndex = page;
        pages[currentIndex].SetActive(true);
    }

    private void ToggleUI()
    {
        uiActive = !uiActive;
        timerUI.SetActive(uiActive);
    }
}
