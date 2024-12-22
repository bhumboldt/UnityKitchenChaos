using System;
using UnityEngine;

public class CuttingCounter : BaseCounter
{
    public event EventHandler<OnProgressChangedEventArgs> OnProgressChanged;

    public class OnProgressChangedEventArgs : EventArgs
    {
        public float progressNormalized;
    }
    
    public event EventHandler OnCut;
    
    [SerializeField] private CuttingRecipeSO[] cuttingRecipeSOArray;

    private int cuttingProgress;
    
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
                    CuttingRecipeSO cuttingRecipeSO = GetCuttonRecipeSOWithInput(GetKitchenObject().GetScriptObject());
                    cuttingProgress = 0;
                    OnProgressChanged?.Invoke(this, new OnProgressChangedEventArgs()
                        { progressNormalized = (float)cuttingProgress / cuttingRecipeSO.cuttingProgressMax });
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
                cuttingProgress++;
                OnCut?.Invoke(this, EventArgs.Empty);

                CuttingRecipeSO recipe = GetCuttonRecipeSOWithInput(GetKitchenObject().GetScriptObject());
                
                OnProgressChanged?.Invoke(this, new OnProgressChangedEventArgs()
                {
                    progressNormalized = (float)cuttingProgress / recipe.cuttingProgressMax
                });

                if (cuttingProgress >= recipe.cuttingProgressMax)
                {
                    // Cut the object that is here
                    GetKitchenObject().DestroySelf();

                    KitchenObject.SpawnKitchenObject(output, this);
                }
            }
        }
    }

    private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        return GetOutputForInput(inputKitchenObjectSO) != null;
    }

    private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO)
    {
        CuttingRecipeSO cuttingRecipeSo = GetCuttonRecipeSOWithInput(inputKitchenObjectSO);
        if (cuttingRecipeSo != null)
        {
            return cuttingRecipeSo.output;
        }

        return null;
    }

    private CuttingRecipeSO GetCuttonRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach (var cuttingRecipeSo in cuttingRecipeSOArray)
        {
            if (cuttingRecipeSo.input == inputKitchenObjectSO)
            {
                return cuttingRecipeSo;
            }
        }

        return null;
    }
}
