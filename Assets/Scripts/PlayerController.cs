using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEngine.Rendering.HighDefinition.CameraSettings;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform cuddleSpot;
    [SerializeField] private GameObject toolTip;
    [SerializeField] private GameObject door;
    public bool cuddling;
    private Animator animator;
    [SerializeField]
    private AnimationToRagdoll rag;
    public static float Balance;
    [SerializeField] private Text balanceDisplay;
    public PlayerActions actions;
    
    public float health;
    public static float maxHealth;
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

    
    public static float runSpeed;

    
    public static float walkSpeed;

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

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Lava"))
        {
            takeDamage(0.05f);
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
        healthBar.UpdateHealthBar(health, maxHealth);
        if(SceneManager.GetActiveScene().buildIndex == 1)
        {
            SoundEffectManager.Instance.PlaySoundFXClip(damageClip, transform, 1f);
        }
        
        if (health <= 0 && !dead)
        {
            dead = false;
            Die();
            if(SceneManager.GetActiveScene().buildIndex == 3)
            {
                GameManagerLevel2.gameOver();
            }
            else if (SceneManager.GetActiveScene().buildIndex == 1)
            {
                GameManagerScript.gameOver();
            }

        }
       
        
    }

    private void Cuddle()
    {
        isRunning = false;
        horizontalMouseInput = 0;
        moveInput.y = 0;
        moveInput.z = 0;
        moveInput.x = 0;
        cuddling = true;
        anyStateAnimator.TryPlayAnimation("Cuddle");
        actions.Disable();
        characterController.enabled = false;

        transform.rotation = Quaternion.Euler(0,0,0);
        transform.position += new Vector3(0, -0.3f, 0);
    }
    private void StopCuddle()
    {
        cuddling = false;
        anyStateAnimator.OnAnimationDone("Cuddle");
        actions.Enable();
        characterController.enabled = true;
        transform.position += new Vector3(0, 0.3f, 0);
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

        Balance = 0;
        maxHealth = 10;
        runSpeed = 3F;
        walkSpeed = 1F;
        
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
        cuddling = false;
        AnyStateAnimation stand = new
        AnyStateAnimation("Stand", "Jump", "Cuddle");
        AnyStateAnimation walk = new
        AnyStateAnimation("Walk", "Jump","Cuddle");
        AnyStateAnimation run = new
        AnyStateAnimation("Run", "Jump","Cuddle");
        AnyStateAnimation jump = new
        AnyStateAnimation("Jump","Cuddle");
        AnyStateAnimation cuddle = new
        AnyStateAnimation("Cuddle");
        AnyStateAnimation fight = new
        AnyStateAnimation("Fight");

        anyStateAnimator.AddAnimation(stand, walk, run, jump, cuddle,fight );

        
    }
    void Update()
    {
        
        balanceDisplay.text = Balance.ToString();

        if (!dead && !cuddling)
        {
            actions.Enable();
            characterController.enabled = true;
            Move();
            Rotate();
            Gravity();
        }
        else if( dead && !cuddling)
        {
            Die();
        }


        Grounded = playerVelocity.y == 0;
        if (Grounded)
        {
            anyStateAnimator.OnAnimationDone("Jump");
        }


        float dist = Vector3.Distance(cuddleSpot.position, transform.position);
        if (dist < 2)
        {
            toolTip.SetActive(true);
            if (!cuddling)
            {
                if (Input.GetKeyDown(KeyCode.F))
                {

                    Cuddle();
                }
            }
            else
            {
                if (Input.anyKeyDown)
                {
                    StopCuddle();
                }
            }

        }
        else
        {
            toolTip.SetActive(false);
        }
    }
}
