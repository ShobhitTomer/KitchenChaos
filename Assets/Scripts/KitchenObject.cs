using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] private KitchenObjectsSO kitchenObjectsSO;


    private IKitchenObjectParent kitchenObjectParent;

    public KitchenObjectsSO GetKitchenObjectsSO()
    {
        return kitchenObjectsSO;
    }

    public void SetKitchenObjectParent(IKitchenObjectParent kitchenObjectParent) //We are calling this fuction to set a kitchen object to a counter
    {
        if(this.kitchenObjectParent != null) //If there is already a object then we clear it
        {
            this.kitchenObjectParent.ClearKitchenObject();
        }


        this.kitchenObjectParent = kitchenObjectParent; //set the old one to the new one

        if (kitchenObjectParent.HasKitchenObject()) //Checking for error, if there is already object on the counter and we are trying to set it then it throws an error
        {
            Debug.LogError("IKitchenObjectParent Already has a KitchenObject on it");
        }

        kitchenObjectParent.SetKitchenObject(this); //calling function to set the kitchen object to the current counter or player

        transform.parent = kitchenObjectParent.GetKitchenObjectFollowTransform(); //we are assigning the point where we want the object to be
        transform.localPosition = Vector3.zero; //The relative position will be zero
    }

    public IKitchenObjectParent GetKitchenObjectParent() //Getter method to get the clearCounter
    {
        return kitchenObjectParent;
    }

    public void DestroySelf(){
        kitchenObjectParent.ClearKitchenObject();

        Destroy(gameObject);
    }


    public static KitchenObject SpawnKitchenObject(KitchenObjectsSO kitchenObjectsSO, IKitchenObjectParent kitchenObjectParent){
        Transform kitchenObjectTransform = Instantiate(kitchenObjectsSO.prefab); //We will have a clone of the visul of our prefab
        KitchenObject kitchenObject = kitchenObjectTransform.GetComponent<KitchenObject>();
        
        kitchenObject.SetKitchenObjectParent(kitchenObjectParent); //calling the setclearcounter from the KitchenObjectClass passing the clone that we just had

        return kitchenObject;
    }

}
