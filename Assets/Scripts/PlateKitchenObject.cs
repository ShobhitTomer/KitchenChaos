using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObject : KitchenObject
{

    public event EventHandler<OnIngredientAddedEventArgs> OnIngredientAdded; //Event so that we can listen which ingredient is added

    public class OnIngredientAddedEventArgs : EventArgs
    {
        public KitchenObjectsSO kitchenObjectsSO;
    }
    

    [SerializeField] private List<KitchenObjectsSO> validKitchenOjectsSOList; //Valid ingredient list assigned in Unity itself
    
    private List<KitchenObjectsSO> kitchenObjectsSOList; //List to hold the current elements that we have on the plate


    private void Awake()
    {
        kitchenObjectsSOList = new List<KitchenObjectsSO>();
    }

    public bool TryAddIngredient(KitchenObjectsSO kitchenObjectsSO) //Function to try to add ingredient to the list
    {
        if (!validKitchenOjectsSOList.Contains(kitchenObjectsSO)) //Checking if the ingredient is valid or not
        {
            //Not a valid ingredient
            return false;
        }
        if (kitchenObjectsSOList.Contains(kitchenObjectsSO))
        {
            //Already has this type
            return false;
        }
        else
        {
            kitchenObjectsSOList.Add(kitchenObjectsSO);
            
            OnIngredientAdded?.Invoke(this, new OnIngredientAddedEventArgs
                //Firing an event after successfully adding that ingredient
            {
                kitchenObjectsSO = kitchenObjectsSO
            });
            return true;
        }
    }
}
