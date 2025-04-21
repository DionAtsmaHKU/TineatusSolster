using UnityEngine;
using Yarn.Unity;
using TMPro;

public class PlayerInputHandler : MonoBehaviour
{
    public DialogueRunner dialogueRunner;
    public TMP_InputField inputField; // Assign this in the Unity Inspector
    private string playerPurpose;

    void Start()
    {
        dialogueRunner.AddCommandHandler("askPlayerForPurpose", AskPlayerForPurpose);
    }

    public void AskPlayerForPurpose()
    {
        inputField.gameObject.SetActive(true);
        inputField.text = ""; // Clear previous input
        inputField.onEndEdit.AddListener(OnPurposeEntered);
    }

    private void OnPurposeEntered(string input)
    {
        playerPurpose = input;
        dialogueRunner.VariableStorage.SetValue("$playerPurpose", input);

        inputField.onEndEdit.RemoveListener(OnPurposeEntered);
        inputField.gameObject.SetActive(false);
        dialogueRunner.StartDialogue("afterpurpose");

    }
}
