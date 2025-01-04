using System;
using UnityEngine;

public class TrashCounter : BaseCounter
{
    public static event EventHandler OnTrash;
    
    public override void Interact(Player player)
    {
        if (player.HasKitchenObject())
        {
            OnTrash?.Invoke(this, EventArgs.Empty);
            player.GetKitchenObject().DestroySelf();
        }
    }
}
