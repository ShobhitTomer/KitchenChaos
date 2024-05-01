using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObject : KitchenObject
{

    [SerializeField] private List<KitchenObjectsSO> validKitchenOjectsSOList;
    
    private List<KitchenObjectsSO> kitchenObjectsSOList;


    private void Awake()
    {
        kitchenObjectsSOList = new List<KitchenObjectsSO>();
    }

    public bool TryAddIngredient(KitchenObjectsSO kitchenObjectsSO)
    {
        if (!validKitchenOjectsSOList.Contains(kitchenObjectsSO))
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
            return true;
        }
    }
}
