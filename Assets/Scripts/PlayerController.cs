using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Scripting.APIUpdating;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float movementSpeed = 10f;

    Rigidbody2D playerRigidbody;
    PlayerInput playerInput;
    InputAction moveAction;
    InputAction interactAction;

    void Awake() 
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions["Move"];
        interactAction = playerInput.actions["Interact"];
    }

    void Update() 
    {
        Move();
        Interact();
    }

    void Move()
    {
        Vector2 inputVector = moveAction.ReadValue<Vector2>();
        Vector2 currentMovementVector = new Vector2(inputVector.x * movementSpeed, inputVector.y * movementSpeed);

        playerRigidbody.velocity = currentMovementVector;
    }

    void Interact()
    {
        if (interactAction.triggered)
        {
            Debug.Log("Interacting!");
        }
    }
}
