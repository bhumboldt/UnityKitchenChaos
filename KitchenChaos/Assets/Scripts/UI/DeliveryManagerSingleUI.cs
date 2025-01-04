using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeliveryManagerSingleUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI recipeName;
    [SerializeField] private Transform iconContainer;
    [SerializeField] private Transform iconTemplate;

    private void Awake()
    {
        iconTemplate.gameObject.SetActive(false);
    }

    public void SetRecipeSO(RecipeSO recipeSO)
    {
        recipeName.text = recipeSO.name;

        foreach (Transform child in iconContainer)
        {
            if (child == iconTemplate)
            {
                continue;
            }
            
            Destroy(child.gameObject);
        }

        foreach (KitchenObjectSO kitchenObject in recipeSO.kitchenObjectSoList)
        {
            Transform iconTransform = Instantiate(iconTemplate, iconContainer);
            iconTransform.gameObject.SetActive(true);
            iconTransform.GetComponent<Image>().sprite = kitchenObject.sprite;
        }
    }
}
