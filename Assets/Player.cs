using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{   
    
    private PlayerActions actions;
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private CharacterController characterController;
    [SerializeField]
    #region INPUT
    private Vector3 moveInput;
    private float horizontalMouseInput;
    #endregion
    [SerializeField]
    #region VALUE
    private float moveSpeed = 2.0F;
    private float rotationSpeed = 80.0F;
    #endregion
    private Vector2 playerVelocity;
    private float gravityValue = -9.81F;
    [SerializeField]
    private float jumpHeight = 0.2F;
    private void Gravity()
    {
        playerVelocity.y += gravityValue * Time.deltaTime;
        characterController.Move(playerVelocity * Time.deltaTime);
        if (characterController.isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = 0;
        }
    }
    private void Rotate()
    {
        if(!Mouse.current.rightButton.isPressed)
        {
            float mouseX = horizontalMouseInput * rotationSpeed * Time.deltaTime;
            transform.Rotate(Vector3.up * mouseX);
        }
    }

    private void Move()
    {
        if(animator.GetBool("Fight") == true)
        {
            moveInput.x = 0;
            moveInput.y = 0;
            
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Fight"))
            {
                animator.SetBool("Fight", false);
            }
        }
        if(animator.GetBool("Jump") == true)
        {
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Jump"))
            {
                animator.SetBool("Jump", false);
            }
        }
        if(moveInput.y > 0)
        {
            animator.SetBool("IsRunning", true);
            animator.SetBool("Stand", false);
        }
        else
        {
            animator.SetBool("IsRunning", false);
            animator.SetBool("Stand", true);
        }
        Vector3 movement = transform.right * moveInput.x + transform.forward * moveInput.y;
        characterController.Move(moveSpeed * Time.deltaTime * movement);

    }

    void Update()
    {
        Gravity();
        Move();
        Rotate();
    }
    private void Stand()
    {
        animator.SetBool("Stand", true);
        animator.SetBool("Fight", false);
    }
    private void Fight()
    {
        animator.SetBool("Stand", false);
        animator.SetBool("Fight", true);
    }
    private void Jump()
    {

        animator.SetBool("Jump", true);
        playerVelocity.y += Mathf.Sqrt(jumpHeight * -75f * gravityValue);
    }

    private void OnEnable()
    {
        actions.Enable();
    }
    private void OnDisable()
    {
        actions.Disable();
    }
    void Awake()
    {
        actions = new PlayerActions();

        actions.Controls.Stand.performed += cxt => Stand();
        actions.Controls.Fight.performed += cxt => Fight();
        actions.Controls.Jump.performed += cxt => Jump();
        actions.Controls.Move.performed += cxt => moveInput = cxt.ReadValue<Vector2>();
        actions.Controls.MouseMovement.performed += cxt => horizontalMouseInput = cxt.ReadValue<float>();
    }

}
