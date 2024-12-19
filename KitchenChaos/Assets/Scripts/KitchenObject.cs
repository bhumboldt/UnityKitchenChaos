using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO scriptableObject;
    
    private ClearCounter clearCounter;

    public KitchenObjectSO GetScriptObject()
    {
        return scriptableObject;
    }

    public void SetClearCounter(ClearCounter clearCounter)
    {
        this.clearCounter?.ClearKitchenObject();
        
        this.clearCounter = clearCounter;
        if (clearCounter.HasKitchenObject())
        {
            Debug.LogError("Counter already has a kitchen object");
        }
        clearCounter.SetKitchenObject(this);
        transform.parent = clearCounter.GetKitchenObjectFollowTransform();
        transform.localPosition = Vector3.zero;
    }

    public ClearCounter GetClearCounter()
    {
        return clearCounter;
    }
}
