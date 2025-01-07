using System;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayingClockUI : MonoBehaviour
{
    [SerializeField] private Image timerImage;

    private void Start()
    {
        timerImage.fillAmount = 0;
    }

    private void Update()
    {
        float fillAmount = KitchenGameManager.Instance.GetGamePlayingTimerNormalized();
        
        timerImage.fillAmount = fillAmount;
    }
}
