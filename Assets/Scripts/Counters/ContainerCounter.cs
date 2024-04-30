using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ContainerCounter : BaseCounter
{

    public event EventHandler OnPlayerGrabbedObject; //Firing an event to know when the player grabs an object

    [SerializeField] private KitchenObjectsSO kitchenObjectsSO;

    public override void Interact(Player player) //Defining the reference to the player
    {
        if (!player.HasKitchenObject())
        {
            //Player is not carrying anything
            KitchenObject.SpawnKitchenObject(kitchenObjectsSO, player);

            OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
        }
    }
}
