using System;
using UnityEngine;

public class ClearCounter : BaseCounter, IKitchenObjectParent
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            // There is no object here, place it if player has one
            if (player.HasKitchenObject())
            {
                player.GetKitchenObject().SetKitchenObjectParent(this);
            }
            else
            {
                // Player has nothing, do nothing
            }
        }
        else
        {
            // There is a kitchen object here
            if (player.HasKitchenObject())
            {
                // Player is carrying something
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plate))
                {
                    // Player is holding a Plate
                    if (plate.TryAddIngredient(GetKitchenObject().GetScriptObject()))
                    {
                        GetKitchenObject().DestroySelf();
                    }
                }
                else
                {
                    // Player is not carrying a plate, but something else
                    if (GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                    {
                        // Counter has a plate
                        if (plateKitchenObject.TryAddIngredient(player.GetKitchenObject().GetScriptObject()))
                        {
                            player.GetKitchenObject().DestroySelf();
                        }
                    }
                }
            }
            else
            {
                // Player is not carrying anything
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }
}
