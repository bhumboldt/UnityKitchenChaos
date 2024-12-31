using System;
using UnityEngine;

public class StoveCounter : BaseCounter, IHasProgress
{
    public event EventHandler<OnStateChangedEventArgs> OnStateChanged;
    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;

    public class OnStateChangedEventArgs : EventArgs
    {
        public State state;
    }
    
    public enum State
    {
        Idle,
        Frying,
        Fried,
        Burned,
    }
    
    [SerializeField] private FryingRecipeSO[] fryingRecipes;
    [SerializeField] private BurningRecipeSO[] burningRecipes;

    private State currentState;
    private float fryingTimer;
    private FryingRecipeSO currentFryingRecipe;
    private float burningTimer;
    private BurningRecipeSO burningRecipeSo;

    private void Start()
    {
        currentState = State.Idle;
    }

    private void Update()
    {
        if (HasKitchenObject())
        {
            switch (currentState)
            {
                case State.Idle:
                    break;
                case State.Frying:
                    fryingTimer += Time.deltaTime;
                    
                    SendOnProgressEvent(fryingTimer / currentFryingRecipe.fryingTimeMax);
                    
                    if (fryingTimer > currentFryingRecipe.fryingTimeMax)
                    {
                        GetKitchenObject().DestroySelf();

                        KitchenObject.SpawnKitchenObject(currentFryingRecipe.output, this);

                        burningRecipeSo = GetBurningRecipeSOWithInput(GetKitchenObject().GetScriptObject());
                        currentState = State.Fried;
                        burningTimer = 0f;
                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs() { state = State.Fried });
                    }
                    break;
                case State.Fried:
                    burningTimer += Time.deltaTime;
                    
                    SendOnProgressEvent(burningTimer / burningRecipeSo.burningTimeMax);
                    
                    if (burningTimer > burningRecipeSo.burningTimeMax)
                    {
                        GetKitchenObject().DestroySelf();

                        KitchenObject.SpawnKitchenObject(burningRecipeSo.output, this);

                        currentState = State.Burned;
                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs() { state = State.Burned });
                        SendOnProgressEvent(0f);
                    }
                    break;
                case State.Burned:
                    break;
            }
        }
    }

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
                    FryingRecipeSO fryingRecipeSO = GetFryingRecipeSOWithInput(GetKitchenObject().GetScriptObject());
                    currentFryingRecipe = fryingRecipeSO;
                    currentState = State.Frying;
                    fryingTimer = 0f;
                    OnStateChanged?.Invoke(this, new OnStateChangedEventArgs() { state = State.Frying });
                    SendOnProgressEvent(fryingTimer / currentFryingRecipe.fryingTimeMax);
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
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plate))
                {
                    // Player is holding a Plate
                    if (plate.TryAddIngredient(GetKitchenObject().GetScriptObject()))
                    {
                        GetKitchenObject().DestroySelf();
                        currentState = State.Idle;
                        SendOnProgressEvent(0f);
                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs() { state = State.Idle });
                    }
                }
            }
            else
            {
                // Player is not carrying anything
                GetKitchenObject().SetKitchenObjectParent(player);
                
                currentState = State.Idle;
                SendOnProgressEvent(0f);
                OnStateChanged?.Invoke(this, new OnStateChangedEventArgs() { state = State.Idle });
            }
        }
    }
    
    private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        return GetOutputForInput(inputKitchenObjectSO) != null;
    }

    private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO)
    {
        FryingRecipeSO fryingRecipeSo = GetFryingRecipeSOWithInput(inputKitchenObjectSO);
        if (fryingRecipeSo != null)
        {
            return fryingRecipeSo.output;
        }

        return null;
    }

    private FryingRecipeSO GetFryingRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach (var fryingRecipeSo in fryingRecipes)
        {
            if (fryingRecipeSo.input == inputKitchenObjectSO)
            {
                return fryingRecipeSo;
            }
        }

        return null;
    }
    
    private BurningRecipeSO GetBurningRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach (var burningRecipeSo in burningRecipes)
        {
            if (burningRecipeSo.input == inputKitchenObjectSO)
            {
                return burningRecipeSo;
            }
        }

        return null;
    }

    private void SendOnProgressEvent(float progress)
    {
        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs()
        {
            progressNormalized = progress
        });
    }
}
