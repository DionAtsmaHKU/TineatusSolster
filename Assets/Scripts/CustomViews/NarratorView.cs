using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class NarratorView : LineView
{
    public override void RunLine(LocalizedLine dialogueLine, Action onDialogueLineFinished)
    {
        if (dialogueLine.CharacterName == null)
            base.RunLine(dialogueLine, onDialogueLineFinished);
    }
}
