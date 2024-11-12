using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Transform cameraTransform;
    Rigidbody rb;
    Vector3 moveDir = Vector3.zero;
    Quaternion targetRotation = Quaternion.identity;
    [SerializeField] float movementSpeed;
    [SerializeField] float rotationSpeed;

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
    }

    private void HandleMovement()
    {
        moveDir = cameraTransform.forward * InputManager.instance.VerticalInput + 
            cameraTransform.right * InputManager.instance.HorizontalInput;
        moveDir.Normalize();
        moveDir.y = 0;
        rb.velocity = moveDir * movementSpeed;
    }

    private void HandleRotation()
    {
        targetRotation = Quaternion.LookRotation(moveDir);
        if (moveDir != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        }
    }
}
