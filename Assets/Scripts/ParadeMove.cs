using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParadeMove : MonoBehaviour
{
    [SerializeField] Knowledge knowledge;
    private Animator animator;
    private bool standStill = true;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (standStill) 
            //

        if (knowledge.DoesHeKnow("SecondLoop"))
        {
            standStill = false;
        }
    }
}
