using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] float followTime = 1f;
    [SerializeField] float xRotationSpeed = 1f;
    [SerializeField] float yRotationSpeed = 1f;

    Vector3 followVelocity = Vector3.zero;
    Transform target;
    float xRotAngle = 0f;
    float yRotAngle = 0f;
    RaycastHit hit;
    Transform cameraTransform;
    Vector3 newCameraPosition;


    private void Awake()
    {
        target = FindObjectOfType<MyCharacterController>().transform;
    }

    private void Start()
    {
        cameraTransform = Camera.main.transform;
        newCameraPosition = cameraTransform.localPosition;
        xRotAngle = transform.rotation.eulerAngles.y;
        yRotAngle = transform.rotation.eulerAngles.x;
    }

    private void LateUpdate()
    {
        FollowTarget();
        HandleRotation();
    }

    void FollowTarget()
    {
        transform.position = Vector3.SmoothDamp
            (transform.position, target.position, ref followVelocity, followTime);
    }

    void HandleRotation()
    {
        xRotAngle += InputManager.instance.MouseX * xRotationSpeed * Time.deltaTime;
        yRotAngle -= InputManager.instance.MouseY * yRotationSpeed * Time.deltaTime;
        yRotAngle = Mathf.Clamp(yRotAngle, -15, 45);
        transform.rotation = Quaternion.Euler(yRotAngle, xRotAngle, 0f);
    }
}
