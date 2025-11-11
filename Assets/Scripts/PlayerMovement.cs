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
    private Vector3 startPos;

    public static event Action OnInteract;

    private void Awake()
    {
        VariableManager.onLoop += ResetPlayerPos;
        runner.onDialogueComplete.AddListener(EnterExitDialogue);
        runner.onDialogueStart.AddListener(EnterExitDialogue);
    }

    private void OnDestroy()
    {
        VariableManager.onLoop -= ResetPlayerPos;
    }

    void Start()
    {
        startPos = transform.position;
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

        if (idleTimer > 3000f && !isIdling)
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

		// Calculate movement direction
		Vector3 moveDir = new Vector3(moveZ, 0f, -moveX).normalized;


		// Update idle timer
		if (moveDir.magnitude < 0.01f && !inDialogue)
		{
			idleTimer += Time.deltaTime;
		}
		else
		{
			idleTimer = 0f;
		}

		// Move the player
		rb.velocity = moveDir * moveSpeed + new Vector3(0, rb.velocity.y, 0);

		// ✅ Rotate to face movement direction (if moving)
		if (moveDir != Vector3.zero)
		{
			Quaternion targetRotation = Quaternion.LookRotation(moveDir);
			transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
		}

		// Update animation
		animator.SetFloat("speed", moveDir.magnitude);
	}


    private void ResetPlayerPos()
    {
        transform.position = startPos;
    }

    public void EnterExitDialogue()
    {
        inDialogue = !inDialogue;
    }
}
