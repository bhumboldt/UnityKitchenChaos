using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO scriptableObject;

    public KitchenObjectSO GetScriptObject()
    {
        return scriptableObject;
    }
}
