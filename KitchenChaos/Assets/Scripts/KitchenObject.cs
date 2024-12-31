using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO scriptableObject;
    
    private IKitchenObjectParent kitchenObjectParent;

    public KitchenObjectSO GetScriptObject()
    {
        return scriptableObject;
    }

    public void SetKitchenObjectParent(IKitchenObjectParent kitchenObjectParent)
    {
        this.kitchenObjectParent?.ClearKitchenObject();
        
        this.kitchenObjectParent = kitchenObjectParent;
        if (kitchenObjectParent.HasKitchenObject())
        {
            Debug.LogError("Counter already has a kitchen object");
        }
        kitchenObjectParent.SetKitchenObject(this);
        transform.parent = kitchenObjectParent.GetKitchenObjectFollowTransform();
        transform.localPosition = Vector3.zero;
    }

    public IKitchenObjectParent GetKitchenObjectParent()
    {
        return kitchenObjectParent;
    }

    public void DestroySelf()
    {
        kitchenObjectParent.ClearKitchenObject();
        Destroy(gameObject);
    }

    public bool TryGetPlate(out PlateKitchenObject plateKitchenObject)
    {
        if (this is PlateKitchenObject)
        {
            plateKitchenObject = (this as PlateKitchenObject);
            return true;
        }

        plateKitchenObject = null;
        return false;
    }


    public static KitchenObject SpawnKitchenObject(KitchenObjectSO kitchenObjectSo, IKitchenObjectParent kitchenObjectParent)
    {
        Transform kitchenObjectTransform = Instantiate(kitchenObjectSo.prefab);
        KitchenObject ko = kitchenObjectTransform.GetComponent<KitchenObject>();
        ko.SetKitchenObjectParent(kitchenObjectParent);

        return ko;
    }
}
