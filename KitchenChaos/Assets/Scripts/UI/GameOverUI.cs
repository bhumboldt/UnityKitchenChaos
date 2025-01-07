using System;
using TMPro;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI recipesDeliveredText;
    
    private void Start()
    {
        KitchenGameManager.Instance.OnStateChange += KitchenGameManager_OnStateChange;

        Hide();
    }

    private void KitchenGameManager_OnStateChange(object sender, EventArgs e)
    {
        if (KitchenGameManager.Instance.IsGameOver())
        {
            recipesDeliveredText.text = DeliveryManager.Instance.GetRecipesDeliveredSuccessfully().ToString();
            Show();
        }
        else
        {
            Hide();
        }
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
