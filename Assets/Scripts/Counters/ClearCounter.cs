using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter
{

    [SerializeField] private KitchenObjectsSO kitchenObjectsSO;


    public override void Interact(Player player) //Defining the reference to the player
    {
        if (!HasKitchenObject())
        {
            //There is no KitchenObject here
            if (player.HasKitchenObject())
            {
                //Player is holding something
                player.GetKitchenObject().SetKitchenObjectParent(this);
            }
            else
            {
                //Player is not carrying anything
            }
        }
        else
        {
            //There is a KitchenObject here
            if(player.HasKitchenObject())
            {
                //Player is carrying something
            }
            else
            {
                //player is not carrying anything
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }

}
