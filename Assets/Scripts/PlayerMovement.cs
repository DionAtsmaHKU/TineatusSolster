using System;
using UnityEngine;
using Yarn.Unity;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] DialogueRunner runner;
    public float moveSpeed = 5f;
    private CharacterController controller;
    private Rigidbody rb;
    private bool inDialogue = false;
    
    public static event Action OnInteract;

    private void Awake()
    {
        runner.onDialogueComplete.AddListener(EnterExitDialogue);
        runner.onDialogueStart.AddListener(EnterExitDialogue);
    }

    void Start()
    {
        rb = GetComponentInChildren<Rigidbody>();
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

        Vector2 move = new Vector2(moveZ, -moveX).normalized * moveSpeed;
        rb.velocity = new Vector3(move.x, rb.velocity.y, move.y);
        // controller.Move(move * Time.deltaTime); // Move while respecting collisions
    }

    public void EnterExitDialogue()
    {
        inDialogue = !inDialogue;
    }
}
