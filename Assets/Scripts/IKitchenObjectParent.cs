using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IKitchenObjectParent
{
    //We are making this interface so that we can implement the methods for player as well as counters


    public Transform GetKitchenObjectFollowTransform();


    public void SetKitchenObject(KitchenObject kitchenObject);


    public KitchenObject GetKitchenObject();


    public void ClearKitchenObject();


    public bool HasKitchenObject();
    
}
