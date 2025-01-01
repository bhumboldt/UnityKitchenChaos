using System;
using System.Collections.Generic;
using UnityEngine;

public class PlateCompleteVisual : MonoBehaviour
{
    [Serializable]
    public struct KitchenObjectSO_GameObject
    {
        public KitchenObjectSO kitchenObjectSo;
        public GameObject gameObject;
    }
    
    [SerializeField] private List<KitchenObjectSO_GameObject> kitchenObjectSoGameObjects = new List<KitchenObjectSO_GameObject>();
    
    [SerializeField] private PlateKitchenObject plateKitchenObject;

    private void Start()
    {
        plateKitchenObject.OnIngredientAdded += PlateKitchenObjectOnOnIngredientAdded;

        foreach (var kitchenObject in kitchenObjectSoGameObjects)
        {
            kitchenObject.gameObject.SetActive(false);
        }
    }

    private void PlateKitchenObjectOnOnIngredientAdded(object sender, PlateKitchenObject.OnIngredientAddedEventArgs e)
    {
        foreach (var kitchenObjectSOGameObject in kitchenObjectSoGameObjects)
        {
            if (kitchenObjectSOGameObject.kitchenObjectSo == e.KitchenObjectSo)
            {
                kitchenObjectSOGameObject.gameObject.SetActive(true);
            }
        }   
    }
}
