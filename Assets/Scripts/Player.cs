using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private Animator animator;
    [SerializeField]
    private AnimationToRagdoll rag;
    
    private PlayerActions actions;

    [SerializeField]
    private AnyStateAnimator anyStateAnimator;
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
    private bool RS = false;
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
        
         float mouseX = horizontalMouseInput * rotationSpeed * Time.deltaTime;
         transform.Rotate(Vector3.up * mouseX);
        
    }

    private void Move()
    {
        Vector3 movement = transform.right * moveInput.x + transform.forward * moveInput.y;
        characterController.Move(moveSpeed * Time.deltaTime * movement);

       


        if (isRunning)
        {
            anyStateAnimator.TryPlayAnimation("Run");
        }
        else if (moveInput.x != 0 || moveInput.y != 0)
        {
            anyStateAnimator.TryPlayAnimation("Walk");
        }
        else
        {
            anyStateAnimator.TryPlayAnimation("Stand");
        }


    }

    
    private void Fight()
    {
        anyStateAnimator.TryPlayAnimation("Fight");
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
            print("here");
            anyStateAnimator.TryPlayAnimation("Jump");
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3f * gravityValue);

        }
    }

    public void SetDead(bool x)
    {
        dead = x;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Enemy") { 
            Die();
            
            collision.gameObject.SetActive(false);
        }
        
    }
    public void Die()
    {
        isRunning = false;
        horizontalMouseInput = 0;
        moveInput.y = 0;
        moveInput.z = 0;
        moveInput.x = 0;
        actions.Disable();
        characterController.enabled = false;
        
        
        rag.dead = true;
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
        actions.Controls.Fight.performed += cxt => Fight();
    }

    void Start()
    {
        AnyStateAnimation stand = new
        AnyStateAnimation("Stand", "Jump");
        AnyStateAnimation walk = new
        AnyStateAnimation("Walk", "Jump");
        AnyStateAnimation run = new
        AnyStateAnimation("Run", "Jump");
        AnyStateAnimation jump = new
        AnyStateAnimation("Jump");
        AnyStateAnimation fight = new
        AnyStateAnimation("Fight");

        anyStateAnimator.AddAnimation(stand, walk, run, jump,fight );
    }
    void Update()
    {
        if (!dead)
        {
            actions.Enable();
            characterController.enabled = true;
            
            Move();
            Rotate();
            Gravity();
        }
        else
        {

            Die();
        }


        Grounded = playerVelocity.y == 0;
        if (Grounded)
        {
            anyStateAnimator.OnAnimationDone("Jump");
        }
    }
}
