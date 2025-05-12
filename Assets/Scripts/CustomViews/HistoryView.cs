using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Yarn.Unity;
using UnityEngine.UI;
using UnityEditor.VersionControl;
using System.Security.Policy;
using System.Runtime.CompilerServices;

public class HistoryView : DialogueViewBase
{
    DialogueRunner runner;
    private List<GameObject> oldMessages = new List<GameObject>();
    
    public TMPro.TextMeshProUGUI text;

    [Tooltip("This is the chat message bubble UI object (what we are cloning for each message!)... NOT the container group for all chat bubbles")]
    public GameObject dialogueBubblePrefab;
    public float lettersPerSecond = 10f;

    bool isFirstMessage = true;

    // current message bubble styling settings, modified by SetSender
    Color currentBGColor = Color.black, currentTextColor = Color.white;

    void Awake()
    {
        runner = GetComponent<DialogueRunner>();
    }

    void Start()
    {
        dialogueBubblePrefab.SetActive(false);
        UpdateMessageBoxSettings();
    }

    // when we clone a new message box, re-style the message box based on whether SetSenderMe or SetSenderThem was most recently called
    void UpdateMessageBoxSettings()
    {
        var bg = dialogueBubblePrefab.GetComponentInChildren<Image>();
        //bg.color = currentBGColor;
        var message = dialogueBubblePrefab.GetComponentInChildren<TMPro.TextMeshProUGUI>();
        message.text = "";
        message.color = currentTextColor;

        var layoutGroup = dialogueBubblePrefab.GetComponent<HorizontalLayoutGroup>();
        layoutGroup.padding.left = 0;
        layoutGroup.padding.right = 32;
        bg.transform.SetAsFirstSibling();
    }

    public void CloneMessageBoxToHistory()
    {
        // if this isn't the very first message, then clone current message box and move it up
        if (isFirstMessage == false)
        {
            var oldClone = Instantiate(
                dialogueBubblePrefab,
                dialogueBubblePrefab.transform.position,
                dialogueBubblePrefab.transform.rotation,
                dialogueBubblePrefab.transform.parent
            );
            oldMessages.Add(oldClone);
            dialogueBubblePrefab.transform.SetAsLastSibling();
        }
        isFirstMessage = false;

        // reset message box and configure based on current settings
        dialogueBubblePrefab.SetActive(true);
        UpdateMessageBoxSettings();
    }

    Coroutine currentTypewriterEffect;

    public override void RunLine(LocalizedLine dialogueLine, Action onDialogueLineFinished)
    {
        if (currentTypewriterEffect != null)
        {
            StopCoroutine(currentTypewriterEffect);
        }

        CloneMessageBoxToHistory();

        text.text = dialogueLine.Text.Text;

        currentTypewriterEffect = StartCoroutine(ShowTextAndNotify());
        IEnumerator ShowTextAndNotify()
        {
            yield return StartCoroutine(Effects.Typewriter(text, lettersPerSecond, null));
            currentTypewriterEffect = null;
            onDialogueLineFinished();
        }
    }

    public void ClearHistory()
    {
        CloneMessageBoxToHistory();
        foreach (GameObject o in oldMessages)
        {
            Destroy(o);
        }
        oldMessages.Clear();
    }
}