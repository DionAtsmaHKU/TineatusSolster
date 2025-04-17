using UnityEngine;
using Yarn.Unity;

public class DialogueTrigger : MonoBehaviour
{
    public DialogueRunner dialogueRunner;
    public string yarnNode = "FirstDialogueBoat"; // Change this to your desired node name

    void Update()
    {
        // Check if E is pressed and dialogue isn't already running
        if (Input.GetKeyDown(KeyCode.E) && !dialogueRunner.IsDialogueRunning)
        {
            dialogueRunner.StartDialogue(yarnNode);
        }
    }
}
