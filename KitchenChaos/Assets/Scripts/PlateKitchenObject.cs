using System;
using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObject : KitchenObject
{
  public event EventHandler<OnIngredientAddedEventArgs> OnIngredientAdded;

  public class OnIngredientAddedEventArgs : EventArgs
  {
    public KitchenObjectSO KitchenObjectSo;
  }
  
  [SerializeField] private List<KitchenObjectSO> validKitchenObjectSOList = new List<KitchenObjectSO>();
  
  private List<KitchenObjectSO> kitchenObjectSOList = new List<KitchenObjectSO>();
  
  public bool TryAddIngredient(KitchenObjectSO ingredient)
  {
    if (!validKitchenObjectSOList.Contains(ingredient))
    {
      // Not one we can add
      return false;
    }
    
    if (kitchenObjectSOList.Contains(ingredient))
    {
      // Already has ingredient type
      return false;
    }
    
    kitchenObjectSOList.Add(ingredient);
    
    OnIngredientAdded?.Invoke(this, new OnIngredientAddedEventArgs { KitchenObjectSo = ingredient });
    return true;
  }

  public List<KitchenObjectSO> GetKitchenObjects()
  {
    return kitchenObjectSOList;
  }
}
