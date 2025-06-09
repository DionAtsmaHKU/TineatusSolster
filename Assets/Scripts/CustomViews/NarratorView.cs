using System;
using Yarn.Unity;

public class NarratorView : LineView
{
    public DialogueAudio dialogueAudio;

    public override void RunLine(LocalizedLine dialogueLine, Action onDialogueLineFinished)
    {
        if (dialogueLine.CharacterName == null)
        {
            base.RunLine(dialogueLine, onDialogueLineFinished);
        }
    }
}
