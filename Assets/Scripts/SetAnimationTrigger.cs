using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class SetAnimationTrigger : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] string characterName;
    [SerializeField] DialogueRunner runner;

    private void Awake()
    {
        runner.AddCommandHandler<string, string>("SetTrigger", SetTrig);
    }

    private void SetTrig(string character, string trigger)
    {
        if (character == characterName) 
            animator.SetTrigger(trigger);
    }
}
