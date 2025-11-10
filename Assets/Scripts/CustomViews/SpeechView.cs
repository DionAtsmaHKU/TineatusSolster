using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
using System;
using UnityEngine.UI;

public class SpeechView : LineView
{
    public DialogueAudio dialogueAudio;
    public HorizontalLayoutGroup layoutGroup;

    public override void RunLine(LocalizedLine dialogueLine, Action onDialogueLineFinished)
    {
        if (dialogueLine.CharacterName == null || dialogueLine.CharacterName == "Star" || dialogueLine.CharacterName == "Narrator")
            return;

        if (dialogueLine.CharacterName == "Sasha")
        {
            layoutGroup.padding.right = 80;
            layoutGroup.padding.left = 0;
        }
		
        if (dialogueLine.CharacterName == "Psy" || dialogueLine.CharacterName == "Art" || dialogueLine.CharacterName == "Tech")
        {
            layoutGroup.padding.right = 0;
            layoutGroup.padding.left = 0;
        }		
			
        else 
        {
            layoutGroup.reverseArrangement = false;
            layoutGroup.padding.right = 0;
            layoutGroup.padding.left = 80;
        }
        base.RunLine(dialogueLine, onDialogueLineFinished);
    }
}
