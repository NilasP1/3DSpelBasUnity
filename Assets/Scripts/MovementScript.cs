using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class MovementScript : MonoBehaviour
{
    [Header("Movement Settings")]
    public float walkSpeed = 4f;
    public float runSpeed = 7f;
    public float crouchSpeed = 2f;
    public float jumpForce = 5f;

    private float currentSpeed;

    private Rigidbody rb;
    private Transform cameraTransform;

    [Header("Mouse Look")]
    public float mouseSensitivity = 200f;
    public float smoothTime = 0.1f;
    private float rotationX = 0f;
    private float rotationY = 0f;
    private float smoothX;
    private float smoothY;
    private float velocityX;
    private float velocityY;

    [Header("Crouch Settings")]
    public float crouchHeight = 0.5f;
    private float standingHeight;
    private bool isCrouching = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cameraTransform = Camera.main.transform;

        standingHeight = GetComponent<CapsuleCollider>().height;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        currentSpeed = walkSpeed;
    }

    void Update()
    {
        HandleMouseLook();
        HandleMovement();
        HandleJumping();
        HandleCrouch();
    }

    void HandleMouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        rotationY += mouseX;
        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -90f, 90f);

        smoothX = Mathf.SmoothDamp(smoothX, rotationX, ref velocityX, smoothTime);
        smoothY = Mathf.SmoothDamp(smoothY, rotationY, ref velocityY, smoothTime);

        transform.rotation = Quaternion.Euler(0f, smoothY, 0f);
        cameraTransform.localRotation = Quaternion.Euler(smoothX, 0f, 0f);
    }

    void HandleMovement()
    {
        // Get player input
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 moveDirection = transform.right * moveX + transform.forward * moveZ;
        Vector3 velocity = moveDirection.normalized * currentSpeed;

        // Apply the velocity to the Rigidbody
        rb.velocity = new Vector3(velocity.x, rb.velocity.y, velocity.z);

        // Handle running, crouching, and walking speeds
        if (Input.GetKey(KeyCode.LeftShift) && isGrounded() && !isCrouching)
        {
            currentSpeed = runSpeed; // Running speed when on the ground
        }
        else if (isCrouching)
        {
            currentSpeed = crouchSpeed; // Crouch speed
        }
        else if (isGrounded()) // Reset to walk speed when grounded
        {
            currentSpeed = walkSpeed;
        }
    }

    void HandleJumping()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded())
        {
            // Jump with force
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
        }
    }

    void HandleCrouch()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            isCrouching = true;
            GetComponent<CapsuleCollider>().height = crouchHeight;
            cameraTransform.localPosition = new Vector3(cameraTransform.localPosition.x, cameraTransform.localPosition.y - (standingHeight - crouchHeight) / 2, cameraTransform.localPosition.z);
        }
        else if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            if (!Physics.Raycast(transform.position, Vector3.up, standingHeight))
            {
                isCrouching = false;
                GetComponent<CapsuleCollider>().height = standingHeight;
                cameraTransform.localPosition = new Vector3(cameraTransform.localPosition.x, cameraTransform.localPosition.y + (standingHeight - crouchHeight) / 2, cameraTransform.localPosition.z);
            }
        }
    }

    bool isGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, 1.1f); // Check if grounded
    }
}
