using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class DeliveryManager : MonoBehaviour
{
    public event EventHandler OnRecipeSpawned;
    public event EventHandler OnRecipeCompleted;

    public event EventHandler OnRecipeSuccess;
    public event EventHandler OnRecipeFailed;
    
    public static DeliveryManager Instance { get; private set; }
    
    [SerializeField] private RecipeListSO recipeListSO;
    [SerializeField] private float spawnRecipeTimerMax = 4f;
    
    private List<RecipeSO> waitingRecipes = new List<RecipeSO>();
    private float spawnRecipeTimer = 0f;
    private int waitingRecipessMax = 4;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        spawnRecipeTimer -= Time.deltaTime;
        if (spawnRecipeTimer <= 0f)
        {
            spawnRecipeTimer = spawnRecipeTimerMax;

            if (waitingRecipes.Count < waitingRecipessMax)
            {
                RecipeSO waitingRecipeSO = recipeListSO.recipeSOList[Random.Range(0, recipeListSO.recipeSOList.Count)];
                waitingRecipes.Add(waitingRecipeSO);
                
                OnRecipeSpawned?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public List<RecipeSO> GetWaitingRecipes()
    {
        return waitingRecipes;
    }

    public void DeliverRecipe(PlateKitchenObject plate)
    {
        for (int i = 0; i < waitingRecipes.Count; i++)
        {
            RecipeSO waitingRecipe = waitingRecipes[i];

            if (waitingRecipe.kitchenObjectSoList.Count == plate.GetKitchenObjects().Count)
            {
                bool ingredientFound = false;
                bool plateContentsMatchRecipe = true;
                // Same number of ingredients
                foreach (KitchenObjectSO kitchenObjectSo in waitingRecipe.kitchenObjectSoList)
                {
                    // Loop through all items in recipe
                    foreach (KitchenObjectSO plateKitchenObjectSo in plate.GetKitchenObjects())
                    {
                        if (plateKitchenObjectSo == kitchenObjectSo)
                        {
                            ingredientFound = true;
                            break;
                        }
                    }

                    if (!ingredientFound)
                    {
                        // This recipe ingredient was not found on plate
                        plateContentsMatchRecipe = false;
                    }
                }

                if (plateContentsMatchRecipe)
                {
                    // Player delivered correct recipe
                    waitingRecipes.RemoveAt(i);
                    
                    OnRecipeCompleted?.Invoke(this, EventArgs.Empty);
                    OnRecipeSuccess?.Invoke(this, EventArgs.Empty);
                    
                    return;
                }
            }
        }
        
        // No matches found!
        // Player did not deliver correct recipe
        OnRecipeFailed?.Invoke(this, EventArgs.Empty);
    }
}
