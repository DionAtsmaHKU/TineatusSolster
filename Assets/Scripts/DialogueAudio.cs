using UnityEngine;
using FMODUnity;
using FMOD.Studio;
using Yarn.Unity;

public class DialogueAudio : MonoBehaviour
{
    [SerializeField] DialogueRunner dialogueRunner;
    [SerializeField] OptionsListView optionsListView;

    private string continuePath = "event:/ui/continue_text_box";
    private string newTextBoxPath = "event:/ui/new_text_box";
    EventInstance continueEv, newTextBoxEv;

    // Start is called before the first frame update
    void Start()
    {
        continueEv = RuntimeManager.CreateInstance(continuePath);
        newTextBoxEv = RuntimeManager.CreateInstance(newTextBoxPath);
    }

    public void PlayContinue()
    {
        //continueEv.start();
    }

    public void PlayNext()
    {
        //newTextBoxEv.start();
    }
}
