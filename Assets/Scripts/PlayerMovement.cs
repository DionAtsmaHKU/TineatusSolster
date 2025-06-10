using System;
using UnityEngine;
using Yarn.Unity;
using FMOD.Studio;
using FMODUnity;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] DialogueRunner runner;
	Animator animator;
    public float moveSpeed = 5f;
    private CharacterController controller;
    private Rigidbody rb;
    private bool inDialogue = false;
    private float idleTimer = 0f;
    private string idlePath = "event:/ui/player_idling";
    private EventInstance idleEv;
    private bool isIdling;

    public static event Action OnInteract;

    private void Awake()
    {
        runner.onDialogueComplete.AddListener(EnterExitDialogue);
        runner.onDialogueStart.AddListener(EnterExitDialogue);
    }

    void Start()
    {
		animator = GetComponentInChildren<Animator>();
        idleEv = RuntimeManager.CreateInstance(idlePath);
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

        if (idleTimer > 10f && !isIdling)
        {
            isIdling = true;
            idleEv.start();
        }
        else if (idleTimer <= 10f && isIdling)
        {
            isIdling = false;
            idleEv.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        }
    }

    private void Move()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector2 move = new Vector2(moveZ, -moveX).normalized * moveSpeed;
        if (move.magnitude < 0.01 && !inDialogue)
        {
            idleTimer += Time.deltaTime;
        } else { idleTimer = 0f; }

        rb.velocity = new Vector3(move.x, rb.velocity.y, move.y);
        // controller.Move(move * Time.deltaTime); // Move while respecting collisions
		animator.SetFloat("speed", move.magnitude);
    }

    public void EnterExitDialogue()
    {
        inDialogue = !inDialogue;
    }
}
