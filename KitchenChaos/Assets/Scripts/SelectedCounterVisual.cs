using System;
using UnityEngine;
using UnityEngine.Serialization;

public class SelectedCounterVisual : MonoBehaviour
{
    [SerializeField] private BaseCounter baseCounter;
    [SerializeField] private GameObject[] visualGameObjects;
    
    private void Start()
    {
        // Access singletons in start, not awake, since singletons are set in Awake, before start has been called (aka you could get a null reference error)
        // Use awake for initialization, use start for access
        Player.Instance.OnSelectedCounterChanged += InstanceOnSelectedCounterChanged;
    }

    private void InstanceOnSelectedCounterChanged(object sender, Player.OnSelectedCounterChangedEventArgs e)
    {
        if (e.selectedCounter == baseCounter)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    private void Show()
    {
        foreach (var visualGameObject in visualGameObjects)
        {
            visualGameObject.SetActive(true);
        }
        
    }

    private void Hide()
    {
        foreach (var visualGameObject in visualGameObjects)
        {
            visualGameObject.SetActive(false);
        }
        
    }
}
