using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotebookManager : MonoBehaviour
{
    [SerializeField] GameObject notebook;
    [SerializeField] List<GameObject> pages = new List<GameObject>();
    private Timer timer;
    private int currentIndex = 0;
    private bool isActive = false;

    private void Start()
    {
        timer = FindAnyObjectByType<Timer>();
    }

    private void Update()
    {
        if (timer.isPaused())
            return;

        if (Input.GetKeyDown(KeyCode.N))
            ToggleNotebook();
    }

    public void ToggleNotebook()
    {
        isActive = !isActive;
        notebook.SetActive(isActive);
    }

    public void FlipToPage(int page)
    {
        pages[currentIndex].SetActive(false);
        currentIndex = page;
        pages[currentIndex].SetActive(true);
    }
}
