using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour
{

    public event EventHandler OnInteractAction; //Created an event so that others can listen to it
    public event EventHandler OnInteractAlternateAction; 

    private PlayerInputActions playerInputActions; //To get inputs from the new input system
    private void Awake() //Calling the new input system when we are loading the files
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
        playerInputActions.Player.Interact.performed += Interact_performed;
        playerInputActions.Player.InteractAlternate.performed += InteractAlternate_performed;
    }

    private void InteractAlternate_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
            OnInteractAlternateAction?.Invoke(this, EventArgs.Empty);
    }

    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj) //Firing the event so that others know
    {
            OnInteractAction?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetMovementVectorNormalized() //Normalising the values recieved from the input
    {
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>(); //Reading the values and assigning to inputVector


        inputVector = inputVector.normalized;

        return inputVector;
    }
}
