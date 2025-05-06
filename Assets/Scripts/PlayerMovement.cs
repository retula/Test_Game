using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;

    public float groundDrag;

    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool readyToJump;

    [SerializeField] Animator animator;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;

    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;

    private void Start()
    {
        animator = GetComponent<Animator>();
        // pause = GetComponent<Canvas>();
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }


    private void Update()
    {
        // ground check
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight, whatIsGround);
        // * 0.5f * 0.2f

        MyInput();
        SpeedControl();
        QuitGame();

        // handle drag
        if (grounded)
            rb.linearDamping = groundDrag;
        else
            rb.linearDamping = 0;
    }


    private void FixedUpdate()
    {
        MovePlayer();
    }


    private void MyInput()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        // when jump
        if(Input.GetKey(jumpKey) && readyToJump && grounded)
        {
            readyToJump = false;

            Jump();

            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }
    

    private void QuitGame()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
    }

    private void MovePlayer()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        // on ground
        if (grounded)
        {
            Debug.Log("we are grounded");
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);

        }


        else if (!grounded)
        {
            Debug.Log("We have the high ground");
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
        }
    }


    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

        // limit velocity if needed
        if(flatVel.magnitude > moveSpeed)
        {
            Debug.Log("We're moving");         
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.linearVelocity = new Vector3(limitedVel.x, rb.linearVelocity.y, limitedVel.z);
            // animator.SetBool("isMoving", true);
        }

        if(moveDirection != Vector3.zero)
            animator.SetBool("isMoving", true);
        else animator.SetBool("isMoving", false);
    }


    private void Jump()
    {
        // reset y velocity
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetJump()
    {
        readyToJump = true;
    }
}
