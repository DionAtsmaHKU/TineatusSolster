using UnityEngine;
using UnityEngine.Events;
using Yarn.Unity;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] string eventName;
    [SerializeField] Sprite characterPortrait;
    [SerializeField] DialogueRunner runner;
    [SerializeField] Portraits portraits;
    private bool player_present = false;

    private void Awake()
    {
        PlayerMovement.OnInteract += TriggerDialogue;
    }

    private void OnDestroy()
    {
        PlayerMovement.OnInteract -= TriggerDialogue;
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            player_present = true;
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            player_present = false;
        }
    }

    public void TriggerDialogue()
    {
        if (player_present == true)
        {
            portraits.SetOtherPortrait(characterPortrait);
            Debug.Log("Lets talk to / about:" + eventName);
            runner.StartDialogue(eventName);
        }
    }
}
