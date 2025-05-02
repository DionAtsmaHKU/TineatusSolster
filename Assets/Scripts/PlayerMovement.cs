using System;
using UnityEngine;
using Yarn.Unity;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] DialogueRunner runner;
    public float moveSpeed = 5f;
    private CharacterController controller;
    private bool inDialogue = false;
    
    public static event Action OnInteract;

    private void Awake()
    {
        runner.onDialogueComplete.AddListener(EnterExitDialogue);
        runner.onDialogueStart.AddListener(EnterExitDialogue);
    }

    void Start()
    {
        controller = GetComponent<CharacterController>(); // Get the CharacterController
    }

    void Update()
    {
        if (inDialogue)
            return;

        Move();

        if (Input.GetKeyDown(KeyCode.E))
        {
            OnInteract.Invoke();
        }
    }

    private void Move()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 move = new Vector3(moveX, 0, moveZ).normalized * moveSpeed;

        controller.Move(move * Time.deltaTime); // Move while respecting collisions
    }

    public void EnterExitDialogue()
    {
        inDialogue = !inDialogue;
    }
}
