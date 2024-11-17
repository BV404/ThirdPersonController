using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Transform cameraTransform;
    Rigidbody rb;
    Vector3 moveDir = Vector3.zero;
    Quaternion targetRotation = Quaternion.identity;
    float movementSpeed;
    float initMovementSpeed = 0;
    bool isGrounded;

    [SerializeField] float walkingSpeed;
    [SerializeField] float runningSpeed;
    [SerializeField] float rotationSpeed;
    [SerializeField] float transitionSpeed;
    [SerializeField] float gravitySpeed;

    // Start is called before the first frame update
    void Start()
    {
        cameraTransform = Camera.main.transform;
        rb = GetComponent<Rigidbody>();
    }

    public void HandleAllMovement()
    {
        HandleMovement();
        HandleRotation();
        HandleGravity();
    }

    private void HandleMovement()
    {
        moveDir = cameraTransform.forward * InputManager.instance.VerticalInput + 
            cameraTransform.right * InputManager.instance.HorizontalInput;
        moveDir.Normalize();
        moveDir.y = 0;

        if (moveDir != Vector3.zero)
        {
            if (InputManager.instance.IsSprinting)
                movementSpeed = runningSpeed;
            else movementSpeed = walkingSpeed;
        }
        else movementSpeed = 0f;


        LerpToMovementSpeed();
        rb.velocity = moveDir * initMovementSpeed;
    }

    private void LerpToMovementSpeed()
    {
        if (initMovementSpeed > movementSpeed)
        {
            initMovementSpeed -= transitionSpeed * Time.deltaTime;
        }
        else
        {
            initMovementSpeed += transitionSpeed * Time.deltaTime;
        }
    }

    private void HandleRotation()
    {
        targetRotation = Quaternion.LookRotation(moveDir);
        if (moveDir != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        }
    }

    private void HandleGravity()
    {
        Vector3 currentVelocity = rb.velocity;
        if (!isGrounded)
        {
            currentVelocity.y += -9.8f * gravitySpeed * Time.deltaTime;
        }
        else
        {
            currentVelocity.y = 0;
        }
        rb.velocity = currentVelocity;
    }

    private void OnCollisionStay(Collision collision)
    {
        isGrounded = true;
    }
    private void OnCollisionExit(Collision collision)
    {
        isGrounded = false;
    }
}
