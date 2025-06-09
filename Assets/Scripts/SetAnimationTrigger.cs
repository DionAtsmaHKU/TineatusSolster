using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class SetAnimationTrigger : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] DialogueRunner runner;

    private void Awake()
    {
        runner.AddCommandHandler<string>("SetTrigger", SetTrig);
    }

    private void SetTrig(string trigger)
    {
        animator.SetTrigger(trigger);
    }
}
