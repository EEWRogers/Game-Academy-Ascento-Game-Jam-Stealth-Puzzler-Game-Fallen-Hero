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
    bool playerHasHorizontalVelocity;

    bool playerIsInputtingMovement = false;
    bool isPushingBlock = false;

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

        SetMovementAnimation();
        playerRigidbody.velocity = currentMovementVector;
    }

    void SetMovementAnimation()
    {
        if (playerRigidbody.velocity.x == 0 && playerRigidbody.velocity.y == 0)
        {
            playerAnimator.SetBool("SRun", false);
            playerAnimator.SetBool("WRun", false);
            playerAnimator.SetBool("HorizontalRun", false);
        }

        if (playerRigidbody.velocity.y < 0)
        {
            playerAnimator.SetBool("SRun", true);
            playerAnimator.SetBool("WRun", false);
            playerAnimator.SetBool("HorizontalRun", false);
        }

        if (playerRigidbody.velocity.y > 0)
        {
            playerAnimator.SetBool("WRun", true);
            playerAnimator.SetBool("SRun", false);
            playerAnimator.SetBool("HorizontalRun", false);
        }

        if (Mathf.Abs(playerRigidbody.velocity.x) > Mathf.Epsilon)
        {
            playerAnimator.SetBool("HorizontalRun", true);

            FlipPlayerSprite();
        }
    }

    void FlipPlayerSprite()
    {
        playerHasHorizontalVelocity = Mathf.Abs(playerRigidbody.velocity.x) > Mathf.Epsilon;

        if (playerHasHorizontalVelocity)
        {
            gameObject.transform.localScale = new Vector2 (Mathf.Sign(playerRigidbody.velocity.x), 1f);
        }
    }

    void Interact()
    {
        if (interactAction.triggered)
        {
            Debug.Log("Interacting!");
        }
    }
    
    void OnCollisionEnter2D(Collision2D other) 
    {
        if (other.gameObject.GetComponentInParent<PushBox>() != null)
        {
            other.gameObject.GetComponentInParent<PushBox>().Push();
        }
    }

}