using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    private CharacterController controller;

    public static event Action OnInteract;

    void Start()
    {
        controller = GetComponent<CharacterController>(); // Get the CharacterController
    }

    void Update()
    {
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
}
