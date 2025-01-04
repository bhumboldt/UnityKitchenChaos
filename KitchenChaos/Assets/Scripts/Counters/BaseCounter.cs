using System;
using UnityEngine;

public class BaseCounter : MonoBehaviour, IKitchenObjectParent
{
   public static event EventHandler OnAnyObjectPlacedHere;
   
   [SerializeField] private Transform counterTopPoint;

   private KitchenObject kitchenObject;

   public virtual void Interact(Player player)
   {
      Debug.LogError("BaseCounter.Interact()");
   }

   public virtual void InteractAlternate(Player player)
   {
      
   }

public Transform GetKitchenObjectFollowTransform()
   {
      return counterTopPoint;
   }

   public void SetKitchenObject(KitchenObject kitchenObject)
   {
      if (this.kitchenObject != null)
      {
         OnAnyObjectPlacedHere?.Invoke(this, EventArgs.Empty);
      }
      
      this.kitchenObject = kitchenObject;
   }

   public KitchenObject GetKitchenObject()
   {
      return kitchenObject;
   }

   public void ClearKitchenObject()
   {
      kitchenObject = null;
   }

   public bool HasKitchenObject()
   {
      return kitchenObject != null;
   }
}
