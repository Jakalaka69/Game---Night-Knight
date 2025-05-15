using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;
using UnityEngine.UI;
using static UnityEngine.Rendering.HighDefinition.CameraSettings;

public class PlayerControllerLevel2 : MonoBehaviour
{
    
    
    private Animator animator;
    [SerializeField]
    private AnimationToRagdoll rag;
    public static float Balance;
    
    public PlayerActions actions;
    public GameManagerLevel2 GM;
    public float health;
    
    [SerializeField] HealthBar healthBar;
    [SerializeField] AudioClip damageClip;

    public AnyStateAnimator anyStateAnimator;
    [SerializeField]
    private CharacterController characterController;
    [SerializeField]
    #region INPUT
    private Vector3 moveInput;
    private float horizontalMouseInput;
    private float verticalMouseInput;
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


    public static float runSpeed = 3F;
    public static float walkSpeed = 1F;

    private bool dead = false;

    [SerializeField]
    private bool Grounded = true;
    private bool RS = false;
    public bool isAttacking = false;
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
        isAttacking = true;
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

            anyStateAnimator.TryPlayAnimation("Jump");
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3f * gravityValue);

        }
    }

    public void SetDead(bool x)
    {
        dead = x;
    }



    public void takeDamage(float amount)
    {
        health -= amount;
        healthBar.UpdateHealthBar(health, PlayerController.maxHealth);
        SoundEffectManager.Instance.PlaySoundFXClip(damageClip, transform, 1f);
        if (health <= 0 && !dead)
        {
            dead = false;
            Die();
            GameManagerLevel2.gameOver();
        }


    }

    
    

    public void Die()
    {
        print("heres");
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
        actions.Controls.MouseMovementY.performed += cxt => verticalMouseInput = cxt.ReadValue<float>();
        actions.Controls.Run.performed += cxt => Run();
        moveSpeed = walkSpeed;
        actions.Controls.Jump.performed += cxt => Jump();
        actions.Controls.Fight.performed += cxt => Fight();


    }

    void Start()
    {
        
        AnyStateAnimation stand = new
        AnyStateAnimation("Stand", "Jump", "Cuddle");
        AnyStateAnimation walk = new
        AnyStateAnimation("Walk", "Jump", "Cuddle");
        AnyStateAnimation run = new
        AnyStateAnimation("Run", "Jump", "Cuddle");
        AnyStateAnimation jump = new
        AnyStateAnimation("Jump", "Cuddle");
        AnyStateAnimation cuddle = new
        AnyStateAnimation("Cuddle");
        AnyStateAnimation fight = new
        AnyStateAnimation("Fight");

        anyStateAnimator.AddAnimation(stand, walk, run, jump, cuddle, fight);


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
        else if (dead)
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
