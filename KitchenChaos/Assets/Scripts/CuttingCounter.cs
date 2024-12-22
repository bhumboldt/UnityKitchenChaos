using UnityEngine;

public class CuttingCounter : BaseCounter
{
    [SerializeField] private CuttingRecipeSO[] cuttingRecipeSOArray;
    
    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            // There is no object here, place it if player has one
            if (player.HasKitchenObject())
            {
                // Player is carrying something we can drop
                if (HasRecipeWithInput(player.GetKitchenObject().GetScriptObject()))
                {
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                }   
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
            }
            else
            {
                // Player is not carrying anything
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }

    public override void InteractAlternate(Player player)
    {
        if (HasKitchenObject())
        {
            KitchenObjectSO output = GetOutputForInput(GetKitchenObject().GetScriptObject());

            if (output != null)
            {
                // Cut the object that is here
                GetKitchenObject().DestroySelf();

                KitchenObject.SpawnKitchenObject(output, this);
            }
        }
    }

    private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        return GetOutputForInput(inputKitchenObjectSO) != null;
    }

    private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach (var cuttingRecipeSo in cuttingRecipeSOArray)
        {
            if (cuttingRecipeSo.input == inputKitchenObjectSO)
            {
                return cuttingRecipeSo.output;
            }
        }

        return null;
    }
}
