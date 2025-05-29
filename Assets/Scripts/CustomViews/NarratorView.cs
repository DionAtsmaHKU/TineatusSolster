using System;
using Yarn.Unity;

public class NarratorView : LineView
{
    public DialogueAudio dialogueAudio;

    public override void RunLine(LocalizedLine dialogueLine, Action onDialogueLineFinished)
    {
        if (dialogueLine.CharacterName == null)
        {
            dialogueAudio.PlayContinue();
            base.RunLine(dialogueLine, onDialogueLineFinished);
        }
    }
}
