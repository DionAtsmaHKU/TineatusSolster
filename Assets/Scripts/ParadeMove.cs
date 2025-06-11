using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParadeMove : MonoBehaviour
{
    [SerializeField] Knowledge knowledge;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (knowledge.DoesHeKnow("SecondLoop"))
        {
            animator.SetFloat("speed", 1);
        }
    }
}
