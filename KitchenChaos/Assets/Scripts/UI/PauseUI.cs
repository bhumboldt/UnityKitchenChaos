using System;
using UnityEngine;
using UnityEngine.UI;

public class PauseUI : MonoBehaviour
{
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button optionsButton;
    
    private void Start()
    {
        KitchenGameManager.Instance.OnGamePaused += InstanceOnOnGamePaused;
        KitchenGameManager.Instance.OnGameResumed += InstanceOnOnGameResumed;
        
        mainMenuButton.onClick.AddListener(() =>
        {
            Loader.Load(Loader.Scene.MainMenuScene);
        });

        resumeButton.onClick.AddListener(() =>
        {
            KitchenGameManager.Instance.TogglePauseGame();
        });

        optionsButton.onClick.AddListener(() =>
        {
            OptionsUI.Instance.Show();
        });
        
        Hide();
    }

    private void InstanceOnOnGameResumed(object sender, EventArgs e)
    {
        Hide();
    }

    private void InstanceOnOnGamePaused(object sender, EventArgs e)
    {
        Show();
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
