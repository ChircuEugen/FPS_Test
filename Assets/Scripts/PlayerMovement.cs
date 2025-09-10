using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Transform cam;
    private InputReader playerInput;
    private CharacterController controller;
    private Animator animator;

    private Vector3 playerVelocity;
    public float jumpHeight = 1f;
    private float gravityValue = -9.81f;

    private bool isGrounded;
    public float groundDistance = 0.1f;
    public LayerMask groundLayer;


    public float speed = 3f;


    void Start()
    {
        cam = Camera.main.GetComponent<Transform>();
        playerInput = GetComponent<InputReader>();
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        isGrounded = Physics.CheckSphere(transform.position, groundDistance, groundLayer);
        animator.SetBool("IsGrounded", isGrounded);

        if (isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        Vector3 movementInput = transform.right * playerInput.moveInput.ReadValue<Vector2>().x + transform.forward * playerInput.moveInput.ReadValue<Vector2>().y;
        movementInput.Normalize();

        if(movementInput != Vector3.zero)
        {
            animator.SetBool("IsMoving", true);

            Vector3 localDirection = transform.InverseTransformDirection(movementInput);
            localDirection.Normalize();

            animator.SetFloat("LocalDirectionX", localDirection.x);
            animator.SetFloat("LocalDirectionY", localDirection.z);
        }
        else
        {
            animator.SetBool("IsMoving", false);
        }

        controller.Move(movementInput * speed * Time.deltaTime);

        transform.eulerAngles = new Vector3(0, cam.eulerAngles.y, 0);

        if (playerInput.jumpAction.triggered && isGrounded)
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -2.0f * gravityValue);
            animator.SetTrigger("Jumped");
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }
}
