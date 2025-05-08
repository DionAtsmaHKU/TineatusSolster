using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
using System;

public class SpeechView : LineView
{
    public override void RunLine(LocalizedLine dialogueLine, Action onDialogueLineFinished)
    {
        if (dialogueLine.CharacterName != null)
            base.RunLine(dialogueLine, onDialogueLineFinished);
    }
}
