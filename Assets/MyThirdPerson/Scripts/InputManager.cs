using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    PlayerControls playerControls;
    Vector2 movementInput;
    Vector2 cameraInput;

    public static InputManager instance;

    public float HorizontalInput { get { return movementInput.x; } } 
    public float VerticalInput { get { return movementInput.y; } }
    public float MouseX { get { return cameraInput.x; } }
    public float MouseY { get { return cameraInput.y; } }


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else Destroy(gameObject);
    }

    private void OnEnable()
    {
        if (playerControls == null)
        {
            playerControls = new PlayerControls();
            playerControls.PlayerMovement.Movement.performed += 
                (i) => { movementInput = i.ReadValue<Vector2>(); };
            playerControls.PlayerMovement.Camera.performed +=
                (i) => { cameraInput = i.ReadValue<Vector2>(); };
        }
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }
}
