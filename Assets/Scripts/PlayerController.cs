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
    [SerializeField] Dialogue currentSceneDialogue;
    Rigidbody2D playerRigidbody;

    //animation related components
    Animator playerAnimator;
    string currentAnimationState;
    bool playerHasHorizontalVelocity;

    // animation states
    const string PLAYER_IDLE_DOWN = "IdleDown";
    const string PLAYER_BLINK_DOWN = "BlinkDown";
    const string PLAYER_RUN_DOWN = "RunDown";
    const string PLAYER_IDLE_HORIZONTAL = "IdleHorizontal";
    const string PLAYER_BLINK_HORIZONTAL = "BlinkHorizontal";
    const string PLAYER_RUN_HORIZONTAL = "RunHorizontal";
    const string PLAYER_IDLE_UP = "IdleUp";
    const string PLAYER_RUN_UP = "RunUp";

    PlayerInput playerInput;
    InputAction moveAction;
    InputAction interactAction;

    bool canMove = true;
    
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
        if (canMove)
        {
            Move();
        }
        Interact();
        SetAnimation();

        Debug.Log(currentAnimationState);
    }

    void Move()
    {
        Vector2 inputVector = moveAction.ReadValue<Vector2>();
        Vector2 currentMovementVector = new Vector2(inputVector.x * movementSpeed, inputVector.y * movementSpeed);

        playerRigidbody.velocity = currentMovementVector;
    }

    void SetAnimation()
    {
        if (playerRigidbody.velocity == Vector2.zero)
        {
            switch (currentAnimationState)
            {
                case PLAYER_RUN_DOWN:
                ChangeAnimationState(PLAYER_IDLE_DOWN);
                break;

                case PLAYER_RUN_HORIZONTAL:
                ChangeAnimationState(PLAYER_IDLE_HORIZONTAL);
                break;

                case PLAYER_RUN_UP:
                ChangeAnimationState(PLAYER_IDLE_UP);
                break;
            }
        }
        
        if (playerRigidbody.velocity.y > 0)
        {
            ChangeAnimationState(PLAYER_RUN_UP);
        }

        if (playerRigidbody.velocity.y < 0)
        {
            ChangeAnimationState(PLAYER_RUN_DOWN);
        }

        if (playerRigidbody.velocity.x != 0 && playerRigidbody.velocity.y == 0)
        {
            ChangeAnimationState(PLAYER_RUN_HORIZONTAL);
            transform.localScale = new Vector3(Mathf.Sign(playerRigidbody.velocity.x), 1, 1);
        }
    }

    void ChangeAnimationState(string newState)
    {
        if (currentAnimationState == newState) { return; }

        playerAnimator.Play(newState);

        currentAnimationState = newState;
    }

    public void LockPlayerMovement()
    {
        canMove = false;
    }

    public void UnlockPlayerMovement()
    {
        canMove = true;
    }

    void Interact()
    {
        if (interactAction.triggered)
        {
            if (currentSceneDialogue.isActiveAndEnabled)
            {
                currentSceneDialogue.SkipLine();
            }
        }
    }

}