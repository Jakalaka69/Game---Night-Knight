using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{   
    
    private PlayerActions actions;
    [SerializeField]
    private AnyStateAnimator AnyStateAnimator;
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
    private Vector3 playerVelocity;
    private float gravityValue = -9.81F;
    [SerializeField]
    private float jumpHeight = 0.2F;
    private bool isRunning = false;

    [SerializeField]
    private float runSpeed = 0.5F;

    [SerializeField]
    private float walkSpeed = 0.25F;

    private bool dead = false;

    [SerializeField]
    private bool Grounded = true;
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
        Vector3 movement = transform.right * moveInput.x + transform.forward * moveInput.y;
        characterController.Move(moveSpeed * Time.deltaTime * movement);


        if (isRunning)
        {
            AnyStateAnimator.TryPlayAnimation("Run");
        }
        else if (moveInput.x != 0 || moveInput.y != 0)
        {
            AnyStateAnimator.TryPlayAnimation("Walk");
        }
        else
        {
            AnyStateAnimator.TryPlayAnimation("Stand");
        }


    }

    

    private void Run()
    {
        isRunning = !isRunning;
        if (isRunning)
        {
            moveSpeed = runSpeed;
        }
        else
        {
            moveSpeed = walkSpeed;
        }
    }

    private void Jump()
    {
        if (characterController.isGrounded)
        {

            AnyStateAnimator.TryPlayAnimation("Jump");
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3f * gravityValue);

        }
    }
    public void Die()
    {
        AnyStateAnimator.TryPlayAnimation("Die");
        dead = true;
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


        
        actions.Controls.Move.performed += cxt => moveInput = cxt.ReadValue<Vector2>();
        actions.Controls.MouseMovement.performed += cxt => horizontalMouseInput = cxt.ReadValue<float>();
        actions.Controls.Run.performed += cxt => Run();
        moveSpeed = walkSpeed;
        actions.Controls.Jump.performed += cxt => Jump();
    }

    void Start()
    {
        AnyStateAnimation stand = new
        AnyStateAnimation("Stand", "Jump", "Die");
        AnyStateAnimation walk = new
        AnyStateAnimation("Walk", "Jump", "Die");
        AnyStateAnimation run = new
        AnyStateAnimation("Run", "Jump", "Die");
        AnyStateAnimation jump = new
        AnyStateAnimation("Jump", "Die");
        AnyStateAnimation die = new AnyStateAnimation("Die");
        AnyStateAnimator.AddAnimation(stand, walk, run, jump, die);
    }
    void Update()
    {
        if (!dead)
        {
            Move();
            Rotate();
            Gravity();
        }
        Grounded = playerVelocity.y == 0;
}
}
