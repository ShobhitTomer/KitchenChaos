using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Player : MonoBehaviour, IKitchenObjectParent
{
    private static Player instance; // we can also do this is one line just by using "public static Player Instance{get; set;}"
    public static Player Instance //We are using field property just for the singleton pattern, we can also use regular get and set function
    {
        get
        {
            return instance;
        }
        private set
        {
            instance = value;
        }
    }

    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;
    public class OnSelectedCounterChangedEventArgs : EventArgs
    {
        public BaseCounter selectedCounter; //Created an additional class to pass more info into the event
    }

    private float moveSpeed = 7f; 
    [SerializeField] private GameInput gameInput; //Variable to take in gameInput from the new input system
    [SerializeField] private LayerMask countersLayerMask; //Variable used to define the layer for Raycast
    [SerializeField] private Transform kitchenObjectHoldPoint;//Adding them to implement the interface methods

    private bool isWalking; //variable to understand the player is walking or not for animations
    private Vector3 lastInteractDir; //variable to get the last direction player was looking at
    private BaseCounter selectedCounter; //To get the counter which is selected or at which we are looking at
    private KitchenObject kitchenObject;//Adding them to implement the interface methods

    private void Awake()
    {
        if(Instance != null)
        {
            Debug.LogError("There is more one Player Instance");
        }
        Instance = this;
    } // we set the instance of the object to this class


    private void Start()
    {
        gameInput.OnInteractAction += GameInput_OnInteractAction;
        gameInput.OnInteractAlternateAction += GameInput_OnInteractAlternateAction;
    }


    private void Update()
    {
        HandleMovement();
        HandleInteractions();
    }


    private void GameInput_OnInteractAlternateAction(object sender, System.EventArgs e) //Calling an event to interact with the counter
    {
        if (selectedCounter != null)
        {
            selectedCounter.InteractAlternate(this); //If we have a selectedcounter then we interact with it
        }
    }

    private void GameInput_OnInteractAction(object sender, System.EventArgs e) //Calling an event to interact with the counter
    {
        if (selectedCounter != null)
        {
            selectedCounter.Interact(this); //If we have a selectedcounter then we interact with it
        }
    }

    private void HandleInteractions()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        if (moveDir != Vector3.zero)
        {
            lastInteractDir = moveDir;
        }

        float interactDistance = 2f;

        if(Physics.Raycast(transform.position, lastInteractDir, out RaycastHit raycastHit, interactDistance, countersLayerMask))
        {
            if(raycastHit.transform.TryGetComponent(out BaseCounter baseCounter))
            {
                //Has ClearCounter
                if(baseCounter != selectedCounter)
                {
                    SetSelectedCounter(baseCounter); //Firing the Event so that Counter Visual can listen to the event
                }
            }
            else
            {
                SetSelectedCounter(null); //Firing the Event so that Couter Visual can listen to the event
            }
        }
        else
        {
            SetSelectedCounter(null); //Firing the Event so that Counter Visual can listen to the event
        }
    }

    public bool IsWalking()
    {
        return isWalking;
    }

    private void HandleMovement()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        float moveDistance = moveSpeed * Time.deltaTime;
        float playerRadius = .6f;
        float playerHeight = 2f;
        bool canMove = !(Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDir, moveDistance));

        if (!canMove)
        {
            //cannot move towards moveDir

            //Attempt only X movement
            Vector3 moveDirX = new Vector3(moveDir.x, 0f, 0f).normalized;
            canMove = moveDir.x !=0 && !(Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirX, moveDistance));

            if (canMove)
            {
                //can move only on X
                moveDir = moveDirX;
            }
            else
            {
                //Cannot move only on X

                //Attempt only Z movement
                Vector3 moveDirZ = new Vector3(0f, 0f, moveDir.z).normalized;
                canMove = moveDir.z !=0 && !(Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirZ, moveDistance));

                if (canMove)
                {
                    //Can only move on the Z
                    moveDir = moveDirZ;
                }
                else
                {
                    //We cannot move in any direction
                }
            }
        }

        if (canMove)
        {
            transform.position += moveDir * moveDistance;
        }

        isWalking = moveDir != Vector3.zero;

        float rotateSpeed = 10f;
        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed);
    }

    private void SetSelectedCounter(BaseCounter selectedCounter) // Created a function to fire the events and avoid code duplication
    {
        this.selectedCounter = selectedCounter;

        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs
        {
            selectedCounter = selectedCounter
        });
    }

    public Transform GetKitchenObjectFollowTransform() //Returning the reference to the counterTopPoint
    {
        return kitchenObjectHoldPoint;
    }

    public void SetKitchenObject(KitchenObject kitchenObject) //Changing the kitchenobject to the one passed in the parameter
    {
        this.kitchenObject = kitchenObject;
    }

    public KitchenObject GetKitchenObject() //getting the kitchenObject
    {
        return kitchenObject;
    }

    public void ClearKitchenObject() //Setting kitchenObject to null
    {
        kitchenObject = null;
    }

    public bool HasKitchenObject() //Checking whether there is anything assigned to kitchenObject or not
    {
        return kitchenObject != null;
    }
}
