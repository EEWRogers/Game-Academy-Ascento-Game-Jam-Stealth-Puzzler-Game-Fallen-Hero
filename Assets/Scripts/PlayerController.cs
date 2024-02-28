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
    Animator playerAnimator;
    PlayerInput playerInput;
    InputAction moveAction;
    InputAction interactAction;
    

    void Awake() 
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
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

        SetMovementAnimation(currentMovementVector);
        playerRigidbody.velocity = currentMovementVector;
    }

    void SetMovementAnimation(Vector2 currentMovementVector)
    {
        if (currentMovementVector.x == 0 && currentMovementVector.y == 0)
        {
            playerAnimator.SetBool("SRun", false);
            playerAnimator.SetBool("WRun", false);
        }

        if (currentMovementVector.y < 0)
        {
            playerAnimator.SetBool("SRun", true);
            playerAnimator.SetBool("WRun", false);
        }

        if (currentMovementVector.y > 0)
        {
            playerAnimator.SetBool("WRun", true);
            playerAnimator.SetBool("SRun", false);
        }
    }

    void Interact()
    {
        if (interactAction.triggered)
        {
            Debug.Log("Interacting!");
        }
    }
}
