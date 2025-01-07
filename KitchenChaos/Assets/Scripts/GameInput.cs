using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance { get; private set; }
    
    public event EventHandler OnInteractAction;
    public event EventHandler OnInteractAlternateAction;
    public event EventHandler OnPauseAction;
    
    private PlayerInputActions playerInputActions;
    
    private void Awake()
    {
        Instance = this;
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
        
        // Only notify when action is triggered (unlike movement where we will constantly query)
        playerInputActions.Player.Interact.performed += InteractOnperformed;
        playerInputActions.Player.InteractAlternate.performed += InteractAlternateOnperformed;
        playerInputActions.Player.Pause.performed += PauseOnperformed;
    }

    private void OnDestroy()
    {
        playerInputActions.Player.Interact.performed -= InteractOnperformed;
        playerInputActions.Player.InteractAlternate.performed -= InteractAlternateOnperformed;
        playerInputActions.Player.Pause.performed -= PauseOnperformed;
        
        playerInputActions.Dispose();
    }

    private void PauseOnperformed(InputAction.CallbackContext obj)
    {
        OnPauseAction?.Invoke(this, EventArgs.Empty);
    }

    private void InteractAlternateOnperformed(InputAction.CallbackContext obj)
    {
        OnInteractAlternateAction?.Invoke(this, EventArgs.Empty);
    }

    private void InteractOnperformed(InputAction.CallbackContext obj)
    {
        OnInteractAction?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetMovementVectorNormalized()
    {
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();
        
        // Normalize so we have the same speed no matter what is pressed
        inputVector = inputVector.normalized;

        return inputVector;
    }
}
