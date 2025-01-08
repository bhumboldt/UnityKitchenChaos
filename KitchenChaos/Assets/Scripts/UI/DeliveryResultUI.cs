using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeliveryResultUI : MonoBehaviour
{
    private const string POPUP = "Popup";
    
    [SerializeField] private Image backgroundImage;
    [SerializeField] private Image iconImage;
    [SerializeField] private TextMeshProUGUI messageText;
    
    [SerializeField] private Color successColor;
    [SerializeField] private Color failureColor;
    
    [SerializeField] private Sprite successSprite;
    [SerializeField] private Sprite failureSprite;
    
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        DeliveryManager.Instance.OnRecipeCompleted += DeliveryManager_RecipeCompleted;
        DeliveryManager.Instance.OnRecipeFailed += DeliveryManager_RecipeFailed;
        
        gameObject.SetActive(false);
    }

    private void DeliveryManager_RecipeFailed(object sender, EventArgs e)
    {
        gameObject.SetActive(true);
        animator.SetTrigger(POPUP);
        backgroundImage.color = failureColor;
        iconImage.sprite = failureSprite;
        messageText.text = "DELIVERY\nFAILED";
    }

    private void DeliveryManager_RecipeCompleted(object sender, EventArgs e)
    {
        gameObject.SetActive(true);
        animator.SetTrigger(POPUP);
        backgroundImage.color = successColor;
        iconImage.sprite = successSprite;
        messageText.text = "DELIVERY\nSUCCESS";
    }
}
