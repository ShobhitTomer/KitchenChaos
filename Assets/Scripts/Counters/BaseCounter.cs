using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCounter : MonoBehaviour, IKitchenObjectParent
{
    //Defined this parent class so that every counter can easily inherit the properties from this class

    [SerializeField] private Transform counterTopPoint;


    private KitchenObject kitchenObject;


    public virtual void Interact(Player player) //Similar to a abstract method but not strict like it - Virtual
    {
        Debug.LogError("BaseCounter.Interact();");
    }

    public virtual void InteractAlternate(Player player) //Similar to a abstract method but not strict like it - Virtual
    {
        //Debug.LogError("BaseCounter.InteractAlternate();");
    }

    public Transform GetKitchenObjectFollowTransform() //Returning the reference to the counterTopPoint
    {
        return counterTopPoint;
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
