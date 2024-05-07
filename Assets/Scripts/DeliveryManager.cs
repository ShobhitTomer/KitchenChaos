using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class DeliveryManager : MonoBehaviour
{
    
    public static DeliveryManager Instance { get; private set; }

    [SerializeField] private RecipeListSO recipeListSO;
    
    
    private List<RecipeSO> waitingRecipeSOList;

    private float spawnRecipeTimer;
    private float spawnRecipeTimerMax = 4f;
    private int waitingRecipesMax = 4;

    private void Awake()
    {
        Instance = this;
        waitingRecipeSOList = new List<RecipeSO>();
    }

    private void Update()
    {
        spawnRecipeTimer -= Time.deltaTime;
        if (spawnRecipeTimer <= 0f)
        {
            spawnRecipeTimer = spawnRecipeTimerMax;

            if (waitingRecipeSOList.Count < waitingRecipesMax)
            {
                RecipeSO waitingRecipeSO = recipeListSO.recipeSOList[Random.Range(0, recipeListSO.recipeSOList.Count)];
                Debug.Log(waitingRecipeSO.recipeName);
                waitingRecipeSOList.Add(waitingRecipeSO);
            }
        }
    }

    public void DeliverRecipe(PlateKitchenObject plateKitchenObject)
    {
        for (int i = 0; i < waitingRecipeSOList.Count; i++)
        {
            RecipeSO waitingRecipeSO = waitingRecipeSOList[i];

            if (waitingRecipeSO.kitchenObjectsSOList.Count == plateKitchenObject.GetKitchenObjectsSOList().Count)
                //Has the same number of ingredients
            {
                bool plateContentsMatchesRecipe = true;
                foreach (KitchenObjectsSO recipeKitchenObjectsSO in waitingRecipeSO.kitchenObjectsSOList)
                {
                    //Cycling through all the ingredients in the recipe
                    bool ingredientFound = false;
                    foreach (KitchenObjectsSO plateKitchenObjectsSO in plateKitchenObject.GetKitchenObjectsSOList())
                    {
                        //Cycling through all the ingredients in the plate
                        if (plateKitchenObjectsSO == recipeKitchenObjectsSO)
                        {
                            //Ingredient Matches!
                            ingredientFound = true;
                            break;
                        }
                    }

                    if (!ingredientFound)
                    {
                        //The recipe ingredient was not found on the plate
                        plateContentsMatchesRecipe = false;
                    }
                }

                if (plateContentsMatchesRecipe)
                {
                    //Player has delivered the correct recipe
                    Debug.Log("Player has delivered the correct recipe");
                    waitingRecipeSOList.RemoveAt(i);
                    return;
                }
            }
        }
        //No matches found!
        //Player did not deliver a correct recipe
        Debug.Log("Player did not deliver a correct recipe");
    }
}
