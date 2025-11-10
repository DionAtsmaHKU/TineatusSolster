using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
using System;
using UnityEngine.UI;
using TMPro;

public class SpeechView : LineView
{
    public DialogueAudio dialogueAudio;
    public HorizontalLayoutGroup layoutGroup;
	
	public GameObject DialogueBubble;        
    public GameObject SashaDialogueBubble;
	
	//Bubbles of the Majors
	public GameObject PsyDialogueBubble;
	public GameObject TechDialogueBubble;
	public GameObject ArtDialogueBubble;
	
    public TextMeshProUGUI DefaultText;
    public TextMeshProUGUI SashaText;
	
    public TextMeshProUGUI PsyText;
    public TextMeshProUGUI TechText;
    public TextMeshProUGUI ArtText;

    public override void RunLine(LocalizedLine dialogueLine, Action onDialogueLineFinished)
    {
        if (dialogueLine.CharacterName == null || dialogueLine.CharacterName == "Star")
            return;	        

		
        if (dialogueLine.CharacterName == "SASHA")
        {
            DialogueBubble.SetActive(false);
            SashaDialogueBubble.SetActive(true);
			PsyDialogueBubble.SetActive(false);
			TechDialogueBubble.SetActive(false);
			ArtDialogueBubble.SetActive(false);
			lineText = SashaText;
            layoutGroup = SashaDialogueBubble.GetComponent<HorizontalLayoutGroup>();
            layoutGroup.padding.left = 0;
            layoutGroup.padding.right = 80;
        }
        else if (dialogueLine.CharacterName == "PSY")
        {
            DialogueBubble.SetActive(false);
			SashaDialogueBubble.SetActive(false);
            PsyDialogueBubble.SetActive(true);
			TechDialogueBubble.SetActive(false);
			ArtDialogueBubble.SetActive(false);
			
			lineText = PsyText;
            layoutGroup = PsyDialogueBubble.GetComponent<HorizontalLayoutGroup>();
			
            layoutGroup.padding.right = 0;
            layoutGroup.padding.left = 0;
        }	
        else if (dialogueLine.CharacterName == "ART" )
        {
			DialogueBubble.SetActive(false);
			SashaDialogueBubble.SetActive(false);
			PsyDialogueBubble.SetActive(false);
			TechDialogueBubble.SetActive(false);
            ArtDialogueBubble.SetActive(true);
			
			lineText = ArtText;
            layoutGroup = ArtDialogueBubble.GetComponent<HorizontalLayoutGroup>();
			
            layoutGroup.padding.right = 0;
            layoutGroup.padding.left = 0;
        }
		else if (dialogueLine.CharacterName == "TECH")
        {
            DialogueBubble.SetActive(false);
			SashaDialogueBubble.SetActive(false);
			PsyDialogueBubble.SetActive(false);
            TechDialogueBubble.SetActive(true);
			ArtDialogueBubble.SetActive(false);
			
			lineText = TechText;
            layoutGroup = TechDialogueBubble.GetComponent<HorizontalLayoutGroup>();
			
            layoutGroup.padding.right = 0;
            layoutGroup.padding.left = 0;
        }		
        else
        {
            DialogueBubble.SetActive(true);
            SashaDialogueBubble.SetActive(false);
			PsyDialogueBubble.SetActive(false);
			TechDialogueBubble.SetActive(false);
			ArtDialogueBubble.SetActive(false);
			lineText = DefaultText;

            layoutGroup = DialogueBubble.GetComponent<HorizontalLayoutGroup>();
            layoutGroup.padding.left = 80;
            layoutGroup.padding.right = 0;
        }
        base.RunLine(dialogueLine, onDialogueLineFinished);
    }
}
