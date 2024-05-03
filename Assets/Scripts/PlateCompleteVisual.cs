using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateCompleteVisual : MonoBehaviour
{
    [Serializable]
    public struct KitchenObjectSO_GameObject
    {
        public KitchenObjectsSO kitchenObjectsSO;
        public GameObject gameObject;
    }
    
    
    [SerializeField] private PlateKitchenObject plateKitchenObject;
    [SerializeField] private List<KitchenObjectSO_GameObject> kitchenObjectSOGameObjectList; 

    private void Start()
    {
        plateKitchenObject.OnIngredientAdded += PlateKitchenObject_OnIngredientAdded; //Firing on the event at the start of the project
        foreach (KitchenObjectSO_GameObject kitchenObjectSoGameObject in kitchenObjectSOGameObjectList)
            //Checking the list of objects and setting every object as false so that we dont see them  
        {
            kitchenObjectSoGameObject.gameObject.SetActive(false);
        }
    }

    private void PlateKitchenObject_OnIngredientAdded(object sender, PlateKitchenObject.OnIngredientAddedEventArgs e)
        //Listening to the event and creating a function based on that
    {
        foreach (KitchenObjectSO_GameObject kitchenObjectSoGameObject in kitchenObjectSOGameObjectList)
        {
            if (kitchenObjectSoGameObject.kitchenObjectsSO == e.kitchenObjectsSO)
                //Going through the list and setting the element that we just received to true
            {
                kitchenObjectSoGameObject.gameObject.SetActive(true);
            }
        }
    }
}
