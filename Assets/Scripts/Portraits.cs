using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity;

public class Portraits : MonoBehaviour
{
    [SerializeField] DialogueRunner dialogueRunner;
    [SerializeField] GameObject canvas;
    [SerializeField] Image sashaPortrait;
    [SerializeField] Image otherPortrait;
    [SerializeField] List<Sprite> sashaSprites = new List<Sprite>();
    private bool inDialogue;

    public void Awake()
    {
        dialogueRunner.AddCommandHandler<int>("SetSashaSprite", SetSashaPortrait);
        dialogueRunner.onDialogueStart.AddListener(TogglePortraits);
        dialogueRunner.onDialogueComplete.AddListener(TogglePortraits);
    }

    private void TogglePortraits()
    {
        if (inDialogue)
        {
            canvas.SetActive(false);
            inDialogue = false;
        }
        else
        {
            canvas.SetActive(true);
            inDialogue = true;
        }
    }

    public void SetSashaPortrait(int i)
    {
        sashaPortrait.color = Color.white;
        sashaPortrait.sprite = sashaSprites[i];
    }

    public void SetOtherPortrait(Sprite newSprite)
    {
        if (newSprite == null)
        {
            otherPortrait.color = Color.clear;
        }
        else
        {
            otherPortrait.color = Color.white;
        }


        otherPortrait.sprite = newSprite;
    }
}
